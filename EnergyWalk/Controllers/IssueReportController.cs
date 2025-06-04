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
    public class IssueReportController : ControllerBase
    {

        private readonly IIssueReportService _IssueReportService;
        public IssueReportController(IIssueReportService IssueReportService)
        {
            _IssueReportService = IssueReportService;
            // Constructor logic if needed
        }

        [HttpPost]
        [Route("SaveIssueReport")]
        [ResponseType(typeof(IssueReport))]
        public async Task<ResponseMessage> SaveIssueReport(RequestMessage requestMessage)
        {

            return await _IssueReportService.CreateAsync(requestMessage);
        }
      
        [HttpPost]
        [Route("UpdateIssueReport")]
        [ResponseType(typeof(IssueReport))]
        public async Task<ResponseMessage> UpdateIssueReport(RequestMessage requestMessage)
        {

            return await _IssueReportService.UpdateAsync(requestMessage);
        }


        [HttpPost]
        [Route("GetIssueReportByID")]
        [ResponseType(typeof(IssueReport))]
        public async Task<ResponseMessage> GetIssueReportByID(RequestMessage requestMessage)
        {

            return await _IssueReportService.GetByIdAsync(requestMessage);
        }


        [HttpPost]
        [Route("getAllIssueReport")]
        [ResponseType(typeof(IssueReport))]
        public ResponseMessage GetAllIssueReport(RequestMessage requestMessage)
        {
            ResponseMessage response = new ResponseMessage();
            response.ResponseObj = _IssueReportService.GetAllAsync();
            return response;
        }      



    }
}
