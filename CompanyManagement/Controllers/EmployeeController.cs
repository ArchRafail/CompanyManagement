using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CompanyManagement.Controllers
{
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
        public IActionResult GetEmployee(int id)
        {
            Employee? employee = _employeesRepository.Get(id);
            if (employee != null)
            {
                var departments = _departmentsRepository.GetAll();
                List<DepartmentModel> departmentModels = departments.Select(dp => new DepartmentModel(dp.Id, dp.Name)).ToList();
                ChangeViewModel changeViewModel = new ChangeViewModel();
                changeViewModel.Employee = employee;
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
        public IActionResult AddEmployee()
        {
            var departments = _departmentsRepository.GetAll();
            List<DepartmentModel> departmentModels = departments.Select(dp => new DepartmentModel(dp.Id, dp.Name)).ToList();
            ChangeViewModel changeViewModel = new ChangeViewModel();
            changeViewModel.Departments = departmentModels;
            changeViewModel.Employee = new Employee();
            return View(changeViewModel);
        }

        [HttpPost]
        public IActionResult AddEmployee(ChangeViewModel viewModel)
        {
            ModelValidating(viewModel);
            if (ModelState.IsValid)
            {
                var employee = viewModel.Employee;
                var departments = _departmentsRepository.GetAll();
                if (departments != null & departments!.Count != 0)
                {
                    employee!.Department = departments.Single(x => x.Id == viewModel.DepartmentId);
                    if (employee.Department == null)
                    {
                        return RedirectToAction("AddEmployee");
                    }
                    _employeesRepository.Add(employee);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("AddEmployee");
        }

        [HttpPost]
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
                    employee!.Department = departments.Single(x => x.Id == viewModel.DepartmentId);
                    if (employee.Department == null)
                    {
                        return RedirectToAction("GetEmployee", new { id = @employee.Id });
                    }
                    _employeesRepository.Update(employee);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("GetEmployee", new { id = viewModel.Employee!.Id });
        }

        [HttpPost]
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
                ModelState.AddModelError("DepartmentId", "Department cant be empty or zero!");
            }
        }
    }
}
