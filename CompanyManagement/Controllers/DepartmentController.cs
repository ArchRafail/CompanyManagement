using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;

namespace CompanyManagement.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartmentController(IEmployeesRepository employeesRepository, IDepartmentsRepository departmentsRepository)
        {
            _employeesRepository = employeesRepository;
            _departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_departmentsRepository.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetDepartment(int id)
        {
            Department? department = _departmentsRepository.Get(id);
            if (department != null)
            {
                return View(department);
            }
            return View("Error", new ErrorViewModel()
            {
                Summary = "No department found",
                Description = "That department was not found. Check it id before making request."
            }
            );
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AddDepartment()
        {
            Department department = new Department();
            return View(department);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddDepartment(Department department)
        {
            ModelValidating(department);
            if (ModelState.IsValid)
            {
                _departmentsRepository.Add(department);
                return RedirectToAction("Index");
            }
            return RedirectToAction("AddDepartment");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Update(Department department)
        {
            ModelValidating(department);
            List<Department> departments = _departmentsRepository.GetAll();
            bool noSuchDepartment = true;
            foreach (var departmentFromRepository in departments)
            {
                if (departmentFromRepository.Id == department.Id)
                {
                    noSuchDepartment = false;
                    break;
                }
            }
            if (noSuchDepartment)
            {
                ModelState.AddModelError("Id", $"There is no department in the repository with id {department.Id}!");
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Department departmentUpdated = new Department();
                departmentUpdated.Id = department.Id;
                departmentUpdated.Name = department.Name;
                departmentUpdated.Manager = department.Manager;
                if (department.Employees != null && department.Employees.Count != 0)
                {
                    departmentUpdated.Employees = department.Employees;
                }
                _departmentsRepository.Update(departmentUpdated);
                return RedirectToAction("Index");
            }
            return RedirectToAction("GetDepartment", new { id = department.Id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _departmentsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [NonAction]
        private void ModelValidating(Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                ModelState.AddModelError("Name", "Name can't be empty!");
                return;
            }
            if (department.Name.Length < 2 || department.Name.Length > 50)
            {
                ModelState.AddModelError("Name", "Name is not respect to the length rule!");
                return;
            }
            if (string.IsNullOrEmpty(department.Manager))
            {
                ModelState.AddModelError("Manager", "Manager's name can't be empty!");
                return;
            }
            string managerPattern = @"^[A-Z]([A-Za-z])+\s[A-Z]([A-Za-z])+$";
            Regex managerRegex = new Regex(managerPattern);
            if (!managerRegex.IsMatch(department.Manager))
            {
                ModelState.AddModelError("Manager", "Must contains First name (at least 2 characters), space and Last name (at least 2 characters).");
                return;
            }
            if (department.Manager.Length > 50)
            {
                ModelState.AddModelError("Manager", "Manager's name is too long!");
                return;
            }
        }
    }
}
