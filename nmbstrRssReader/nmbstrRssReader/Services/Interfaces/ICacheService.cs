using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmbstrRssReader.Model;

namespace nmbstrRssReader.Services.Interfaces
{
    public interface ICacheService
    {
        Task Initialize();

        Task<List<Channel>> GetChannels();
        Task<bool> AddChannel(Channel channel);
        Task<bool> RemoveChannel(Channel channel);
        Task<bool> UpdateChannel(Channel channel);
        Task<bool> IsChannelExists(string id);
        Task<bool> AddItem(Item item);
        Task<bool> RemoveItem(Item item);
        Task<bool> RemoveItemsByChannelId(string channelId);
        Task<List<Item>> GetItemsByChannelId(string channelId);
        Task<bool> IsItemExists(string id);
    }
}
