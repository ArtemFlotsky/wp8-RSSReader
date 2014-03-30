using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using nmbstrRssReader.Model;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Services
{
    public class NetworkService : INetworkService
    {
        public async Task<Channel> GetChannelByUrlAsync(string url)
        {
            var client = GetDefaultHttpClient();
            var str = await client.GetStringAsync(url);
            var stringReader = new StringReader(str);
            var xmlReader = XmlReader.Create(stringReader);
            var feed = SyndicationFeed.Load(xmlReader);
            var result = new Channel()
            {
                Title = feed.Title.Text,
                ImageUrl = feed.ImageUrl.OriginalString,
                Url = url
            };
            return result;
        }

        private HttpClient GetDefaultHttpClient()
        {
            return new HttpClient();
        }
    }
}
