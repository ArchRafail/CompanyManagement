using CompanyManagement.Models;

namespace CompanyManagement.Interfaces
{
    public interface IEmployeesRepository
    {
        public List<Employee> GetAll();

        public Employee? Get(int id);

        public void Add(Employee employee);

        public void Update(Employee employee);

        public void Delete(int id);
    }
}
