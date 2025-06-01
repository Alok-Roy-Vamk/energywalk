using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        public int CompanyID { get; set; }
        public int TeamLead { get; set; }

        public string Description { get; set; }
    }
}
