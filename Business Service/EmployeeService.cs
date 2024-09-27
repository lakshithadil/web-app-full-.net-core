using FINAP.HRMS.DotNetCore.WebApi.Business_Service.Contracts;
using FINAP.HRMS.DotNetCore.WebApi.Models;
using FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts;

namespace FINAP.HRMS.DotNetCore.WebApi.Business_Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Add new employee
        public async Task AddEmployee(Employee employee)
        {
            await _employeeRepository.AddEmployee(employee);
        }

        // Get all employees
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }

        // Get employee by ID
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await _employeeRepository.GetEmployeeById(employeeId);
        }

        // Update employee
        public async Task UpdateEmployee(int employeeId, Employee employee)
        {
            await _employeeRepository.UpdateEmployee(employeeId, employee);
        }

        // Delete employee
        public async Task DeleteEmployee(int employeeId)
        {
            await _employeeRepository.DeleteEmployee(employeeId);
        }
    }
}

