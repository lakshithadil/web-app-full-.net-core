using FINAP.HRMS.DotNetCore.WebApi.Models;

namespace FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int employeeId);
        Task UpdateEmployee(int employeeId, Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
