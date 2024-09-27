using FINAP.HRMS.DotNetCore.WebApi.Models;

namespace FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts
{
    public interface IDepartmentRepository
    {
        Task AddDepartment(Department department); 
        Task<IEnumerable<Department>> GetAllDepartments(); 
        Task<Department> GetDepartmentById(int departmentId); 
        Task UpdateDepartment(int departmentId, Department department); 
        Task DeleteDepartment(int departmentId);
    }
}
