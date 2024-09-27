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
    [Route("api/departments")]
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(
            IDepartmentService departmentService,
            IMapper mapper,
            ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ApiResponseModel> AddDepartment(DepartmentRequestModel departmentRequest)
        {
            try
            {
                var department = _mapper.Map<Department>(departmentRequest);
                await _departmentService.AddDepartment(department);
               
                var departmentDto = _mapper.Map<DepartmentDto>(department);
                
                _logger.LogInformation(LogMessages.DepartmentCreated);
                AddResponseMessage(Response, LogMessages.DepartmentCreated, departmentDto, true, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
            return Response;
        }


        [HttpGet]
        public async Task<ApiResponseModel> GetAll()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartments();
                var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
                AddResponseMessage(Response, LogMessages.DepartmentsRetrieved, departmentDtos, true, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
            return Response;
        }


        [HttpGet("{departmentId}")]
        public async Task<ApiResponseModel> GetDepartmentById(int departmentId)
        {
            try
            {
                var department = await _departmentService.GetDepartmentById(departmentId);
                if (department == null)
                {
                    AddResponseMessage(Response, LogMessages.DepartmentNotFound, null, false, HttpStatusCode.NotFound);
                    return Response;
                }

                var departmentDto = _mapper.Map<DepartmentDto>(department);
                AddResponseMessage(Response, LogMessages.DepartmentRetrieved, departmentDto, true, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
            return Response;
        }



        [HttpPut("{departmentId}")]
        public async Task<ApiResponseModel> UpdateDepartment(int departmentId, DepartmentRequestModel departmentRequest)
        {
            try
            {
                var existingDepartment = await _departmentService.GetDepartmentById(departmentId);
                if (existingDepartment == null)
                {
                    _logger.LogWarning(LogMessages.DepartmentNotFound);
                    return AddResponseMessage(Response, LogMessages.DepartmentNotFound, null, true, HttpStatusCode.NotFound);
                }

                var updatedDepartment = _mapper.Map<Department>(departmentRequest);
                updatedDepartment.DepartmentId = departmentId; // Ensure the ID is not changed

                await _departmentService.UpdateDepartment(existingDepartment.DepartmentId, updatedDepartment);

                _logger.LogInformation(LogMessages.DepartmentUpdated);
                return AddResponseMessage(Response, LogMessages.DepartmentUpdated, null, true, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{departmentId}")]
        public async Task<ApiResponseModel> DeleteDepartment(int departmentId)
        {
            try
            {
                var existingDepartment = await _departmentService.GetDepartmentById(departmentId);
                if (existingDepartment == null)
                {
                    _logger.LogWarning(LogMessages.DepartmentNotFound);
                    return AddResponseMessage(Response, LogMessages.DepartmentNotFound, null, true, HttpStatusCode.NotFound);
                }

                await _departmentService.DeleteDepartment(departmentId);

                _logger.LogInformation(LogMessages.DepartmentDeleted);
                return AddResponseMessage(Response, LogMessages.DepartmentDeleted, null, true, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.InternalServerError);
                return AddResponseMessage(Response, ex.Message, null, false, HttpStatusCode.InternalServerError);
            }
        }

    }
}
