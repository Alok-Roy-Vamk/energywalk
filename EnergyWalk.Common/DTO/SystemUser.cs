using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class SystemUser
    {
        [Key]
        public int SystemUserID { get; set; }

        public int CompanyID { get; set; }
        public string UserID { get; set; }

        public string  Password { get; set; }
    }
}
