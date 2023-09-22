using CancelItemService.IServices;
using CancelItemService.Models;
using CancelItemService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CancelItemService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CancelInvoiceController : ControllerBase
    {
        private readonly ICancelInvoiceService _cancelInvoiceService;
        public CancelInvoiceController(ICancelInvoiceService cancelInvoiceService)
        {
            _cancelInvoiceService = cancelInvoiceService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Invoice>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Invoice>>> CancelInvoice([FromBody] Invoice item)
        {
            return Ok(await this._cancelInvoiceService.CancelInvoice(item));
        }
    }
}
