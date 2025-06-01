using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class Consumption
    {
        [Key]
        public int ConsumptionID { get; set; }

        public int CompanyID { get; set; }

        public int RecordedDate { get; set; }

        public decimal ConsumptionUnit { get; set; }
    }
}
