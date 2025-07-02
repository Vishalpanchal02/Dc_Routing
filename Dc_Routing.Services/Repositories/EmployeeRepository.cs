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

        public async Task<int> AddBulkEmployeesAsync(List<TblDcemployee> employees)
        {
       
            _context.TblDcemployees.AddRange(employees);
            return await _context.SaveChangesAsync(); // returns count of inserted rows
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _context.TblDcemployees
              .Select(emp => new EmployeeDto
              {
                  ECode = emp.ECode,
                  EmpName = emp.EmpName,
                  EmailId = emp.EmailId,
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

      

    public async Task<TblDcemployee> LoginAsync(string ecode, string password)
        {
           
            var employee = await _context.TblDcemployees
                .FirstOrDefaultAsync(u => u.ECode==ecode && u.PasswordHash == password);

            return employee;
        }

        //public async Task<bool> UpdateEmployeeAsync(TblDcemployee employee)
        //{
        //    var existing = await _context.TblDcemployees
        //        .FirstOrDefaultAsync(e => e.ECode == employee.ECode);

        //    if (existing != null)
        //    {
        //        // ✅ Update existing employee
        //        existing.EmpName = employee.EmpName;
        //        existing.EmailId = employee.EmailId;
        //        existing.IsContract = employee.IsContract;
        //        existing.Designation = employee.Designation;
        //        existing.Department = employee.Department;
        //        existing.SubDepartment = employee.SubDepartment;
        //        existing.ReportingManagerName = employee.ReportingManagerName;
        //        existing.RptPersonCard = employee.RptPersonCard;
        //        existing.ActInAct = employee.ActInAct;
        //        existing.PasswordHash = employee.PasswordHash;

        //        _context.TblDcemployees.Update(existing);
        //    }
        //    else
        //    {
        //        // ✅ Insert new employee
        //        await _context.TblDcemployees.AddAsync(employee);
        //    }

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> UpsertEmployeeAsync(TblDcemployee employee)
        {
            var existing = await _context.TblDcemployees
                .FirstOrDefaultAsync(e => e.ECode == employee.ECode);

            if (existing != null)
            {
                // Update
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

                _context.TblDcemployees.Update(existing);
            }
            else
            {
                // Insert
                await _context.TblDcemployees.AddAsync(employee);
            }

            return true; // Let UoW SaveChangesAsync
        }
    }
}
