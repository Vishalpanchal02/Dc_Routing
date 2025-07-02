using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Dc_Routing.Services.Dto
{
    public class EmployeeDto
    {
        public string ECode { get; set; }

        public string EmpName { get; set; }

        public string EmailId { get; set; }

        public bool? IsContract { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string SubDepartment { get; set; }

        public string ReportingManagerName { get; set; }

        public string RptPersonCard { get; set; }

        public string ActInAct { get; set; }
        public string HashPassword { get; set; }
    }
}
