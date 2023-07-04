using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace CompanyManagement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IDepartmentsRepository _departmentsRepository;

        public EmployeeController(IEmployeesRepository employeesRepository, IDepartmentsRepository departmentsRepository)
        {
            _employeesRepository = employeesRepository;
            _departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_employeesRepository.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetEmployee(int id)
        {
            Employee? employee = _employeesRepository.Get(id);
            if (employee != null)
            {
                var departments = _departmentsRepository.GetAll();
                List<DepartmentModel> departmentModels = departments.Select(dp => new DepartmentModel(dp.Id, dp.Name)).ToList();
                ChangeViewModel changeViewModel = new ChangeViewModel();
                changeViewModel.Employee = new EmployeeModel(employee);
                changeViewModel.DepartmentId = employee!.Department!.Id;
                changeViewModel.Departments = departmentModels;
                return View(changeViewModel);
            }
            return View("Error", new ErrorViewModel()
                {
                    Summary = "No employee found",
                    Description = "That employee was not found. Check it id before making request."
                }
            );
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AddEmployee()
        {
            var departments = _departmentsRepository.GetAll();
            List<DepartmentModel> departmentModels = departments.Select(dp => new DepartmentModel(dp.Id, dp.Name)).ToList();
            ChangeViewModel changeViewModel = new ChangeViewModel();
            changeViewModel.Departments = departmentModels;
            changeViewModel.Employee = new EmployeeModel();
            return View(changeViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddEmployee(ChangeViewModel viewModel)
        {
            ModelValidating(viewModel);
            if (ModelState.IsValid)
            {
                var employee = viewModel.Employee;
                var departments = _departmentsRepository.GetAll();
                if (departments != null & departments!.Count != 0)
                {
                    Department? department = null;
                    if (viewModel.DepartmentId != null)
                    {
                        employee!.DepartmentId = (int)viewModel.DepartmentId;
                        department = departments.Single(x => x.Id == viewModel.DepartmentId);
                    }
                    if (department == null)
                    {
                        return RedirectToAction("AddEmployee");
                    }

                    Employee employeeNew = new Employee();
                    employeeNew.Id = employee!.Id;
                    employeeNew.Name = employee!.Name;
                    employeeNew.Email = employee!.Email;
                    employeeNew.PhoneNumber = employee!.PhoneNumber;
                    employeeNew.DepartmentId = employee!.DepartmentId;
                    employeeNew.Role = "user";
                    employeeNew.Password = AccountController.CreateMD5("11111111");

                    _employeesRepository.Add(employeeNew);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("AddEmployee");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Update(ChangeViewModel viewModel)
        {
            ModelValidating(viewModel);
            List<Employee> employees = _employeesRepository.GetAll();
            bool noSuchEmployee = true;
            foreach (var employee in employees)
            {
                if (employee.Id == viewModel.Employee!.Id)
                {
                    noSuchEmployee = false;
                    break;
                }
            }
            if (noSuchEmployee)
            {
                ModelState.AddModelError("Id", $"There is no user in the repository with id {viewModel.Employee!.Id}!");
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var employee = viewModel.Employee;
                var departments = _departmentsRepository.GetAll();
                if (departments != null & departments!.Count != 0)
                {
                    Department? department = null;
                    if (viewModel.DepartmentId != null)
                    {
                        employee!.DepartmentId = (int)viewModel.DepartmentId;
                        department = departments.Single(x => x.Id == viewModel.DepartmentId);
                    }
                    if (department == null)
                    {
                        return RedirectToAction("GetEmployee", new { id = @employee!.Id });
                    }

                    Employee employeeUpdated = new Employee();
                    employeeUpdated.Id = employee!.Id;
                    employeeUpdated.Name = employee!.Name;
                    employeeUpdated.Email = employee!.Email;
                    employeeUpdated.PhoneNumber = employee!.PhoneNumber;
                    employeeUpdated.Role = employee!.Role;
                    employeeUpdated.DepartmentId = employee!.DepartmentId;
                    var employeePassword = _employeesRepository.Get(employee.Id)!.Password;
                    employeeUpdated.Password = employeePassword;

                    _employeesRepository.Update(employeeUpdated);

                    if (employeeUpdated.Id == int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                    {
                        AccountController.CreatingClaims(employeeUpdated, HttpContext);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("GetEmployee", new { id = viewModel.Employee!.Id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _employeesRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [NonAction]
        private void ModelValidating(ChangeViewModel viewModel)
        {
            if (viewModel == null || viewModel.Employee == null || string.IsNullOrEmpty(viewModel.Employee.Name))
            {
                ModelState.AddModelError("Name", "Name can't be empty!");
                return;
            }
            string namePattern = @"^[A-Z]([A-Za-z])+\s[A-Z]([A-Za-z])+$";
            Regex nameRegex = new Regex(namePattern);
            if (!nameRegex.IsMatch(viewModel.Employee.Name))
            {
                ModelState.AddModelError("Name", "Must contains First name (at least 2 characters), space and Last name (at least 2 characters).");
                return;
            }
            if (viewModel.Employee.Name.Length > 50)
            {
                ModelState.AddModelError("Name", "Name is too long!");
                return;
            }
            if (string.IsNullOrEmpty(viewModel.Employee.Email))
            {
                ModelState.AddModelError("Email", "Email can't be empty!");
                return;
            }
            string emailPattern = @"^([0-9a-zA-Z]([-.\w\+]*[0-9a-zA-Z\+])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            Regex emailRegex = new Regex(emailPattern);
            if (!emailRegex.IsMatch(viewModel.Employee.Email))
            {
                ModelState.AddModelError("Email", "Email incorrect!");
                return;
            }
            if (string.IsNullOrEmpty(viewModel.Employee.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number can't be empty!");
                return;
            }
            string phonePattern = @"^\+?[1-9][0-9]{11}$";
            Regex phoneRegex = new Regex(phonePattern);
            if (!phoneRegex.IsMatch(viewModel.Employee.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Incorect type of phone number!");
                return;
            }
            if (viewModel.DepartmentId == null || viewModel.DepartmentId == 0)
            {
                ModelState.AddModelError("DepartmentId", "Department can't be empty or zero!");
            }
            if (string.IsNullOrEmpty(viewModel.Employee.Role))
            {
                ModelState.AddModelError("Role", "Role can't be empty!");
            }
        }
    }
}
