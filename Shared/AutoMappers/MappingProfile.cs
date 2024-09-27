using AutoMapper;
using FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.DTOs;
using FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Models.RequestModels;
using FINAP.HRMS.DotNetCore.WebApi.Models;

namespace FINAP.HRMS.DotNetCore.WebApi.Shared.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentRequestModel, Department>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<EmployeeRequestModel, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
