using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.Dto;
using Dc_Routing.Services.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Dc_Routing.Services.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRMS2Context _context ;

        public EmployeeRepository(HRMS2Context context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _context.Employees
              .Select(emp => new EmployeeDto
              {
                  ECode = emp.Ecode,
                  EmpName = emp.EmpName,
                  EmailID = emp.EmailId,
                  IsContract = emp.IsContract,
                  Designation = emp.Designation,
                  Department = emp.Department,
                  SubDepartment = emp.SubDepartment,
                  ReportingManagerName = emp.ReportingManagerName,
                  RptPersonCard = emp.RptPersonCard,
                  ActInAct = emp.ActInAct
              })
              .ToListAsync();
        }

      

    public async Task<Employee> LoginAsync(string ecode, string password)
        {
           
            var employee = await _context.Employees
                .FirstOrDefaultAsync(u => u.Ecode==ecode && u.PasswordHash == password);

            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var existing = await _context.Employees.FirstOrDefaultAsync(e => e.Ecode == employee.Ecode);

            if (existing == null)
                return false;

            existing.EmpName = employee.EmpName;
            existing.EmailId = employee.EmailId;
            existing.IsContract = employee.IsContract;
            existing.Designation = employee.Designation;
            existing.Department = employee.Department;
            existing.SubDepartment = employee.SubDepartment;
            existing.ReportingManagerName = employee.ReportingManagerName;
            existing.RptPersonCard = employee.RptPersonCard;
            existing.ActInAct = employee.ActInAct;
            existing.PasswordHash = employee.PasswordHash;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
