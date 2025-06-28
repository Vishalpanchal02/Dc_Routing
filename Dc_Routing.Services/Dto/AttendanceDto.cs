using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc_Routing.Services.Dto
{
     public class AttendanceDto
    {
        public int DcAttendanceId { get; set; }
        public string ECode { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public DateTime SubmitOn { get; set; }
    }
}
