using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyWalk.Common.DTO;
using EnergyWalk.Common.OperationDTO;
using EnergyWalk.DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EnergyWalk.Services.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly EnergyWalkDBContext _energyWalkDBContext;

        public CompanyService(EnergyWalkDBContext energyWalkDBContext)
        {
            _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
        }

        public async Task<ResponseMessage> CreateAsync(RequestMessage requestMessage)
        {
            Company company = JsonConvert.DeserializeObject<Company>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (company == null) throw new ArgumentNullException(nameof(company));
            _energyWalkDBContext.Companys.Add(company);
            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = company;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteAsync(int companyID)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var company = await _energyWalkDBContext.Companys.FirstOrDefaultAsync(u => u.CompanyID == companyID);
            if (company == null)
            {   responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "Company not found.";
                return responseMessage;
            }

            _energyWalkDBContext.Companys.Remove(company);
            await _energyWalkDBContext.SaveChangesAsync();

            responseMessage.ResponseObj = company;
            responseMessage.StatusCode = 200; // OK
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllAsync()
        {

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.Companys.ToListAsync();
            return responseMessage;
        }

        public async Task<ResponseMessage?> GetByIdAsync(RequestMessage requestMessage)
        {
            int CompanyID= JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.Companys.FirstOrDefaultAsync(u => u.CompanyID == CompanyID);
            return responseMessage;  
        }

        public async Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage)
        {

            Company company = JsonConvert.DeserializeObject<Company>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (company == null) throw new ArgumentNullException(nameof(company));
            var existingcompany = await _energyWalkDBContext.Companys
                .FirstOrDefaultAsync(u => u.CompanyID == company.CompanyID);
            if (existingcompany == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "Company not found.";
                return responseMessage;
            }              

            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = company;
            return responseMessage;
        }
    }

    public interface ICompanyService
    {
        Task<ResponseMessage> CreateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetByIdAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllAsync();
        Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteAsync(int id);
    }


}
