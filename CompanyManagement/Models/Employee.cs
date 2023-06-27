using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagement.Models
{
    public class Employee
    {
        [Key]
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

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}