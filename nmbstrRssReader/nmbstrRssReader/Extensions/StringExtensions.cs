using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static string TrimHtml(this string str)
        {
            return StripTagsRegexCompiled(str);
        }

        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }
    }
}
