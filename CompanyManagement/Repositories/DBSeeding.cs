using CompanyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Repositories
{
    public class DBSeeding
    {
        private readonly ModelBuilder modelBuilder;

        public DBSeeding(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            List<Department> departments = new List<Department>()
            {
                new Department() { Id = 1, Name = "Accounting", Manager = "John Week"},
                new Department() { Id = 2, Name = "Marketing", Manager = "Bill Gates"},
                new Department() { Id = 3, Name = "Sales", Manager = "Che Gevara"},
                new Department() { Id = 4, Name = "Human Resources", Manager = "Winston Churchill"},
                new Department() { Id = 5, Name = "Legal", Manager = "Nelson Mandela"},
                new Department() { Id = 6, Name = "Engineering", Manager = "Mahatma Gandhi"}
            };

            modelBuilder.Entity<Department>().HasData(
                departments[0],
                departments[1],
                departments[2],
                departments[3],
                departments[4],
                departments[5]
            );

            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Theodore Roosevelt",
                    Email = "t.roosevelt@comp.com",
                    PhoneNumber = "380671234567",
                    DepartmentId = departments[0].Id
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Dwight Eisenhower",
                    Email = "de@comp.com",
                    PhoneNumber = "380957654321",
                    DepartmentId = departments[1].Id
                },
                new Employee()
                {
                    Id = 3,
                    Name = "Ronald Reagan",
                    Email = "rore@sale.comp.com",
                    PhoneNumber = "380960246897",
                    DepartmentId = departments[2].Id
                },
                new Employee()
                {
                    Id = 4,
                    Name = "Margaret Thatcher",
                    Email = "thatcher@hr.comp.com",
                    PhoneNumber = "380939753102",
                    DepartmentId = departments[3].Id
                },
                new Employee()
                {
                    Id = 5,
                    Name = "Woodrow Wilson",
                    Email = "woodwi@comp.com",
                    PhoneNumber = "380680918273",
                    DepartmentId = departments[4].Id
                },
                new Employee()
                {
                    Id = 6,
                    Name = "Jawaharlal Nehru",
                    Email = "jawane@comp.com",
                    PhoneNumber = "380989081726",
                    DepartmentId = departments[5].Id
                }
            };

            modelBuilder.Entity<Employee>().HasData(
                employees[0],
                employees[1],
                employees[2],
                employees[3],
                employees[4],
                employees[5]
            );
        }
    }
}
