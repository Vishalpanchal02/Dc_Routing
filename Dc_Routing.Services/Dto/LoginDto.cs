using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc_Routing.Services.Dto
{
    public class LoginDto
    {
        public string ECode { get; set; }
        public string PasswordHash { get; set; }
    }
}
