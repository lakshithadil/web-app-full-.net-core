using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using FINAP.HRMS.DotNetCore.WebApi.Business_Service;
using FINAP.HRMS.DotNetCore.WebApi.Business_Service.Contracts;
using FINAP.HRMS.DotNetCore.WebApi.Repository;
using FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts;
using FINAP.HRMS.DotNetCore.WebApi.Shared.AutoMappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Allowed origins
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins(allowedOrigins) // Allow requests from this origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Allow cookies
    });
});

// Database connection
var connectionString = builder.Configuration.GetConnectionString("LocalSqlServerConnection");

// Register a database connection string as a singleton service
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

// Register AutoMapper and create a mapping configuration
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Configure and add a logger 
builder.Services.AddLogging(loggingBuilder =>
{

    loggingBuilder.AddConsole(); // You can add other logging providers here

});


// Register repositories or services 
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS middleware
app.UseCors("AllowSpecificOrigin");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
