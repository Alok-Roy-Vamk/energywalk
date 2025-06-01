using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class TaskAndTeamAssignment
    {
        [Key]
        public int TaskAndTeamAssignmentID { get; set; }

        public int TaskID { get; set; }
        public int TeamID { get; set; }

        public int Status { get; set; }
    }
}
