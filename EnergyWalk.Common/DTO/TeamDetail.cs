using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class TeamDetail
    {
        [key]
        public int TeamDetailID { get; set; }

        public int TeamID { get; set; }

        public int TeamMemberID { get; set; }

        public int Responsibility { get; set; }

        public int Status { get; set; }
    }
}
