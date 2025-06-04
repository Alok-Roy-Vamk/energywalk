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
    public class ConsumptionService : IConsumptionService
    {
        private readonly EnergyWalkDBContext _energyWalkDBContext;

        public ConsumptionService(EnergyWalkDBContext energyWalkDBContext)
        {
            _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
        }

        public async Task<ResponseMessage> CreateAsync(RequestMessage requestMessage)
        {
            Consumption Consumption = JsonConvert.DeserializeObject<Consumption>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (Consumption == null) throw new ArgumentNullException(nameof(Consumption));
            _energyWalkDBContext.Consumptions.Add(Consumption);
            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = Consumption;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteAsync(int ConsumptionID)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var Consumption = await _energyWalkDBContext.Consumptions.FirstOrDefaultAsync(u => u.ConsumptionID == ConsumptionID);
            if (Consumption == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "Consumption not found.";
                return responseMessage;
            }

            _energyWalkDBContext.Consumptions.Remove(Consumption);
            await _energyWalkDBContext.SaveChangesAsync();

            responseMessage.ResponseObj = Consumption;
            responseMessage.StatusCode = 200; // OK
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllAsync()
        {

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.Consumptions.ToListAsync();
            return responseMessage;
        }

        public async Task<ResponseMessage?> GetByIdAsync(RequestMessage requestMessage)
        {
            int ConsumptionID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.Consumptions.FirstOrDefaultAsync(u => u.ConsumptionID == ConsumptionID);
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage)
        {

            Consumption Consumption = JsonConvert.DeserializeObject<Consumption>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (Consumption == null) throw new ArgumentNullException(nameof(Consumption));
            var existingConsumption = await _energyWalkDBContext.Consumptions
                .FirstOrDefaultAsync(u => u.ConsumptionID == Consumption.ConsumptionID);
            if (existingConsumption == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "Consumption not found.";
                return responseMessage;
            }

            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = Consumption;
            return responseMessage;
        }
    }

    public interface IConsumptionService
    {
        Task<ResponseMessage> CreateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetByIdAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllAsync();
        Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteAsync(int id);
    }



}
