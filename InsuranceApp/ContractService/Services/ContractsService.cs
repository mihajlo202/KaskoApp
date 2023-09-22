using ContractService.IServices;
using ContractService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ContractService.Services
{
    public class ContractsService : IContractsService
    {
        private readonly DataContext _dataContext;

        public ContractsService(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IEnumerable<Contract>> GetContracts()
        {
            return await _dataContext.Contracts.ToListAsync();
        }
        public async Task<IEnumerable<CarCasco>> GetCarCascos()
        {
            return await _dataContext.CarCascos.ToListAsync();
        }
        public async Task<IEnumerable<Contract>> CreateContract(Contract contract)
        {
            _dataContext.Contracts.Add(contract);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Contracts.ToListAsync();
        }
        public async Task<IEnumerable<Contract>> UpdateContract(Contract contract)
        {
            var dbContract = await _dataContext.Contracts.FindAsync(contract.Id);

            dbContract.FullName = contract.FullName;
            dbContract.Registration = contract.Registration;
            dbContract.Address = contract.Address;
            dbContract.Activated = contract.Activated;
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Contracts.ToListAsync();
        }
        public async Task<IEnumerable<Contract>> ActivateContract(Contract contract)
        {
            var carCascos = _dataContext.CarCascos.Where((car) => car.CarMake == contract.CarMake && car.CarModel == contract.CarModel).FirstOrDefault();
            if (carCascos != null)
            {
                PaymentInstruction ins = new PaymentInstruction();
                ins.CarCasco = carCascos;
                ins.Contract = contract;
                using (var client = new System.Net.Http.HttpClient())
                {
                    var url = "http://accountingservice/Accounting/CreatePaymentInstuctions";
                    var jsonContent = JsonConvert.SerializeObject(ins);
                    StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string resString = await response.Content.ReadAsStringAsync();
                        if (JsonConvert.DeserializeObject<Boolean>(resString))
                        {
                            contract.Activated = true;
                            return await UpdateContract(contract);
                        }
                    }
                    return null;
                }
            }
            return null;
        }
    }
}
