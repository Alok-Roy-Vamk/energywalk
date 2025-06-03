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
    public class TeamService : ITeamService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public TeamService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<Team> CreateAsync(Team user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                _energyWalkDBContext.Teams.Add(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAsync(int TeamID)
            {
                var user = await _energyWalkDBContext.Teams
                    .FirstOrDefaultAsync(u => u.TeamID == TeamID);
                if (user == null)
                    return false;

                _energyWalkDBContext.Teams.Remove(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<Team>> GetAllAsync()
            {
                return await _energyWalkDBContext.Teams.ToListAsync();
            }

            public async Task<Team?> GetByIdAsync(int TeamID)
            {
                return await _energyWalkDBContext.Teams
                    .FirstOrDefaultAsync(u => u.TeamID == TeamID);
            }

            public async Task<Team> UpdateAsync(Team user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                var existingUser = await _energyWalkDBContext.Teams
                    .FirstOrDefaultAsync(u => u.TeamID == user.TeamID);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                await _energyWalkDBContext.SaveChangesAsync();
                return existingUser;
            }
    }

    public interface ITeamService
    {
        Task<Team> CreateAsync(Team user);
        Task<Team?> GetByIdAsync(int id);
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team> UpdateAsync(Team user);
        Task<bool> DeleteAsync(int id);
    }


}
