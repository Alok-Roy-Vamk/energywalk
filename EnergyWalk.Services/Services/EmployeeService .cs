using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyWalk.Common.DTO;
using EnergyWalk.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EnergyWalk.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public EmployeeService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<Employee> CreateAsync(Employee user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                _energyWalkDBContext.Employees.Add(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAsync(int EmployeeID)
            {
                var user = await _energyWalkDBContext.Employees
                    .FirstOrDefaultAsync(u => u.EmployeeID == EmployeeID);
                if (user == null)
                    return false;

                _energyWalkDBContext.Employees.Remove(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<Employee>> GetAllAsync()
            {
                return await _energyWalkDBContext.Employees.ToListAsync();
            }

            public async Task<Employee?> GetByIdAsync(int EmployeeID)
            {
                return await _energyWalkDBContext.Employees
                    .FirstOrDefaultAsync(u => u.EmployeeID == EmployeeID);
            }

            public async Task<Employee> UpdateAsync(Employee user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                var existingUser = await _energyWalkDBContext.Employees
                    .FirstOrDefaultAsync(u => u.EmployeeID == user.EmployeeID);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                await _energyWalkDBContext.SaveChangesAsync();
                return existingUser;
            }
    }

    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee user);
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> UpdateAsync(Employee user);
        Task<bool> DeleteAsync(int id);
    }


}
