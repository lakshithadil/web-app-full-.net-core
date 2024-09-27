using FINAP.HRMS.DotNetCore.WebApi.Models;
using FINAP.HRMS.DotNetCore.WebApi.Repository.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace FINAP.HRMS.DotNetCore.WebApi.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly SqlConnection _sqlConnection; 

        public DepartmentRepository(IDbConnection dbConnection)
        {
            _sqlConnection = dbConnection as SqlConnection
                ?? throw new InvalidOperationException("The provided connection is not a SqlConnection.");
        }

        // Add a new department
        public async Task AddDepartment(Department department)
        {
            try
            {
                using (var cmd = new SqlCommand("AddDepartment", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    cmd.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);

                    await _sqlConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception )
            {
                throw;
            }
           
        }

        // Get all departments
        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var departments = new List<Department>();

            try
            {
                using (var cmd = new SqlCommand("GetAllDepartments", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await _sqlConnection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            departments.Add(new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1),
                                DepartmentCode = reader.GetString(2)
                            });
                        }
                    }
                    await _sqlConnection.CloseAsync();
                }

                return departments;
            }
            catch (Exception )
            {
                throw;
            }
            
        }

        // Get a department by ID
        public async Task<Department> GetDepartmentById(int departmentId)
        {
            Department department = null;

            try
            {
                using (var cmd = new SqlCommand("GetDepartmentById", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    await _sqlConnection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            department = new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1),
                                DepartmentCode = reader.GetString(2)
                            };
                        }
                    }
                    await _sqlConnection.CloseAsync();
                }
                
                return department;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // Update a department
        public async Task UpdateDepartment(int departmentId, Department department)
        {
            try
            {
                using (var cmd = new SqlCommand("UpdateDepartment", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    cmd.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);

                    await _sqlConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception )
            {
                throw;
            }
            
        }

        // Delete a department
        public async Task DeleteDepartment(int departmentId)
        {
            try
            {
                using (var cmd = new SqlCommand("DeleteDepartment", _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    await _sqlConnection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception )
            {
                throw;
            }
            
        }
    }
    
}
