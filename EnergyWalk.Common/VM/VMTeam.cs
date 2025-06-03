using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyWalk.Common.DTO;

namespace EnergyWalk.Common.VM
{
    public class VMTeam
    {

        public VMTeam()
        {
            Team = new Team();
            lstTeamDetail = new List<TeamDetail>();
        }

        public Team  Team { get; set; }

        public List<TeamDetail> lstTeamDetail { get; set; }
    }
}
