using CompanyManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModels
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name can't be empty!")]
        [RegularExpression("^[A-Z]([A-Za-z])+\\s[A-Z]([A-Za-z])+$", ErrorMessage = "Must contains First name (at least 2 characters), space and Last name (at least 2 characters).")]
        [StringLength(50, ErrorMessage = "Name is too long!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email can't be empty!")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w\\+]*[0-9a-zA-Z\\+])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email incorrect!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number can't be empty!")]
        [RegularExpression("^\\+?[1-9][0-9]{11}$", ErrorMessage = "Incorect type of phone number!")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role can't be empty!")]
        public string? Role { get; set; }

        public int DepartmentId { get; set; }

        public EmployeeModel() { }

        public EmployeeModel(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            Role = employee.Role;
            DepartmentId = employee.DepartmentId;
        }
    }
}
