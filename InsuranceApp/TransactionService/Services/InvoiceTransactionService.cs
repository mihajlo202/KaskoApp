using System.Data;
using TransactionService.IServices;
using TransactionService.Models;

namespace TransactionService.Services
{
    public class InvoiceTransactionService : IInvoiceTransactionService
    {
        public InvoiceTransactionService() { }
        public async Task<List<Invoice>> ExecuteTransaction(Invoice itemFrom, List<Invoice> itemsTo)
        {
            foreach(Invoice inv in itemsTo)
            {
                var amountToSend = itemFrom.Type == 1 ? itemFrom.OpenAmount : itemFrom.ClosedAmount - itemFrom.Amount;
                if (amountToSend <= 0)
                    break;
                var amountToClose = inv.OpenAmount;
                var amount = amountToSend > amountToClose ? amountToClose : amountToSend;
                itemFrom.OpenAmount -= amount;
                itemFrom.ClosedAmount += amount;
                inv.OpenAmount -= amount;
                inv.ClosedAmount += amount;
            } 
            itemsTo.Add(itemFrom);
            return itemsTo;
        }
    }
}
