
using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.Dto;

namespace Dc_Routing.Services.IRepositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<Employee> LoginAsync(string ecode, string password);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task SaveAsync();
    }
}
