using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlUtility.API.Entities
{
    public class ShortUrl : Entity<long>
    {
        public string PageUrl { get; set; }
    }
}
