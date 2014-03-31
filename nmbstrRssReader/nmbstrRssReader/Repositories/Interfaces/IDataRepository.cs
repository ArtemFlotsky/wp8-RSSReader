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
        Task<bool> UpdateChannel(string id);
        Task<IEnumerable<Item>> GetChannelItemsList(string id);
    }
}
