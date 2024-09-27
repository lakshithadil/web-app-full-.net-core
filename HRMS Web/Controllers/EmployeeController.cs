using AutoMapper;
using FINAP.HRMS.DotNetCore.WebApi.Business_Service.Contracts;
using FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.DTOs;
using FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Models.RequestModels;
using FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Models.ResponseModels;
using FINAP.HRMS.DotNetCore.WebApi.Models;
using FINAP.HRMS.DotNetCore.WebApi.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IEmployeeService employeeService,
            IMapper mapper,
            ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResponseModel> AddEmployee(EmployeeRequestModel employeeRequest)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeRequest);
                await _employeeService.AddEmployee(employee);

                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                _logger.LogInformation(LogMessages.EmployeeCreated);
                return AddResponseMessage(Response, LogMessages.EmployeeCreated, employeeDto, true, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ApiResponseModel> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();
                var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                return AddResponseMessage(Response, LogMessages.EmployeesRetrieved, employeeDtos, true, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{employeeId}")]
        public async Task<ApiResponseModel> GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    _logger.LogWarning(LogMessages.EmployeeNotFound);
                    return AddResponseMessage(Response, LogMessages.EmployeeNotFound, null, false, HttpStatusCode.NotFound);
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return AddResponseMessage(Response, LogMessages.EmployeeRetrieved, employeeDto, true, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{employeeId}")]
        public async Task<ApiResponseModel> UpdateEmployee(int employeeId, EmployeeRequestModel employeeRequest)
        {
            try
            {
                var existingEmployee = await _employeeService.GetEmployeeById(employeeId);
                if (existingEmployee == null)
                {
                    _logger.LogWarning(LogMessages.EmployeeNotFound);
                    return AddResponseMessage(Response, LogMessages.EmployeeNotFound, null, false, HttpStatusCode.NotFound);
                }

                var updatedEmployee = _mapper.Map<Employee>(employeeRequest);
                updatedEmployee.EmployeeId = employeeId; // Ensure the ID is maintained

                await _employeeService.UpdateEmployee(employeeId, updatedEmployee);

                _logger.LogInformation(LogMessages.EmployeeUpdated);
                return AddResponseMessage(Response, LogMessages.EmployeeUpdated, null, true, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{employeeId}")]
        public async Task<ApiResponseModel> DeleteEmployee(int employeeId)
        {
            try
            {
                var existingEmployee = await _employeeService.GetEmployeeById(employeeId);
                if (existingEmployee == null)
                {
                    _logger.LogWarning(LogMessages.EmployeeNotFound);
                    return AddResponseMessage(Response, LogMessages.EmployeeNotFound, null, false, HttpStatusCode.NotFound);
                }

                await _employeeService.DeleteEmployee(employeeId);

                _logger.LogInformation(LogMessages.EmployeeDeleted);
                return AddResponseMessage(Response, LogMessages.EmployeeDeleted, null, true, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }
    }
}
