namespace CompanyManagement.ViewModels
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DepartmentModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
