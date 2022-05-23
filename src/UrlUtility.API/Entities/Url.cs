using System;

namespace UrlUtility.API.Entities
{
    public class Url
    {
        public long Id { get; set; }
        public string PageUrl { get; set; }
        public string UrlIdentifier { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
