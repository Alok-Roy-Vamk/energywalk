using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class EnergySavingGoal
    {
        [Key]
        public int EnergySavingGoalID { get; set; }

        public DateTime SetDate { get; set; }

        public string GoalDescription { get; set; }

    }
}
