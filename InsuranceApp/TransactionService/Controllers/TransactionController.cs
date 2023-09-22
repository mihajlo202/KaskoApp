using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Transactions;
using TransactionService.IServices;
using TransactionService.Models;

namespace TransactionService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IInvoiceTransactionService _transactionService;
        public TransactionController(IInvoiceTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Invoice>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Invoice>>> ExecuteTransaction([FromBody] InvoiceTransaction trans)
        {
            return Ok(await this._transactionService.ExecuteTransaction(trans.ItemFrom, trans.ItemsTo));
        }
    }
}
