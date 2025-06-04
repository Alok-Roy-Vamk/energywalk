using System.Threading.Tasks;
using System.Web.Http.Description;
using EnergyWalk.Common.DTO;
using EnergyWalk.Common.OperationDTO;
using EnergyWalk.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergyWalk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {

        private readonly ISystemUserService _SystemUserService;
        public SystemUserController(ISystemUserService SystemUserService)
        {
            _SystemUserService = SystemUserService;
            // Constructor logic if needed
        }

        [HttpPost]
        [Route("SaveSystemUser")]
        [ResponseType(typeof(SystemUser))]
        public async Task<ResponseMessage> SaveSystemUser(RequestMessage requestMessage)
        {

            return await _SystemUserService.CreateAsync(requestMessage);
        }
      
        [HttpPost]
        [Route("UpdateSystemUser")]
        [ResponseType(typeof(SystemUser))]
        public async Task<ResponseMessage> UpdateSystemUser(RequestMessage requestMessage)
        {

            return await _SystemUserService.UpdateAsync(requestMessage);
        }


        [HttpPost]
        [Route("GetSystemUserByID")]
        [ResponseType(typeof(SystemUser))]
        public async Task<ResponseMessage> GetSystemUserByID(RequestMessage requestMessage)
        {

            return await _SystemUserService.GetByIdAsync(requestMessage);
        }


        [HttpPost]
        [Route("getAllSystemUser")]
        [ResponseType(typeof(SystemUser))]
        public ResponseMessage GetAllSystemUser(RequestMessage requestMessage)
        {
            ResponseMessage response = new ResponseMessage();
            response.ResponseObj = _SystemUserService.GetAllAsync();
            return response;
        }      



    }
}
