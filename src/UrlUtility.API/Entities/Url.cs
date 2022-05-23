using Newtonsoft.Json;
using System;

namespace UrlUtility.API.Entities
{
    public class Url
    {
        public string Id { get; set; }
        public string PageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
