using System;
using UrlUtility.API.Interfaces;

namespace UrlUtility.API.Services
{
    public class TimeService : ITime
    {
        public DateTime DateTime => DateTime.UtcNow;
    }
}
