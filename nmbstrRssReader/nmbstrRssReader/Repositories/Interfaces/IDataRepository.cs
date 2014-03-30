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
        bool AddChannel(string address, string name);
        bool RemoveChannel(string id);

        IEnumerable<Item> GetChannelItemsList(string id);
    }
}
