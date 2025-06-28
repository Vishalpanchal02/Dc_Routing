using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.Dto;
using Dc_Routing.Services.IRepositories;
using Dc_Routing.Services.Repositories;
//using Dc_Routing.UnityWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dc_Routing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dc_RoutingController : ControllerBase
    {


        private readonly IUnitOfWork _uw;

        public Dc_RoutingController(IUnitOfWork unitOfWork)
        {
            _uw = unitOfWork;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _uw.LoginAsync(model.ECode, model.PasswordHash);

            if (user != null)
            {
                return Ok("ok");
            }

            return Unauthorized("invalid credentials");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _uw.GetAllEmployeesAsync();
            if (employees == null || !employees.Any())
                return NotFound("No employees found.");

            return Ok(employees);
        }


        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto dto)
        {
            if (dto == null || dto.ECode == null)
                return BadRequest("Invalid employee data");

            // Map DTO to EF model
            var employee = new Employee
            {
                Ecode = dto.ECode,
                EmpName = dto.EmpName,
                EmailId = dto.EmailID,
                IsContract = dto.IsContract,
                Designation = dto.Designation,
                Department = dto.Department,
                SubDepartment = dto.SubDepartment,
                ReportingManagerName = dto.ReportingManagerName,
                RptPersonCard = dto.RptPersonCard,
                ActInAct = dto.ActInAct,
                PasswordHash = dto.PasswordHash
            };

            var result = await _uw.UpdateEmployeeAsync(employee);

            if (!result)
                return NotFound("Employee not found");

            return Ok("Employee updated successfully");
        }












        //[HttpGet]
        //public IEnumerable<string> Get()
        //{

        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<Dc_RoutingController>/5


        //// POST api/<Dc_RoutingController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<Dc_RoutingController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Dc_RoutingController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
