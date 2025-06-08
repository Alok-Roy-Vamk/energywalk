using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyWalk.Common.DTO;
using EnergyWalk.Common.VM;
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

        public async Task<VMTeam> CreateAsync(VMTeam vmTeam)
        {
            if (vmTeam.Team.TeamID > 0)
            {
                _energyWalkDBContext.Teams.Attach(vmTeam.Team);
                _energyWalkDBContext.Entry(vmTeam.Team).State = EntityState.Modified;
                await _energyWalkDBContext.SaveChangesAsync();
            }
            else
            {
                _energyWalkDBContext.Teams.Add(vmTeam.Team);                
                await _energyWalkDBContext.SaveChangesAsync();
            }
            return vmTeam;
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

    }

    public interface ITeamService
    {
        Task<VMTeam> CreateAsync(VMTeam vmTeam);
        Task<Team?> GetByIdAsync(int id);
        Task<IEnumerable<Team>> GetAllAsync();      
        Task<bool> DeleteAsync(int id);
    }


}
