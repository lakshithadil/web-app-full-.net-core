using FINAP.HRMS.DotNetCore.WebApi.Models;
using FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace FINAP.HRMS.DotNetCore.WebApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection _sqlConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _sqlConnection = dbConnection as SqlConnection
                ?? throw new InvalidOperationException("The provided connection is not a SqlConnection.");
        }

        // Add a new employee
        public async Task AddEmployee(Employee employee)
        {
            try
            {
                using (var cmd = new SqlCommand("AddEmployee", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                    await _sqlConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get all employees
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = new List<Employee>();

            try
            {
                using (var cmd = new SqlCommand("GetAllEmployees", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await _sqlConnection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                EmailAddress = reader.GetString(3),
                                DateOfBirth = reader.GetDateTime(4),
                                Age = reader.GetInt32(5),
                                Salary = reader.GetDecimal(6),
                                Department = reader.GetString(7)
                            });
                        }
                    }
                    await _sqlConnection.CloseAsync();
                }

                return employees;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get an employee by ID
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            Employee employee = null;

            try
            {
                using (var cmd = new SqlCommand("GetEmployeeById", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    await _sqlConnection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                EmailAddress = reader.GetString(3),
                                DateOfBirth = reader.GetDateTime(4),
                                Age = reader.GetInt32(5),
                                Salary = reader.GetDecimal(6),
                                DepartmentId = reader.GetInt32(7),
                                Department = reader.GetString(8)
                            };
                        }
                    }
                    await _sqlConnection.CloseAsync();
                }

                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update an employee
        public async Task UpdateEmployee(int employeeId, Employee employee)
        {
            try
            {
                using (var cmd = new SqlCommand("UpdateEmployee", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                    await _sqlConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete an employee
        public async Task DeleteEmployee(int employeeId)
        {
            try
            {
                using (var cmd = new SqlCommand("DeleteEmployee", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    await _sqlConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
