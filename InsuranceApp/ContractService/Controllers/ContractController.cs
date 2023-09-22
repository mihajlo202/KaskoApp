using ContractService.Data;
using ContractService.IServices;
using ContractService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ContractService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractsService _contractsService;

        public ContractController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Contract>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            return Ok(await _contractsService.GetContracts());
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarCasco>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CarCasco>> GetCarCascos()
        {
            return Ok(await _contractsService.GetCarCascos());
        }
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Contract>>> CreateContract(Contract contract)
        {
            return Ok(await _contractsService.CreateContract(contract));
        }
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Contract>>> UpdateContract(Contract contract)
        {
            return Ok(await _contractsService.UpdateContract(contract));
        }
        [HttpPost]
        [ProducesResponseType(typeof(Contract), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Contract>>> ActivateContract(Contract contract)
        {
            return Ok(await _contractsService.ActivateContract(contract));
        }
    }
}
