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
    public class SystemUserService : ISystemUserService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public SystemUserService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<SystemUser> CreateAsync(SystemUser user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                _energyWalkDBContext.SystemUsers.Add(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAsync(int systemUserID)
            {
                var user = await _energyWalkDBContext.SystemUsers
                    .FirstOrDefaultAsync(u => u.SystemUserID == systemUserID);
                if (user == null)
                    return false;

                _energyWalkDBContext.SystemUsers.Remove(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<SystemUser>> GetAllAsync()
            {
                return await _energyWalkDBContext.SystemUsers.ToListAsync();
            }

            public async Task<SystemUser?> GetByIdAsync(int systemUSerID)
            {
                return await _energyWalkDBContext.SystemUsers
                    .FirstOrDefaultAsync(u => u.SystemUserID == systemUSerID);
            }

            public async Task<SystemUser> UpdateAsync(SystemUser user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                var existingUser = await _energyWalkDBContext.SystemUsers
                    .FirstOrDefaultAsync(u => u.SystemUserID == user.SystemUserID);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                existingUser.CompanyID = user.CompanyID;
                existingUser.UserID = user.UserID;
                existingUser.Password = user.Password;

                await _energyWalkDBContext.SaveChangesAsync();
                return existingUser;
            }
    }

    public interface ISystemUserService
    {
        Task<SystemUser> CreateAsync(SystemUser user);
        Task<SystemUser?> GetByIdAsync(int id);
        Task<IEnumerable<SystemUser>> GetAllAsync();
        Task<SystemUser> UpdateAsync(SystemUser user);
        Task<bool> DeleteAsync(int id);
    }


}
