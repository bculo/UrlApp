using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlUtility.API.Utils
{
    public static class StringUtils
    {
        private static string ToCamelCase(this string characters)
        {
            if (!string.IsNullOrEmpty(characters) && characters.Length > 1)
            {
                return char.ToLowerInvariant(characters[0]) + characters.Substring(1);
            }

            return characters.ToLowerInvariant();
        }
    }
}
