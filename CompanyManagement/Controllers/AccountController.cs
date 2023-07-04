using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace CompanyManagement.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IEmployeesRepository employeesRepository;

        public AccountController(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Employee");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            ModelValidating(loginViewModel);
            if (ModelState.IsValid)
            {
                string passwordMD5 = CreateMD5(loginViewModel.Password!);
                var employees = employeesRepository.GetAll();
                Employee? employee = employees.FirstOrDefault(e => e.Email == loginViewModel.Email && e.Password == passwordMD5);
                if (employee != null)
                {
                    CreatingClaims(employee, HttpContext);
                    return RedirectToAction("Index", "Employee");
                }
                var specificLoginView = new LoginViewModel();
                specificLoginView.Message = "No such user into database";
                return View(specificLoginView);
            }
            var loginView = new LoginViewModel();
            loginView.Message = "Invalid email or password.";
            return View(loginView);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult PasswordChange()
        {
            if (HttpContext.User.Identity != null)
            {
                Employee? employee = null;
                var employees = employeesRepository.GetAll();
                var employeeId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                employee = employees.FirstOrDefault(e => e.Id == employeeId);
                if (employee != null)
                {
                    PasswordModel passwordModel = new PasswordModel(employee);
                    return View(passwordModel);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult PasswordChange(PasswordModel passwordModel)
        {
            PasswordValidating(passwordModel);
            if (ModelState.IsValid)
            {
                Employee? employee = null;
                if (HttpContext.User.Identity != null)
                {
                    var employees = employeesRepository.GetAll();
                    var employeeId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    employee = employees.FirstOrDefault(e => e.Id == employeeId);
                    if (employee != null && employeeId == passwordModel.Id)
                    {
                        Employee employeeUpdated = new Employee();
                        employeeUpdated.Id = employee.Id;
                        employeeUpdated.Name = employee.Name;
                        employeeUpdated.Email = employee.Email;
                        employeeUpdated.PhoneNumber = employee.PhoneNumber;
                        employeeUpdated.Role = employee.Role;

                        var newPassword = CreateMD5(passwordModel.Password!);
                        employeeUpdated.Password = newPassword;

                        employeeUpdated.DepartmentId = employee.DepartmentId;
                        employeesRepository.Update(employeeUpdated);
                        return RedirectToAction("Index", "Employee");
                    }
                    return RedirectToAction("Logout");
                }
            }
            var wrongPasswordMessage = new PasswordModel();
            wrongPasswordMessage.Id = passwordModel.Id;
            wrongPasswordMessage.Message = "Invalid password or confirm password.";
            return View(wrongPasswordMessage);
        }

        [NonAction]
        private void ModelValidating(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrEmpty(loginViewModel.Email))
            {
                ModelState.AddModelError("Email", "Email can't be empty!");
                return;
            }
            string emailPattern = @"^([0-9a-zA-Z]([-.\w\+]*[0-9a-zA-Z\+])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            Regex emailRegex = new Regex(emailPattern);
            if (!emailRegex.IsMatch(loginViewModel.Email))
            {
                ModelState.AddModelError("Email", "Email incorrect!");
                return;
            }
            if (string.IsNullOrEmpty(loginViewModel.Password))
            {
                ModelState.AddModelError("Password", "Password can't be empty!");
                return;
            }
        }

        [NonAction]
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        [NonAction]
        private void PasswordValidating(PasswordModel passwordModel)
        {
            if (string.IsNullOrEmpty(passwordModel.Password))
            {
                ModelState.AddModelError("Password", "Password can't be empty!");
                return;
            }
            if (passwordModel.Password.Length < 8 || passwordModel.Password.Length > 20)
            {
                ModelState.AddModelError("Password", "Password length is not correct!");
                return;
            }
            if (string.IsNullOrEmpty(passwordModel.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Confirm password can't be empty!");
                return;
            }
            if (passwordModel.ConfirmPassword.Length < 8 || passwordModel.ConfirmPassword.Length > 20)
            {
                ModelState.AddModelError("ConfirmPassword", "Confirm password length is not correct!");
                return;
            }
            if (!passwordModel.ConfirmPassword.Equals(passwordModel.Password))
            {
                ModelState.AddModelError("ConfirmPassword", "Confirm password is not match password!");
                return;
            }
        }

        [NonAction]
        public static void CreatingClaims(Employee employee, HttpContext context)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, employee.Name!),
                    new Claim(ClaimTypes.Email, employee.Email!),
                    new Claim(ClaimTypes.Role, employee.Role!)
                };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            context.SignInAsync(claimsPrincipal);
        }
    }
}
