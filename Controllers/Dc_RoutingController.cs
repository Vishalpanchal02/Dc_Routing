using System.ComponentModel;
using Dc_Routing.Data.EFModels;
using Dc_Routing.Services.Dto;
using Dc_Routing.Services.IRepositories;
using Dc_Routing.Services.Repositories;
//using Dc_Routing.UnityWork;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

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


        //[HttpPut("update-employee")]
        //public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto dto)
        //{
        //    if (dto == null || dto.ECode == null)
        //        return BadRequest("Invalid employee data");

        //    // Map DTO to EF model
        //    var employee = new TblDcemployee
        //    {
        //        ECode = dto.ECode,
        //        EmpName = dto.EmpName,
        //        EmailId = dto.EmailId,
        //        IsContract = dto.IsContract,
        //        Designation = dto.Designation,
        //        Department = dto.Department,
        //        SubDepartment = dto.SubDepartment,
        //        ReportingManagerName = dto.ReportingManagerName,
        //        RptPersonCard = dto.RptPersonCard,
        //        ActInAct = dto.ActInAct,
        //        PasswordHash = dto.HashPassword
        //    };

        //    var result = await _uw.UpdateEmployeeAsync(employee);

        //    if (!result)
        //        return NotFound("Employee not found");

        //    return Ok("Employee updated successfully");
        //}

        [HttpPut("upsert-employee")]
        public async Task<IActionResult> UpsertEmployee([FromBody] TblDcemployee dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.ECode))
                return BadRequest("Invalid employee data");

            var success = await _uw.EmployeeRepository.UpsertEmployeeAsync(dto);

            if (!success)
                return StatusCode(500, "Failed to insert/update employee");

            int rowsAffected = await _uw.SaveChangesAsync();

            if (rowsAffected == 0)
                return StatusCode(500, "No changes were saved to the database");

            return Ok("Employee upserted successfully.");
        }

        [HttpPost("upload-employees")]
        public async Task<IActionResult> UploadEmployees(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var employees = new List<TblDcemployee>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                // ✅ Correct way to set license in EPPlus 8 (non-commercial use only)
               
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                        return BadRequest("No worksheet found.");

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        bool.TryParse(worksheet.Cells[row, 4].Text?.Trim(), out bool isContract);
                        bool.TryParse(worksheet.Cells[row, 10].Text?.Trim(), out bool actInAct);

                        employees.Add(new TblDcemployee
                        {
                            ECode = worksheet.Cells[row, 1].Text?.Trim(),
                            EmpName = worksheet.Cells[row, 2].Text?.Trim(),
                            EmailId = worksheet.Cells[row, 3].Text?.Trim(),
                            IsContract = isContract,
                            Designation = worksheet.Cells[row, 5].Text?.Trim(),
                            Department = worksheet.Cells[row, 6].Text?.Trim(),
                            SubDepartment = worksheet.Cells[row, 7].Text?.Trim(),
                            ReportingManagerName = worksheet.Cells[row, 8].Text?.Trim(),
                            RptPersonCard = worksheet.Cells[row, 9].Text?.Trim(),
                            ActInAct = worksheet.Cells[row, 10].Text?.Trim(),
                            PasswordHash = worksheet.Cells[row, 11].Text?.Trim()
                        });
                    }
                }
            }

            if (employees.Count == 0)
                return BadRequest("No valid employees found in the file.");

            //var result = _uw.EmployeeRepository.AddBulkEmployeesAsync(employees);
                       
            //return Ok($"{result} employees uploaded successfully.");

            await _uw.EmployeeRepository.AddBulkEmployeesAsync(employees);

            // ✅ Commit to DB
            await _uw.SaveChangesAsync();

            return Ok($"{employees.Count} employees uploaded successfully.");
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
