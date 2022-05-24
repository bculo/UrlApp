using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;
using UrlUtility.API.Interfaces;

namespace UrlUtility.API.Repository.Sql.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly SqlUrlDbContext _context;

        public ShortUrlRepository(SqlUrlDbContext context)
        {
            _context = context;
        }

        public async Task Add(ShortUrl url)
        {
            _context.ShortUrls.Add(url);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShortUrl>> GetAll()
        {
            return await _context.ShortUrls.AsNoTracking().ToListAsync();
        }

        public async Task<ShortUrl> GetUrl(long id)
        {
            return await _context.ShortUrls.FindAsync(id);
        }
    }
}
