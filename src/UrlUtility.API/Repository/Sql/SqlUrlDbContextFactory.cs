using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlUtility.API.Repository.Sql
{
    public class SqlUrlDbContextFactory : IDesignTimeDbContextFactory<SqlUrlDbContext>
    {
        public SqlUrlDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", false)
                .Build();


            string connectionString = configuration["SqlServer:ConnectionString"];

            DbContextOptionsBuilder<SqlUrlDbContext> optionsBuilder = new DbContextOptionsBuilder<SqlUrlDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SqlUrlDbContext(optionsBuilder.Options);
        }
    }
}
