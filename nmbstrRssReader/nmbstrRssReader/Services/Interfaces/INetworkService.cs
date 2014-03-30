using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmbstrRssReader.Model;

namespace nmbstrRssReader.Services.Interfaces
{
    public interface INetworkService
    {
        Task<Channel> GetChannelByUrlAsync(string url);
    }
}
