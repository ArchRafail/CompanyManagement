using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IDepartmentsRepository
    {
        public List<Department> GetAll();

        public Department? Get(int id);

        public void Add(Department department);

        public void Update(Department department);

        public void Delete(int id);
    }
}
