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
    public class CompanyController : ControllerBase
    {

        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
            // Constructor logic if needed
        }

        [HttpPost]
        [Route("SaveCompany")]
        [ResponseType(typeof(Company))]
        public async Task<ResponseMessage> SaveCompany(RequestMessage requestMessage)
        {

            return await _companyService.CreateAsync(requestMessage);
        }
      
        [HttpPost]
        [Route("UpdateCompany")]
        [ResponseType(typeof(Company))]
        public async Task<ResponseMessage> UpdateCompany(RequestMessage requestMessage)
        {

            return await _companyService.UpdateAsync(requestMessage);
        }


        [HttpPost]
        [Route("GetCompanyByID")]
        [ResponseType(typeof(Company))]
        public async Task<ResponseMessage> GetCompanyByID(RequestMessage requestMessage)
        {

            return await _companyService.GetByIdAsync(requestMessage);
        }


        [HttpPost]
        [Route("getAllCompany")]
        [ResponseType(typeof(Company))]
        public ResponseMessage GetAllCompany(RequestMessage requestMessage)
        {
            ResponseMessage response = new ResponseMessage();
            response.ResponseObj = _companyService.GetAllAsync();
            return response;
        }      



    }
}
