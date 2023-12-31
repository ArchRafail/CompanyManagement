﻿// <auto-generated />
using CompanyManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyManagement.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20230703132959_EmployeeModelChanged")]
    partial class EmployeeModelChanged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyManagement.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Manager")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Manager = "John Week",
                            Name = "Accounting"
                        },
                        new
                        {
                            Id = 2,
                            Manager = "Bill Gates",
                            Name = "Marketing"
                        },
                        new
                        {
                            Id = 3,
                            Manager = "Che Gevara",
                            Name = "Sales"
                        },
                        new
                        {
                            Id = 4,
                            Manager = "Winston Churchill",
                            Name = "Human Resources"
                        },
                        new
                        {
                            Id = 5,
                            Manager = "Nelson Mandela",
                            Name = "Legal"
                        },
                        new
                        {
                            Id = 6,
                            Manager = "Mahatma Gandhi",
                            Name = "Engineering"
                        });
                });

            modelBuilder.Entity("CompanyManagement.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentId = 1,
                            Email = "t.roosevelt@comp.com",
                            Name = "Theodore Roosevelt",
                            Password = "25d55ad283aa400af464c76d713c07ad",
                            PhoneNumber = "380671234567",
                            Role = "user"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentId = 2,
                            Email = "de@comp.com",
                            Name = "Dwight Eisenhower",
                            Password = "1bbd886460827015e5d605ed44252251",
                            PhoneNumber = "380957654321",
                            Role = "user"
                        },
                        new
                        {
                            Id = 3,
                            DepartmentId = 3,
                            Email = "rore@sale.comp.com",
                            Name = "Ronald Reagan",
                            Password = "1bbd886460827015e5d605ed44252251",
                            PhoneNumber = "380960246897",
                            Role = "user"
                        },
                        new
                        {
                            Id = 4,
                            DepartmentId = 4,
                            Email = "thatcher@hr.comp.com",
                            Name = "Margaret Thatcher",
                            Password = "1bbd886460827015e5d605ed44252251",
                            PhoneNumber = "380939753102",
                            Role = "user"
                        },
                        new
                        {
                            Id = 5,
                            DepartmentId = 5,
                            Email = "woodwi@comp.com",
                            Name = "Woodrow Wilson",
                            Password = "5f4dcc3b5aa765d61d8327deb882cf99",
                            PhoneNumber = "380680918273",
                            Role = "admin "
                        },
                        new
                        {
                            Id = 6,
                            DepartmentId = 6,
                            Email = "jawane@comp.com",
                            Name = "Jawaharlal Nehru",
                            Password = "1bbd886460827015e5d605ed44252251",
                            PhoneNumber = "380989081726",
                            Role = "user"
                        });
                });

            modelBuilder.Entity("CompanyManagement.Models.Employee", b =>
                {
                    b.HasOne("CompanyManagement.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CompanyManagement.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
