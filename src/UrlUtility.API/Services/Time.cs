using System;
using UrlUtility.API.Interfaces;

namespace UrlUtility.API.Services
{
    public class Time : ITime
    {
        public DateTime DateTime => DateTime.UtcNow;
    }
}
