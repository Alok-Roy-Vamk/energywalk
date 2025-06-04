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


    public class SystemUserService : ISystemUserService
    {
        private readonly EnergyWalkDBContext _energyWalkDBContext;

        public SystemUserService(EnergyWalkDBContext energyWalkDBContext)
        {
            _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
        }

        public async Task<ResponseMessage> CreateAsync(RequestMessage requestMessage)
        {
            SystemUser SystemUser = JsonConvert.DeserializeObject<SystemUser>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (SystemUser == null) throw new ArgumentNullException(nameof(SystemUser));
            _energyWalkDBContext.SystemUsers.Add(SystemUser);
            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = SystemUser;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteAsync(int SystemUserID)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var SystemUser = await _energyWalkDBContext.SystemUsers.FirstOrDefaultAsync(u => u.SystemUserID == SystemUserID);
            if (SystemUser == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "SystemUser not found.";
                return responseMessage;
            }

            _energyWalkDBContext.SystemUsers.Remove(SystemUser);
            await _energyWalkDBContext.SaveChangesAsync();

            responseMessage.ResponseObj = SystemUser;
            responseMessage.StatusCode = 200; // OK
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllAsync()
        {

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.SystemUsers.ToListAsync();
            return responseMessage;
        }

        public async Task<ResponseMessage?> GetByIdAsync(RequestMessage requestMessage)
        {
            int SystemUserID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.SystemUsers.FirstOrDefaultAsync(u => u.SystemUserID == SystemUserID);
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage)
        {

            SystemUser SystemUser = JsonConvert.DeserializeObject<SystemUser>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (SystemUser == null) throw new ArgumentNullException(nameof(SystemUser));
            var existingSystemUser = await _energyWalkDBContext.SystemUsers
                .FirstOrDefaultAsync(u => u.SystemUserID == SystemUser.SystemUserID);
            if (existingSystemUser == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "SystemUser not found.";
                return responseMessage;
            }

            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = SystemUser;
            return responseMessage;
        }
    }

    public interface ISystemUserService
    {
        Task<ResponseMessage> CreateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetByIdAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllAsync();
        Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteAsync(int id);
    }

}
