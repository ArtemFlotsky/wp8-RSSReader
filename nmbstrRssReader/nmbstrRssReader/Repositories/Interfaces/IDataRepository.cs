using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmbstrRssReader.Model;

namespace nmbstrRssReader.Repositories.Interfaces
{
    public interface IDataRepository
    {
        Task Initialize();

        Task<IEnumerable<Channel>> GetChannelsList();
        Task<IEnumerable<Channel>> UpdateChannelsList(); 

        Task<bool> AddChannel(string address);
        Task<bool> RemoveChannel(string id);
        Task<Channel> UpdateChannel(Channel channel);
        Task<List<Item>> GetChannelItemsList(string id);
    }
}
