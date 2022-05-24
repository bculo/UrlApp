using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;
using UrlUtility.API.Interfaces;

namespace UrlUtility.API.Repository.Cosmo
{
    public class CosmoEntityUrlRepository : IUrlRepository
    {
        private readonly UrlDbContext _context;

        public CosmoEntityUrlRepository(UrlDbContext context)
        {
            _context = context;
        }

        public async Task Add(Url url)
        {
            url.Id = url.PartitionKey = Guid.NewGuid().ToString();
            _context.Urls.Add(url);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Url>> GetAll()
        {
            return await _context.Urls.ToListAsync();
        }

        public async Task<Url> GetUrl(string urlIdentifier)
        {
            return await _context.Urls.SingleOrDefaultAsync(item => item.Id == urlIdentifier);
        }
    }
}
