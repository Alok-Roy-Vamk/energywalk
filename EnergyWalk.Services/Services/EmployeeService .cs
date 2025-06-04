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

    public class EmployeeService : IEmployeeService
    {
        private readonly EnergyWalkDBContext _energyWalkDBContext;

        public EmployeeService(EnergyWalkDBContext energyWalkDBContext)
        {
            _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
        }

        public async Task<ResponseMessage> CreateAsync(RequestMessage requestMessage)
        {
            Employee Employee = JsonConvert.DeserializeObject<Employee>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (Employee == null) throw new ArgumentNullException(nameof(Employee));
            _energyWalkDBContext.Employees.Add(Employee);
            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = Employee;
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteAsync(int EmployeeID)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var Employee = await _energyWalkDBContext.Employees.FirstOrDefaultAsync(u => u.EmployeeID == EmployeeID);
            if (Employee == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "Employee not found.";
                return responseMessage;
            }

            _energyWalkDBContext.Employees.Remove(Employee);
            await _energyWalkDBContext.SaveChangesAsync();

            responseMessage.ResponseObj = Employee;
            responseMessage.StatusCode = 200; // OK
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllAsync()
        {

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.Employees.ToListAsync();
            return responseMessage;
        }

        public async Task<ResponseMessage?> GetByIdAsync(RequestMessage requestMessage)
        {
            int EmployeeID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.ResponseObj = await _energyWalkDBContext.Employees.FirstOrDefaultAsync(u => u.EmployeeID == EmployeeID);
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage)
        {

            Employee Employee = JsonConvert.DeserializeObject<Employee>(requestMessage.RequestObj.ToString());
            ResponseMessage responseMessage = new ResponseMessage();
            if (Employee == null) throw new ArgumentNullException(nameof(Employee));
            var existingEmployee = await _energyWalkDBContext.Employees
                .FirstOrDefaultAsync(u => u.EmployeeID == Employee.EmployeeID);
            if (existingEmployee == null)
            {
                responseMessage.StatusCode = 404; // Not Found
                responseMessage.Message = "Employee not found.";
                return responseMessage;
            }

            await _energyWalkDBContext.SaveChangesAsync();
            responseMessage.ResponseObj = Employee;
            return responseMessage;
        }
    }

    public interface IEmployeeService
    {
        Task<ResponseMessage> CreateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetByIdAsync(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllAsync();
        Task<ResponseMessage> UpdateAsync(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteAsync(int id);
    }


}
