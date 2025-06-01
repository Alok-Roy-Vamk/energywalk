using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class TaskInfo
    {
        [Key]
        public int TaskID { get; set; }

        public int ReferenceID { get; set; }

        public int ReferenceType { get; set; }

        public string CreatedDate { get; set; }

        public int CompanyID { get; set; }

        public string TaskDescription { get; set; }

        public int AssingTo { get; set; }
        public int Status { get; set; }
    }
}
