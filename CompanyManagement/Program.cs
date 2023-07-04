using CompanyManagement.Interfaces;
using CompanyManagement.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});
builder.Services.AddAuthorization();

var app = builder.Build();

await DataBaseInitializer.InitializeDatabaseAsync(app);

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
    );

app.UseAuthentication();
app.UseAuthorization();

app.Run();
