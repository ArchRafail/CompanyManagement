using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private CompanyDbContext companyDbContext;

        public EmployeesRepository(CompanyDbContext companyDbContext)
        {
            this.companyDbContext = companyDbContext;
        }

        public List<Employee> GetAll()
        {
            return companyDbContext.Employees.Include(e => e.Department).ToList();
        }

        public Employee? Get(int id)
        {
            return companyDbContext.Employees.Include(e => e.Department).SingleOrDefault(x => x.Id == id);
        }

        public void Add(Employee employee)
        {
            companyDbContext.Employees.Add(employee);
            companyDbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            companyDbContext.Update(employee);
            companyDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Employee? employee = Get(id);
            if (employee != null)
            {
                companyDbContext.Employees.Remove(employee);
                companyDbContext.SaveChanges();
            }
        }
    }
}
