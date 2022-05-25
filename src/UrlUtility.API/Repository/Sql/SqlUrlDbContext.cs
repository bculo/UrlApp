using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;

namespace UrlUtility.API.Repository.Sql
{
    /// <summary>
    /// DbContext for SQL server
    /// </summary>
    public class SqlUrlDbContext : DbContext
    {
        public SqlUrlDbContext(DbContextOptions<SqlUrlDbContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlUrlDbContext).Assembly);
        }
    }
}
