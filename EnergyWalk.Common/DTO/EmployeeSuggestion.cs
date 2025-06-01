using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class EmployeeSuggestion
    {
        [Key]
        public int EmployeeSuggestionID { get; set; }

        public DateTime CreatedDate { get; set; }
        public int EmployeeID { get; set; }

        public string Suggestion { get; set; }
    }
}
