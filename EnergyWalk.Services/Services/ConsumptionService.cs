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
    public class ConsumptionService : IConsumptionService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public ConsumptionService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<Consumption> CreateAsync(Consumption user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                _energyWalkDBContext.Consumptions.Add(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAsync(int ConsumptionID)
            {
                var user = await _energyWalkDBContext.Consumptions
                    .FirstOrDefaultAsync(u => u.ConsumptionID == ConsumptionID);
                if (user == null)
                    return false;

                _energyWalkDBContext.Consumptions.Remove(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<Consumption>> GetAllAsync()
            {
                return await _energyWalkDBContext.Consumptions.ToListAsync();
            }

            public async Task<Consumption?> GetByIdAsync(int ConsumptionID)
            {
                return await _energyWalkDBContext.Consumptions
                    .FirstOrDefaultAsync(u => u.ConsumptionID == ConsumptionID);
            }

            public async Task<Consumption> UpdateAsync(Consumption user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                var existingUser = await _energyWalkDBContext.Consumptions
                    .FirstOrDefaultAsync(u => u.ConsumptionID == user.ConsumptionID);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                await _energyWalkDBContext.SaveChangesAsync();
                return existingUser;
            }
    }

    public interface IConsumptionService
    {
        Task<Consumption> CreateAsync(Consumption user);
        Task<Consumption?> GetByIdAsync(int id);
        Task<IEnumerable<Consumption>> GetAllAsync();
        Task<Consumption> UpdateAsync(Consumption user);
        Task<bool> DeleteAsync(int id);
    }


}
