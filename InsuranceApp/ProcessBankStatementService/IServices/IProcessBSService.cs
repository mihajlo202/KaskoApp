using ProcessBankStatementService.Models;

namespace ProcessBankStatementService.IServices
{
    public interface IProcessBSService
    {
        public Task<Invoice> ProcessBankStatement(BankStatement item);
    }
}
