using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlUtility.API.Repository.Cosmo
{
    public static class SeedDataEntityCosmoDb
    {
        /// <summary>
        /// Create COSMO DB
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static async Task SeedData(IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<NoSqlUrlDbContext>();

            await dbContext.Database.EnsureCreatedAsync(); 
        }
    }
}
