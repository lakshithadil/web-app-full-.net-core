using FINAP.HRMS.DotNetCore.WebApi.Business_Service.Contracts;
using FINAP.HRMS.DotNetCore.WebApi.Models;
using FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts;

namespace FINAP.HRMS.DotNetCore.WebApi.Business_Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // Add new department
        public async Task AddDepartment(Department department)
        {
            await _departmentRepository.AddDepartment(department);
        }

        // Get all departments
        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _departmentRepository.GetAllDepartments();
        }

        // Get department by ID
        public async Task<Department> GetDepartmentById(int departmentId)
        {
            return await _departmentRepository.GetDepartmentById(departmentId);
        }

        // Update department
        public async Task UpdateDepartment(int departmentId, Department department)
        {
            await _departmentRepository.UpdateDepartment(departmentId, department);
        }

        // Delete department
        public async Task DeleteDepartment(int departmentId)
        {
            await _departmentRepository.DeleteDepartment(departmentId);
        }
    }
}
