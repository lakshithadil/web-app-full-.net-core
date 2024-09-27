using FINAP.HRMS.DotNetCore.WebApi.Models;

namespace FINAP.HRMS.DotNetCore.WebApi.Business_Service.Contracts
{
    public interface IEmployeeService
    {
        Task AddEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int employeeId);
        Task UpdateEmployee(int employeeId, Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
