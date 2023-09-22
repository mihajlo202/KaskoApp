using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessBankStatementService.IServices;
using ProcessBankStatementService.Models;
using System.Net;

namespace ProcessBankStatementService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProcessBankStatementController : ControllerBase
    {
        private readonly IProcessBSService _processBSService;
        public ProcessBankStatementController(IProcessBSService processBSService)
        {
            _processBSService = processBSService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Invoice), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Invoice>> ProcessBankStatement([FromBody] BankStatement item)
        {
            return Ok(await this._processBSService.ProcessBankStatement(item));
        }
    }
}
