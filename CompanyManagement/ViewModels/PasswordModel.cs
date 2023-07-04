using CompanyManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModels
{
    public class PasswordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Password cant be empty!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password has to have length between 8 and 20 symbols.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password cant be empty!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password has to have length between 8 and 20 symbols.")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public string? Message { get; set; }

        public PasswordModel() { }

        public PasswordModel(Employee employee)
        {
            Id = employee.Id;
            Password = "";
            ConfirmPassword = "";
        }
    }
}
