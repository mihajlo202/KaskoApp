using AccountingService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AccountingService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IAccService _accService;
        public AccountingController(IAccService balanceService)
        {
            _accService = balanceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BankStatement>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BankStatement>>> GetBankStatements()
        {
            return Ok(await this._accService.GetBankStatements());
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Invoice>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Invoice>> GetInvoices()
        {
            return Ok(await this._accService.GetInvoices());
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateBankStatement([FromBody] BankStatement bs)
        {
            return Ok(await this._accService.CreateBankStatement(bs));
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateInvoice([FromBody] Invoice item)
        {
            return Ok(await this._accService.UpdateInvoice(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateInvoice([FromBody] Invoice item)
        {
            return Ok(await this._accService.CreateInvoice(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> ApplyBankStatementItem([FromBody] BankStatement item)
        {
            return Ok(await this._accService.ApplyBankStatementItem(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreatePaymentInstuctions([FromBody] PaymentInstruction item)
        {
            return Ok(await this._accService.CreatePaymentInstuctions(item));
        }
    }
}
