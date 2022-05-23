using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UrlUtility.API.Dtos;
using UrlUtility.API.Entities;
using UrlUtility.API.Interfaces;

namespace UrlUtility.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepository _repo;
        private readonly ITime _time;

        public UrlController(IUrlRepository repo, ITime time)
        {
            _repo = repo;
            _time = time;
        }
    
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var dbItems = await _repo.GetAll();

            var dtos = dbItems.Select(i => new UrlDto
            {
                Identifier = i.Id,
                PageUrl = i.PageUrl
            });

            return Ok(dtos);
        }
        
        [HttpGet("Fetch/{urlTicket}")]
        public async Task<IActionResult> GetFullUrl(string urlTicket)
        {
            if (string.IsNullOrEmpty(urlTicket))
            {
                return BadRequest();
            }

            var urlItem = await _repo.GetUrl(urlTicket);

            if (urlItem is null)
            {
                return NotFound();
            }

            return Ok(urlItem.PageUrl);
        }


        [HttpGet("Save/{pageUrl}")]
        public async Task<IActionResult> SaveUrl(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
            {
                return BadRequest();
            }

            string decodedUrl = HttpUtility.UrlDecode(pageUrl);

            var entityModel = new Url
            {
                PageUrl = decodedUrl,
                CreatedOn = _time.DateTime
            };

            await _repo.Add(entityModel);

            return Ok(entityModel.Id);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveUrl([FromBody] SaveUrlDto request)
        {
            if (string.IsNullOrEmpty(request.PageUrl))
            {
                return BadRequest();
            }

            var entityModel = new Url
            {
                PageUrl = request.PageUrl,
                CreatedOn = _time.DateTime
            };

            await _repo.Add(entityModel);

            return Ok(entityModel.Id);
        }
    }
}
