using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private CompanyDbContext companyDbContext;

        public DepartmentsRepository(CompanyDbContext companyDbContext)
        {
            this.companyDbContext = companyDbContext;
        }

        public List<Department> GetAll()
        {
            return companyDbContext.Departments.Include(d => d.Employees).ToList();
        }

        public Department? Get(int id)
        {
            var department = companyDbContext.Departments.Include(d => d.Employees).SingleOrDefault(x => x.Id == id);
            companyDbContext.Entry(department!).State = EntityState.Detached;
            return department;
        }

        public void Add(Department department)
        {
            companyDbContext.Departments.Add(department);
            companyDbContext.SaveChanges();
        }

        public void Update(Department department)
        {
            companyDbContext.Departments.Update(department);
            companyDbContext.Attach(department);
            companyDbContext.Entry(department).State = EntityState.Modified;
            companyDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Department? department = Get(id);
            if (department != null)
            {
                companyDbContext.Departments.Remove(department);
                companyDbContext.SaveChanges();
            }
        }
    }
}
