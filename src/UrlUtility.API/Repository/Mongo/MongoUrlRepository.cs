using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlUtility.API.Entities;
using UrlUtility.API.Interfaces;
using UrlUtility.API.Options;

namespace UrlUtility.API.Repository.Mongo
{
    /// <summary>
    /// Repository implementation for storing URLs using CosmoDB (API for MongoDB) and MongoDB database
    /// </summary>
    public class MongoUrlRepository : IUrlRepository
    {
        private readonly IMongoCollection<Url> _urlCollection;

        public MongoUrlRepository(IOptions<MongoDbOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            _urlCollection = database.GetCollection<Url>(nameof(Url));
        }

        public async Task Add(Url url)
        {
            await _urlCollection.InsertOneAsync(url);
        }

        public async Task<List<Url>> GetAll()
        {
            return await _urlCollection.Find(_ => true)
                .SortByDescending(i => i.CreatedOn)
                .ToListAsync();
        }

        public async Task<Url> GetUrl(string urlIdentifier)
        {
            return await _urlCollection.Find(item => item.Id == urlIdentifier)
                .SingleOrDefaultAsync();
        }
    }
}
