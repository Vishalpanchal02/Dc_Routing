// UnitOfWork.cs
using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.Dto;
using Dc_Routing.Services.IRepositories;

namespace Dc_Routing.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IAttendanceRepository AttendanceRepository { get; }

        private readonly HRMS2Context _context;

        public UnitOfWork(
            HRMS2Context context,
            IEmployeeRepository employeeRepository,
            IAttendanceRepository attendanceRepository)
        {
            _context = context;
            EmployeeRepository = employeeRepository;
            AttendanceRepository = attendanceRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await EmployeeRepository.GetAllEmployeesAsync();
        }

        public async Task<TblDcemployee> LoginAsync(string ecode, string password)
        {
            return await EmployeeRepository.LoginAsync(ecode, password);
        }

        //public async Task<bool> UpdateEmployeeAsync(TblDcemployee employee)
        //{
        //    return await EmployeeRepository.UpdateEmployeeAsync(employee);
        //}

        public async Task<int> AddBulkEmployeesAsync(List<TblDcemployee> employees)
        {
            return await EmployeeRepository.AddBulkEmployeesAsync(employees);
        }

        //public Task<bool> UpdateEmployeeAsync(TblDcemployee employee)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}


