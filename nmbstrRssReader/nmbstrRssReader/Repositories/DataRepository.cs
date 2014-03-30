using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Windows.Storage;
using SQLite;
using System.Xml.Linq;
using nmbstrRssReader.Model;
using nmbstrRssReader.Repositories.Interfaces;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Repositories
{
    public class DataRepository : IDataRepository
    {
        private INetworkService _networkService;
        private ICacheService _cacheService;
        


        public DataRepository(INetworkService networkService, ICacheService cacheService)
        {
            _networkService = networkService;
            _cacheService = cacheService;



        }

        public async Task Initialize()
        {
            await _cacheService.Initialize();
        }

        public async Task<IEnumerable<Channel>> GetChannelsList()
        {
            return await _cacheService.GetChannels();
            
        }

        public async Task<bool> AddChannel(string address)
        {
            var channel = await _networkService.GetChannelByUrlAsync(address);
            await _cacheService.AddChannel(channel);
            return true;
        }

        public async Task<bool> RemoveChannel(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetChannelItemsList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
