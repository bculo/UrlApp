using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;

namespace UrlUtility.API.Interfaces
{
    public interface IShortUrlRepository
    {
        Task Add(ShortUrl url);
        Task<List<ShortUrl>> GetAll();
        Task<ShortUrl> GetUrl(long id);
    }
}
