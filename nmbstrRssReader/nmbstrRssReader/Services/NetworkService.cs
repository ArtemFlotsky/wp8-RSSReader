using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using nmbstrRssReader.Extensions;
using nmbstrRssReader.Model;
using nmbstrRssReader.Resources;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Services
{
    public class NetworkService : INetworkService
    {
        private Dispatcher _dispatcher;

        public NetworkService(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<Channel> GetChannelByUrlAsync(string url)
        {
            try
            {
                var client = GetDefaultHttpClient();
                var str = string.Empty;
                try
                {
                    str = await client.GetStringAsync(url);
                }
                catch (Exception)
                {
                    _dispatcher.BeginInvoke(() => MessageBox.Show(AppResources.NetworkIssueMessage));
                }
                var stringReader = new StringReader(str);
                var xmlReader = XmlReader.Create(stringReader);
                var feed = SyndicationFeed.Load(xmlReader);
                var result = new Channel()
                {
                    Title = feed.Title.Text,
                    Url = url,
                    Description = feed.Description.Text,
                    Items = new List<Item>(),
                    ExternalId = url.CleaupUrl() //костыль
                };
                if (feed.ImageUrl != null)
                {
                    result.ImageUrl = feed.ImageUrl.OriginalString;
                }

                foreach (var item in feed.Items)
                {
                    try
                    {
                        var link = item.Id;
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
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return result;

            }
            catch (Exception e)
            {
                _dispatcher.BeginInvoke(() => MessageBox.Show(e.Message));
            }
            return null;
        }

        private HttpClient GetDefaultHttpClient()
        {
            return new HttpClient();
        }
    }
}
