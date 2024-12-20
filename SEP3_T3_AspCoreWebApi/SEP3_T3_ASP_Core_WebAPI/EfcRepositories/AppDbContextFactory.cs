using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SEP3_T3_ASP_Core_WebAPI
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sep3db;Username=postgres;Password=P@ssw0rd;Timeout=10;SSL Mode=Prefer");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
