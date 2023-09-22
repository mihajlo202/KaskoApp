using TransactionService.Models;

namespace TransactionService.IServices
{
    public interface IInvoiceTransactionService
    {
        public Task<List<Invoice>> ExecuteTransaction(Invoice itemFrom, List<Invoice> itemsTo);
    }
}
