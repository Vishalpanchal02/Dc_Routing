using Dc_Routing.Services.IRepositories;
using Dc_Routing.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Dc_Routing.Data.EFModels;
using OfficeOpenXml;



var builder = WebApplication.CreateBuilder(args);

// ✅ Set EPPlus license globally (non-commercial use)


// ✅ Register DbContext
builder.Services.AddDbContext<HRMS2Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ✅ Register Repositories and Unit of Work
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ✅ Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
