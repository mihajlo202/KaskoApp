using AccountingService.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace AccountingService.Services
{
    public class AccService : IAccService
    {
        private readonly DataContext _dataContext;

        public AccService(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IEnumerable<BankStatement>> GetBankStatements()
        {
            return await _dataContext.BankStatement.ToListAsync(); 
        }
        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await _dataContext.Invoices.ToListAsync();
        }
        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _dataContext.Invoices.FindAsync(id);
        }
        public async Task<List<Invoice>> GetInvoicesByContractId(int id)
        {
            return await _dataContext.Invoices.Where((inv) => inv.ContractId == id).ToListAsync();
        }
        public async Task<BankStatement> CreateBankStatement(BankStatement bs)
        {
            _dataContext.BankStatement.Add(bs);
            if (await _dataContext.SaveChangesAsync() > 0)
                return bs;
            else
                return null;
        }
        private async Task<bool> UpdateBankStatement(BankStatement item)
        {
            var dbBankStatement = await _dataContext.BankStatement.FindAsync(item.Id);
            dbBankStatement.Error = item.Error;
            dbBankStatement.Applied = item.Applied;
            return await _dataContext.SaveChangesAsync() > 0;
        }
        public async Task<Invoice> CreateInvoice(Invoice item)
        {
            _dataContext.Invoices.Add(item);
            if (await _dataContext.SaveChangesAsync() > 0)
                return item;
            else
                return null;
        }
        public async Task<bool> UpdateInvoice(Invoice item)
        {
            var dbItem = await _dataContext.Invoices.FindAsync(item.Id);
            dbItem.OpenAmount = item.OpenAmount;
            dbItem.ClosedAmount = item.ClosedAmount;
            dbItem.Status = item.Status;
            return await _dataContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> ApplyBankStatementItem(BankStatement item)
        {
            var newItem = await ProcessBankStatement(item);
            if (newItem == null)
            {
                item.Error = "Read XML failed.";
                await this.UpdateBankStatement(item);
                return false;
            }
            var matchInvoices = await this.GetInvoicesByContractId(newItem.ContractId ?? -1);
            if (matchInvoices != null && matchInvoices.Count > 0)
            {
                var newInvoice = await this.CreateInvoice(newItem);
                await this.Transaction(newInvoice, matchInvoices);
            }
            else
            {
                item.Error = "Matching item not found.";
                await this.UpdateBankStatement(item);
                return false;
            }
            item.Applied = true;
            var res = await UpdateBankStatement(item);
            return true;
        }
        private async Task<Invoice> ProcessBankStatement(BankStatement item)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var url = "http://processbankstatementservice/ProcessBankStatement/ProcessBankStatement";
                var jsonContent = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Invoice>(resString);
                }
                else
                    return null;
            }
        }
        private async Task<bool> Transaction(Invoice itemFrom, List<Invoice> itemsTo)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var url = "http://transactionservice/Transaction/ExecuteTransaction";
                var tran = new Transaction(itemFrom, itemsTo);
                var jsonContent = JsonConvert.SerializeObject(tran);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resString = await response.Content.ReadAsStringAsync();
                    List<Invoice> invToUpd = JsonConvert.DeserializeObject<List<Invoice>>(resString);
                    foreach (Invoice inv in invToUpd)
                        await this.UpdateInvoice(inv);
                    return true;
                }
                else
                    return false;
            }
        }
        public async Task<bool> CreatePaymentInstuctions(PaymentInstruction item)
        {
            int currentYear = DateTime.Now.Year;
            int payMounts = item.Contract.Mounth;
            int price = 0;
            int diff = currentYear - item.Contract.Year;
            if (diff <= 5)
                price = item.CarCasco.PriceFirst;
            else if (diff > 5 && diff <= 10)
                    price = item.CarCasco.PriceSecond;
            else
                price = item.CarCasco.PriceThird;
            if (item.Contract.Tief)
                price += 20;
            if (item.Contract.Weather)
                price += 10;
            if (item.Contract.Crash)
                price += 30;
            for (int i = 0; i < payMounts; i++)
            {
                Invoice inv = new Invoice();
                inv.ContractId = item.Contract.Id;
                inv.Amount = price / payMounts;
                if(i + 1 == payMounts)
                {
                    inv.Amount += price % payMounts;
                }
                inv.OpenAmount = inv.Amount;
                inv.ClosedAmount = 0;
                inv.Type = 0;

                _dataContext.Invoices.Add(inv);
            }
            if (await _dataContext.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }
    }
}