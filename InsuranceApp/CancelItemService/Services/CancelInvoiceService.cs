using CancelItemService.IServices;
using CancelItemService.Models;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace CancelItemService.Services
{
    public class CancelInvoiceService : ICancelInvoiceService
    {
        public CancelInvoiceService()
        {
        }
        public async Task<bool> CancelInvoice(Invoice item)
        {
            try
            {
                if (item.ClosedAmount == 0)
                {
                    item.Status = 1;
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        var url = "http://accountingservice/Accounting/UpdateInvoice";
                        var jsonContent = JsonConvert.SerializeObject(item);
                        StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(url, content);
                        if (response.IsSuccessStatusCode)
                        {
                            string resString = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<Boolean>(resString);
                        }
                        return false;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
