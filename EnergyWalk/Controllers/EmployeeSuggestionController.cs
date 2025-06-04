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
    public class EmployeeSuggestionController : ControllerBase
    {

        private readonly IEmployeeSuggestionService _EmployeeSuggestionService;
        public EmployeeSuggestionController(IEmployeeSuggestionService EmployeeSuggestionService)
        {
            _EmployeeSuggestionService = EmployeeSuggestionService;
            // Constructor logic if needed
        }

        [HttpPost]
        [Route("SaveEmployeeSuggestion")]
        [ResponseType(typeof(EmployeeSuggestion))]
        public async Task<ResponseMessage> SaveEmployeeSuggestion(RequestMessage requestMessage)
        {

            return await _EmployeeSuggestionService.CreateAsync(requestMessage);
        }
      
        [HttpPost]
        [Route("UpdateEmployeeSuggestion")]
        [ResponseType(typeof(EmployeeSuggestion))]
        public async Task<ResponseMessage> UpdateEmployeeSuggestion(RequestMessage requestMessage)
        {

            return await _EmployeeSuggestionService.UpdateAsync(requestMessage);
        }


        [HttpPost]
        [Route("GetEmployeeSuggestionByID")]
        [ResponseType(typeof(EmployeeSuggestion))]
        public async Task<ResponseMessage> GetEmployeeSuggestionByID(RequestMessage requestMessage)
        {

            return await _EmployeeSuggestionService.GetByIdAsync(requestMessage);
        }


        [HttpPost]
        [Route("getAllEmployeeSuggestion")]
        [ResponseType(typeof(EmployeeSuggestion))]
        public ResponseMessage GetAllEmployeeSuggestion(RequestMessage requestMessage)
        {
            ResponseMessage response = new ResponseMessage();
            response.ResponseObj = _EmployeeSuggestionService.GetAllAsync();
            return response;
        }      



    }
}
