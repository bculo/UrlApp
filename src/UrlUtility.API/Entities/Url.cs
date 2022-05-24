using Newtonsoft.Json;
using System;

namespace UrlUtility.API.Entities
{
    public class Url : Entity<string>
    {
        public string PageUrl { get; set; }
        public string PartitionKey { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
