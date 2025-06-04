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
    public class EmployeeSuggestionService : IEmployeeSuggestionService
    {
        private readonly EnergyWalkDBContext _energyWalkDBContext;

        public EmployeeSuggestionService(EnergyWalkDBContext energyWalkDBContext)
        {
            _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
        }

        public async Task<ResponseMessage> CreateAsync(RequestMessage requestMessage)
        {
            EmployeeSuggestion EmployeeSuggestion = JsonConvert.DeserializeObject<EmployeeSuggestion>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (EmployeeSuggestion == null) throw new ArgumentNullException(nameof(EmployeeSuggestion));
            _energyWalkDBContext.EmployeeSuggestions.Add(EmployeeSuggestion);
            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = EmployeeSuggestion;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteAsync(int EmployeeSuggestionID)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var EmployeeSuggestion = await _energyWalkDBContext.EmployeeSuggestions.FirstOrDefaultAsync(u => u.EmployeeSuggestionID == EmployeeSuggestionID);
            if (EmployeeSuggestion == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "EmployeeSuggestion not found.";
                return responseMessage;
            }

            _energyWalkDBContext.EmployeeSuggestions.Remove(EmployeeSuggestion);
            await _energyWalkDBContext.SaveChangesAsync();

            responseMessage.ResponseObj = EmployeeSuggestion;
            responseMessage.StatusCode = 200; // OK
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllAsync()
        {

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.EmployeeSuggestions.ToListAsync();
            return responseMessage;
        }

        public async Task<ResponseMessage?> GetByIdAsync(RequestMessage requestMessage)
        {
            int EmployeeSuggestionID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.EmployeeSuggestions.FirstOrDefaultAsync(u => u.EmployeeSuggestionID == EmployeeSuggestionID);
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage)
        {

            EmployeeSuggestion EmployeeSuggestion = JsonConvert.DeserializeObject<EmployeeSuggestion>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (EmployeeSuggestion == null) throw new ArgumentNullException(nameof(EmployeeSuggestion));
            var existingEmployeeSuggestion = await _energyWalkDBContext.EmployeeSuggestions
                .FirstOrDefaultAsync(u => u.EmployeeSuggestionID == EmployeeSuggestion.EmployeeSuggestionID);
            if (existingEmployeeSuggestion == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "EmployeeSuggestion not found.";
                return responseMessage;
            }

            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = EmployeeSuggestion;
            return responseMessage;
        }
    }

    public interface IEmployeeSuggestionService
    {
        Task<ResponseMessage> CreateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetByIdAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllAsync();
        Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteAsync(int id);
    }


}
