using ImportBankStatementDocument.IServices;
using ImportBankStatementDocument.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ImportBankStatementDocument.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ImportBankStatementController : ControllerBase
    {
        private readonly IImportBSService _importBankStatementService;
        public ImportBankStatementController(IImportBSService import)
        {
            _importBankStatementService = import;
        }

        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BankStatement>> ImportBankStatement()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            return Ok(await this._importBankStatementService.ImportFile(file));
        }
    }
}
