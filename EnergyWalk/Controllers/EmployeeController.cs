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
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _EmployeeService;
        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
            // Constructor logic if needed
        }

        [HttpPost]
        [Route("SaveEmployee")]
        [ResponseType(typeof(Employee))]
        public async Task<ResponseMessage> SaveEmployee(RequestMessage requestMessage)
        {

            return await _EmployeeService.CreateAsync(requestMessage);
        }
      
        [HttpPost]
        [Route("UpdateEmployee")]
        [ResponseType(typeof(Employee))]
        public async Task<ResponseMessage> UpdateEmployee(RequestMessage requestMessage)
        {

            return await _EmployeeService.UpdateAsync(requestMessage);
        }


        [HttpPost]
        [Route("GetEmployeeByID")]
        [ResponseType(typeof(Employee))]
        public async Task<ResponseMessage> GetEmployeeByID(RequestMessage requestMessage)
        {

            return await _EmployeeService.GetByIdAsync(requestMessage);
        }


        [HttpPost]
        [Route("getAllEmployee")]
        [ResponseType(typeof(Employee))]
        public ResponseMessage GetAllEmployee(RequestMessage requestMessage)
        {
            ResponseMessage response = new ResponseMessage();
            response.ResponseObj = _EmployeeService.GetAllAsync();
            return response;
        }      



    }
}
