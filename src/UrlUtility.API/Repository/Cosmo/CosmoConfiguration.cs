using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlUtility.API.Repository.Cosmo
{
    public static class CosmoConfiguration
    {
        public static async Task<CosmoUrlRepository> InitializeCosmosClientInstanceAsync(IConfiguration config)
        {
            string connection = config.GetValue<string>("Cosmo:ConnectionString");
            string databaseName = config.GetValue<string>("Cosmo:DatabaseName");
            string containerName = config.GetValue<string>("Cosmo:ContainerName");

            var client = new CosmosClient(connection, new CosmosClientOptions
            {
               SerializerOptions = new CosmosSerializationOptions
               {
                   PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
               }
            });

            var urlRepo = new CosmoUrlRepository(client, databaseName, containerName);

            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return urlRepo;
        }
    }
}
