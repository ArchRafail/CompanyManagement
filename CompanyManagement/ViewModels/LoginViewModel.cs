using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email cant be empty!")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w\\+]*[0-9a-zA-Z\\+])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email incorrect!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password cant be empty!")]
        public string? Password { get; set; }

        public string? Message { get; set; }
    }
}
