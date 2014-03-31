using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using nmbstrRssReader.Extensions;
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
                Url = url,
                Description = feed.Description.Text,
                Items = new List<Item>(),
                ExternalId = url.CleaupUrl() //костыль
            };
           
            foreach (var item in feed.Items)
            {
                var link = item.Links.FirstOrDefault().Uri.AbsolutePath;
                var cleanedLink = link.CleaupUrl();
                result.Items.Add(new Item()
                {
                    Title = item.Title.Text,
                    Text = item.Summary.Text,
                    Url = link,
                    ExternalId = cleanedLink,
                    ChannelId = result.ExternalId.CleaupUrl(),
                });
            }
            return result;
        }

        private HttpClient GetDefaultHttpClient()
        {
            return new HttpClient();
        }
    }
}
