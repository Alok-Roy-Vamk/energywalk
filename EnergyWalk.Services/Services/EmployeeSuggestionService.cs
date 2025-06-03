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
    public class EmployeeSuggestionService : IEmployeeSuggestionService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public EmployeeSuggestionService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<EmployeeSuggestion> CreateAsync(EmployeeSuggestion user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                _energyWalkDBContext.EmployeeSuggestions.Add(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAsync(int EmployeeSuggestionID)
            {
                var user = await _energyWalkDBContext.EmployeeSuggestions
                    .FirstOrDefaultAsync(u => u.EmployeeSuggestionID == EmployeeSuggestionID);
                if (user == null)
                    return false;

                _energyWalkDBContext.EmployeeSuggestions.Remove(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<EmployeeSuggestion>> GetAllAsync()
            {
                return await _energyWalkDBContext.EmployeeSuggestions.ToListAsync();
            }

            public async Task<EmployeeSuggestion?> GetByIdAsync(int EmployeeSuggestionID)
            {
                return await _energyWalkDBContext.EmployeeSuggestions
                    .FirstOrDefaultAsync(u => u.EmployeeSuggestionID == EmployeeSuggestionID);
            }

            public async Task<EmployeeSuggestion> UpdateAsync(EmployeeSuggestion user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                var existingUser = await _energyWalkDBContext.EmployeeSuggestions
                    .FirstOrDefaultAsync(u => u.EmployeeSuggestionID == user.EmployeeSuggestionID);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                await _energyWalkDBContext.SaveChangesAsync();
                return existingUser;
            }
    }

    public interface IEmployeeSuggestionService
    {
        Task<EmployeeSuggestion> CreateAsync(EmployeeSuggestion user);
        Task<EmployeeSuggestion?> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeSuggestion>> GetAllAsync();
        Task<EmployeeSuggestion> UpdateAsync(EmployeeSuggestion user);
        Task<bool> DeleteAsync(int id);
    }


}
