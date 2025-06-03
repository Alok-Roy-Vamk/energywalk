using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyWalk.Common.DTO;
using Microsoft.EntityFrameworkCore;

namespace EnergyWalk.DataAccess
{
    public class EnergyWalkDBContext : DbContext
    {

        public EnergyWalkDBContext(DbContextOptions<EnergyWalkDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here
            // Example: modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Company> Companys { get; set; }

        public DbSet<EmployeeSuggestion> EmployeeSuggestions { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EnergySavingGoal> EnergySavingGoals { get; set; }

        public DbSet<IssueReport> IssueReports { get; set; }

        public DbSet<TaskAndTeamAssignment> TaskAndTeamAssignments { get; set; }

        public DbSet<TaskInfo> TaskInfos { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamDetail> TeamDetails { get; set; }



    }
}
