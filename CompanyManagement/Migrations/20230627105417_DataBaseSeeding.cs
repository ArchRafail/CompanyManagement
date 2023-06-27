using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyManagement.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Manager", "Name" },
                values: new object[,]
                {
                    { 1, "John Week", "Accounting" },
                    { 2, "Bill Gates", "Marketing" },
                    { 3, "Che Gevara", "Sales" },
                    { 4, "Winston Churchill", "Human Resources" },
                    { 5, "Nelson Mandela", "Legal" },
                    { 6, "Mahatma Gandhi", "Engineering" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "t.roosevelt@comp.com", "Theodore Roosevelt", "380671234567" },
                    { 2, 2, "de@comp.com", "Dwight Eisenhower", "380957654321" },
                    { 3, 3, "rore@sale.comp.com", "Ronald Reagan", "380960246897" },
                    { 4, 4, "thatcher@hr.comp.com", "Margaret Thatcher", "380939753102" },
                    { 5, 5, "woodwi@comp.com", "Woodrow Wilson", "380680918273" },
                    { 6, 6, "jawane@comp.com", "Jawaharlal Nehru", "380989081726" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
