using AccountingService.Models;

namespace AccountingService.IServices
{
    public interface IAccService
    {
        public Task<IEnumerable<BankStatement>> GetBankStatements();
        public Task<IEnumerable<Invoice>> GetInvoices();
        public Task<BankStatement> CreateBankStatement(BankStatement bs);
        public Task<Invoice> CreateInvoice(Invoice item);
        public Task<bool> ApplyBankStatementItem(BankStatement item);
        public Task<Invoice> GetInvoiceById(int id);
        public Task<bool> UpdateInvoice(Invoice item); 
        public Task<bool> CreatePaymentInstuctions(PaymentInstruction item);
    }
}
