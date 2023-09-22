using ImportBankStatementDocument.IServices;
using ImportBankStatementDocument.Models;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace ImportBankStatementDocument.Services
{
    public class ImportBSService : IImportBSService
    {
        public async Task<BankStatement> ImportFile(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                    var result = new StringBuilder();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            result.AppendLine(await reader.ReadLineAsync());
                    }
                    BankStatement bs = new BankStatement();
                    bs.Applied = false;
                    bs.Error = string.Empty;
                    bs.FileName = fileName;
                    bs.Content = result.ToString();
                    bs.CreatedOn = DateTime.Now;
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        var url = "http://accountingservice/Accounting/CreateBankStatement";
                        var jsonContent = JsonConvert.SerializeObject(bs);
                        StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(url, content);
                        if (response.IsSuccessStatusCode)
                        {
                            string resString = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<BankStatement>(resString);
                        }
                        return null;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
