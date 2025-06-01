using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyWalk.Common.DTO
{
    public class IssueReport
    {
        [Key]
        public int IssueReportID { get; set; }

        public int CompanyID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime ReportDate { get; set; }

        public string ReportDescription { get; set; }

        public string Attachment { get; set; }

        public int Status { get; set; }
    }
}
