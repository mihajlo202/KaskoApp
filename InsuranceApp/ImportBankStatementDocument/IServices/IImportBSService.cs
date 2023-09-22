using ImportBankStatementDocument.Models;

namespace ImportBankStatementDocument.IServices
{
    public interface IImportBSService
    {
        public Task<BankStatement> ImportFile(IFormFile file);
    }
}
