using ProcessBankStatementService.IServices;
using ProcessBankStatementService.Models;
using System.Xml;

namespace ProcessBankStatementService.Services
{
    public class ProcessBSService : IProcessBSService
    {
        public async Task<Invoice> ProcessBankStatement(BankStatement i)
        {
            try
            {
                var newItem = new Invoice();
                newItem.Type = 1;
                newItem.Status = 0;
                newItem.ClosedAmount = 0;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(i.Content);
                var id = doc.GetElementsByTagName("Id");
                var amount = doc.GetElementsByTagName("Amt");
                newItem.Amount = Int32.Parse(amount[0].InnerText);
                newItem.OpenAmount = newItem.Amount;
                newItem.ContractId = Int32.Parse(id[0].InnerText);
                return newItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}