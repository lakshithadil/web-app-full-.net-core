namespace FINAP.HRMS.DotNetCore.WebApi.HRMS_Web.Models.RequestModels
{
    public class EmployeeRequestModel
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
