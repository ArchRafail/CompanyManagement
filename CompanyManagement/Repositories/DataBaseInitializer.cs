using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Repositories
{
    public static class DataBaseInitializer
    {
        public static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var companyDbContext = serviceScope.ServiceProvider.GetService<CompanyDbContext>();
            await companyDbContext!.Database.MigrateAsync();
        }
    }
}
