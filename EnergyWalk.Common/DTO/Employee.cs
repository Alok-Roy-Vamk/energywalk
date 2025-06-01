using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public int CompanyID { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }
    }
}
