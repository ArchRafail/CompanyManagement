using CompanyManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModels
{
    public class ChangeViewModel
    {
        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "Department cant be empty!")]
        public int? DepartmentId { get; set; }
        public IEnumerable<DepartmentModel>? Departments { get; set; }
    }
}
