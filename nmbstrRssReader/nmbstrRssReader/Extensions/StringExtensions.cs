using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmbstrRssReader.Extensions
{
    public static class StringExtensions
    {

        public static string CleaupUrl(this string str)
        {
            return str.Replace(":", string.Empty)
                      .Replace("/", string.Empty)
                      .Replace(".", string.Empty)
                      .Replace("~", string.Empty)
                      .Replace("=", string.Empty)
                      .Replace("&", string.Empty);
        }
    }
}
