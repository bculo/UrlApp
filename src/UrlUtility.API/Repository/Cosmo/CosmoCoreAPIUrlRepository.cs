using Microsoft.Azure.Cosmos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;
using UrlUtility.API.Interfaces;

namespace UrlUtility.API.Repository.Cosmo
{
    /// <summary>
    /// Repository implementation using Core API SQL
    /// </summary>
    public class CosmoCoreAPIUrlRepository : IUrlRepository
    {
        private readonly Container _container;

        public CosmoCoreAPIUrlRepository(CosmosClient client, string databaseName, string containerName)
        {
            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task Add(Url url)
        {
            url.Id = url.PartitionKey = Guid.NewGuid().ToString();
            await _container.CreateItemAsync(url, new PartitionKey(url.PartitionKey));
        }

        public async Task<List<Url>> GetAll()
        {
            var query = _container.GetItemQueryIterator<Url>(new QueryDefinition("SELECT * FROM Url"));
            var results = new List<Url>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Url> GetUrl(string urlIdentifier)
        {
            try
            {
                var response = await _container.ReadItemAsync<Url>(urlIdentifier, new PartitionKey(urlIdentifier));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
