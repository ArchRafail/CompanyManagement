using CompanyManagement.Interfaces;
using CompanyManagement.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddControllersWithViews();

var databaseConnectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<CompanyDbContext>(opts =>
        {
            opts.UseSqlServer(databaseConnectionString);
            opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

var app = builder.Build();

await DataBaseInitializer.InitializeDatabaseAsync(app);

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}"
    );

app.Run();
