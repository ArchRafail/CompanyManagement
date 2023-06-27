using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name can't be empty!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name is not respect to the length rule!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Manager's name field can't be empty!")]
        [RegularExpression("^[A-Z]([A-Za-z])+\\s[A-Z]([A-Za-z])+$", ErrorMessage = "Must contains First name (at least 2 characters), space and Last name (at least 2 characters).")]
        [StringLength(50, ErrorMessage = "Manager's name is too long!")]
        public string? Manager { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
