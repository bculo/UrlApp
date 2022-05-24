using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;

namespace UrlUtility.API.Repository.Cosmo
{
    public class NoSqlUrlDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public NoSqlUrlDbContext(DbContextOptions<NoSqlUrlDbContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Configure modles / containers
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer("Url");

            modelBuilder.Entity<Url>().Property(x => x.Id).ToJsonProperty("id");
            modelBuilder.Entity<Url>().Property(x => x.PageUrl).ToJsonProperty("pageUrl");
            modelBuilder.Entity<Url>().Property(x => x.CreatedOn).ToJsonProperty("createdOn");
            modelBuilder.Entity<Url>().Property(x => x.PartitionKey).ToJsonProperty("partitionKey");

            modelBuilder.Entity<Url>()
                .ToContainer("Url")
                .HasPartitionKey(i => i.PartitionKey)
                .HasNoDiscriminator();
        }
    }
}
