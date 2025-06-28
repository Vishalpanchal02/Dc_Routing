using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.IRepositories;

namespace Dc_Routing.Services.Repositories
{


    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HRMS2Context _Context;
        public AttendanceRepository(HRMS2Context Context) { 
            _Context = Context;
        }
      
    }
}
