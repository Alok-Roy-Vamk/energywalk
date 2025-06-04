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
    public class IssueReportService : IIssueReportService
    {
        private readonly EnergyWalkDBContext _energyWalkDBContext;

        public IssueReportService(EnergyWalkDBContext energyWalkDBContext)
        {
            _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
        }

        public async Task<ResponseMessage> CreateAsync(RequestMessage requestMessage)
        {
            IssueReport IssueReport = JsonConvert.DeserializeObject<IssueReport>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (IssueReport == null) throw new ArgumentNullException(nameof(IssueReport));
            _energyWalkDBContext.IssueReports.Add(IssueReport);
            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = IssueReport;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteAsync(int IssueReportID)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var IssueReport = await _energyWalkDBContext.IssueReports.FirstOrDefaultAsync(u => u.IssueReportID == IssueReportID);
            if (IssueReport == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "IssueReport not found.";
                return responseMessage;
            }

            _energyWalkDBContext.IssueReports.Remove(IssueReport);
            await _energyWalkDBContext.SaveChangesAsync();

            responseMessage.ResponseObj = IssueReport;
            responseMessage.StatusCode = 200; // OK
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllAsync()
        {

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.IssueReports.ToListAsync();
            return responseMessage;
        }

        public async Task<ResponseMessage?> GetByIdAsync(RequestMessage requestMessage)
        {
            int IssueReportID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.IssueReports.FirstOrDefaultAsync(u => u.IssueReportID == IssueReportID);
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage)
        {

            IssueReport IssueReport = JsonConvert.DeserializeObject<IssueReport>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (IssueReport == null) throw new ArgumentNullException(nameof(IssueReport));
            var existingIssueReport = await _energyWalkDBContext.IssueReports
                .FirstOrDefaultAsync(u => u.IssueReportID == IssueReport.IssueReportID);
            if (existingIssueReport == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "IssueReport not found.";
                return responseMessage;
            }

            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = IssueReport;
            return responseMessage;
        }
    }

    public interface IIssueReportService
    {
        Task<ResponseMessage> CreateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetByIdAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllAsync();
        Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteAsync(int id);
    }


}
