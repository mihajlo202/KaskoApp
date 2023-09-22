using ContractService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ContractService.IServices
{
    public interface IContractsService
    {
        public Task<IEnumerable<Contract>> GetContracts();
        public Task<IEnumerable<CarCasco>> GetCarCascos();
        public Task<IEnumerable<Contract>> CreateContract(Contract contract);
        public Task<IEnumerable<Contract>> UpdateContract(Contract contract);
        public Task<IEnumerable<Contract>> ActivateContract(Contract contract);
    }
}
