using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Model.Models.RegistrationModels
{
    public class Registrationmodel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class RegistrationViewmodel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class LoginModel()
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseModel
    {
        public ErrorModel ErrorModel { get; set; } = new();
        public EmployeeModel Employee { get; set; } = new();
        public string? JWTToken { get; set; }
        public int UserId { get; set; }
    }
    public class ErrorModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; } = string.Empty;
    }
    public class EmployeeModel  
    {
        public long UserId { get; set; }
        public string? Designation { get; set; }
        public string? FirstName { get; set; }
        public string? ProfilePic { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long RoleId { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserRolesJSON { get; set; } = string.Empty;
        public long AgencyId { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Dob { get; set; }
        public string? AgencySubscriptionModules { get; set; }
        public bool? IsFirstTimeLogin { get; set; }
    }

}
