using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Data.Context;

namespace ConsoleFiveM
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // On lit la MÊME source que Data\Program.cs, pour ne jamais migrer la mauvaise base
            var basePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Data");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(basePath))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string introuvable dans Data/appsettings.json");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(
                connectionString,
                mysqlOptions => mysqlOptions.ServerVersion(new Version(8, 0, 21), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql)
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}