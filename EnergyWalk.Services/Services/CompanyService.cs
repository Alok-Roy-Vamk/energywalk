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
    public class CompanyService : ICompanyService
    {
            private readonly EnergyWalkDBContext _energyWalkDBContext;

            public CompanyService(EnergyWalkDBContext energyWalkDBContext)
            {
                _energyWalkDBContext = energyWalkDBContext ?? throw new ArgumentNullException(nameof(energyWalkDBContext));
            }

            public async Task<Company> CreateAsync(Company company)
            {
                if (company == null) throw new ArgumentNullException(nameof(company));
                _energyWalkDBContext.Companys.Add(company);
                await _energyWalkDBContext.SaveChangesAsync();
                return company;
            }

            public async Task<bool> DeleteAsync(int CompanyID)
            {
                var company = await _energyWalkDBContext.Companys
                    .FirstOrDefaultAsync(u => u.CompanyID == CompanyID);
                if (company == null)
                    return false;

                _energyWalkDBContext.Companys.Remove(company);
                await _energyWalkDBContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<Company>> GetAllAsync()
            {
                return await _energyWalkDBContext.Companys.ToListAsync();
            }

            public async Task<Company?> GetByIdAsync(int CompanyID)
            {
                return await _energyWalkDBContext.Companys
                    .FirstOrDefaultAsync(u => u.CompanyID == CompanyID);
            }

            public async Task<Company> UpdateAsync(Company company)
            {
                if (company == null) throw new ArgumentNullException(nameof(company));
                var existingcompany = await _energyWalkDBContext.Companys
                    .FirstOrDefaultAsync(u => u.CompanyID == company.CompanyID);
                if (existingcompany == null)
                    throw new InvalidOperationException("company not found.");                

                await _energyWalkDBContext.SaveChangesAsync();
                return existingcompany;
            }
    }

    public interface ICompanyService
    {
        Task<Company> CreateAsync(Company company);
        Task<Company?> GetByIdAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
    }


}
