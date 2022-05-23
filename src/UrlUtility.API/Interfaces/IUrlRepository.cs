using System.Collections.Generic;
using System.Threading.Tasks;
using UrlUtility.API.Entities;

namespace UrlUtility.API.Interfaces
{
    public interface IUrlRepository
    {
        Task Add(Url url);
        Task<List<Url>> GetAll();
        Task<Url> GetUrl(string urlIdentifier);
    }
}
