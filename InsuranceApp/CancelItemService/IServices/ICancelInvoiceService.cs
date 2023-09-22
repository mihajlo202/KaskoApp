using CancelItemService.Models;

namespace CancelItemService.IServices
{
    public interface ICancelInvoiceService
    {
        public Task<bool> CancelInvoice(Invoice item);
    }
}
