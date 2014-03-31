﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using nmbstrRssReader.Extensions;
using SQLite;
using System.Xml.Linq;
using nmbstrRssReader.Model;
using nmbstrRssReader.Repositories.Interfaces;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly INetworkService _networkService;
        private readonly ICacheService _cacheService;
        


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

        public async Task<IEnumerable<Channel>> UpdateChannelsList()
        {
            var resultCollection = new List<Channel>();
            var channels = await _cacheService.GetChannels();
            foreach (var channel in channels)
            {
                var newChannel = await _networkService.GetChannelByUrlAsync(channel.Url);
                if (newChannel != null)
                {
                    await _cacheService.RemoveItemsByChannelId(channel.ExternalId);
                    foreach (var item in newChannel.Items)
                    {
                        if (!await _cacheService.IsItemExists(item.ExternalId))
                        {
                            await _cacheService.AddItem(item);
                        }
                    }
                    await _cacheService.UpdateChannel(newChannel);
                    resultCollection.Add(newChannel);
                }

            }
            return resultCollection;
        }

        public async Task<bool> AddChannel(string address)
        {
            if (await _cacheService.IsChannelExists(address.CleaupUrl())) return false;
            var channel = await _networkService.GetChannelByUrlAsync(address);
            await _cacheService.AddChannel(channel);
            Debug.WriteLine("Adding items");
            foreach (var item in channel.Items)
            {
                if (!await _cacheService.IsItemExists(item.ExternalId))
                {
                    await _cacheService.AddItem(item);                    
                }
            }
            return true;
        }

        public async Task<bool> RemoveChannel(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateChannel(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetChannelItemsList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
