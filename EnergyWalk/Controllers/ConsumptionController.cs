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
    public class ConsumptionController : ControllerBase
    {

        private readonly IConsumptionService _ConsumptionService;
        public ConsumptionController(IConsumptionService ConsumptionService)
        {
            _ConsumptionService = ConsumptionService;
            // Constructor logic if needed
        }

        [HttpPost]
        [Route("SaveConsumption")]
        [ResponseType(typeof(Consumption))]
        public async Task<ResponseMessage> SaveConsumption(RequestMessage requestMessage)
        {

            return await _ConsumptionService.CreateAsync(requestMessage);
        }
      
        [HttpPost]
        [Route("UpdateConsumption")]
        [ResponseType(typeof(Consumption))]
        public async Task<ResponseMessage> UpdateConsumption(RequestMessage requestMessage)
        {

            return await _ConsumptionService.UpdateAsync(requestMessage);
        }


        [HttpPost]
        [Route("GetConsumptionByID")]
        [ResponseType(typeof(Consumption))]
        public async Task<ResponseMessage> GetConsumptionByID(RequestMessage requestMessage)
        {

            return await _ConsumptionService.GetByIdAsync(requestMessage);
        }


        [HttpPost]
        [Route("getAllConsumption")]
        [ResponseType(typeof(Consumption))]
        public ResponseMessage GetAllConsumption(RequestMessage requestMessage)
        {
            ResponseMessage response = new ResponseMessage();
            response.ResponseObj = _ConsumptionService.GetAllAsync();
            return response;
        }      



    }
}
