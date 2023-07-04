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
            var employee = companyDbContext.Employees.Include(e => e.Department).SingleOrDefault(x => x.Id == id);
            companyDbContext.Entry(employee!).State = EntityState.Detached;
            return employee;
        }

        public void Add(Employee employee)
        {
            companyDbContext.Employees.Add(employee);
            companyDbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            companyDbContext.Employees.Update(employee);
            companyDbContext.Attach(employee);
            companyDbContext.Entry(employee).State = EntityState.Modified;
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
