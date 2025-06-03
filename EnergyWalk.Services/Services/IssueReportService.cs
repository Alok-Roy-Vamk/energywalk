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
    public class IssueReportService : IIssueReportService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public IssueReportService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<IssueReport> CreateAsync(IssueReport user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                _energyWalkDBContext.IssueReports.Add(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAsync(int IssueReportID)
            {
                var user = await _energyWalkDBContext.IssueReports
                    .FirstOrDefaultAsync(u => u.IssueReportID == IssueReportID);
                if (user == null)
                    return false;

                _energyWalkDBContext.IssueReports.Remove(user);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<IssueReport>> GetAllAsync()
            {
                return await _energyWalkDBContext.IssueReports.ToListAsync();
            }

            public async Task<IssueReport?> GetByIdAsync(int IssueReportID)
            {
                return await _energyWalkDBContext.IssueReports
                    .FirstOrDefaultAsync(u => u.IssueReportID == IssueReportID);
            }

            public async Task<IssueReport> UpdateAsync(IssueReport user)
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                var existingUser = await _energyWalkDBContext.IssueReports
                    .FirstOrDefaultAsync(u => u.IssueReportID == user.IssueReportID);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                await _energyWalkDBContext.SaveChangesAsync();
                return existingUser;
            }
    }

    public interface IIssueReportService
    {
        Task<IssueReport> CreateAsync(IssueReport user);
        Task<IssueReport?> GetByIdAsync(int id);
        Task<IEnumerable<IssueReport>> GetAllAsync();
        Task<IssueReport> UpdateAsync(IssueReport user);
        Task<bool> DeleteAsync(int id);
    }


}
