using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.Dto;

namespace Dc_Routing.Services.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<TblDcemployee> LoginAsync(string ecode , string password);
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
            Task<bool> UpsertEmployeeAsync(TblDcemployee employee);
        Task<int> AddBulkEmployeesAsync(List<TblDcemployee> employees);
         
    }
}
