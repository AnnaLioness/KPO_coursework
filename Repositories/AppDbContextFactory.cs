using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Используйте ту же строку подключения, что и в Program.cs
            optionsBuilder.UseNpgsql("Host=localhost;Database=KPODatabase;Username=postgres;Password=1234");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
