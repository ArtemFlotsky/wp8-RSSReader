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
        private SQLiteConnection dbConn;

        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.DBName));


        public DataRepository(INetworkService networkService, ICacheService cacheService)
        {
            _networkService = networkService;
            _cacheService = cacheService;



        }

        public async Task Initialize()
        {
            StorageFile dbFile = null;
            // Try to get the 
            try
            {
                dbFile = await ApplicationData.Current.LocalFolder.GetFileAsync(Constants.DBName);
            }
            catch (FileNotFoundException)
            {
                if (dbFile == null)
                {
                    IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();

                    using (
                        Stream input = Application.GetResourceStream(new Uri(Constants.DBName, UriKind.Relative)).Stream
                        )
                    {
                        using (IsolatedStorageFileStream output = iso.CreateFile(DB_PATH))
                        {
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                output.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                dbConn = new SQLiteConnection(dbFile.Path);
            }
            catch (Exception)
            {
                throw;
            }
            

            dbConn.CreateTable<Channel>();
            List<Channel> channels = dbConn.Table<Channel>().ToList();
        }

        public async Task<IEnumerable<Channel>> GetChannelsList()
        {
            _networkService.GetChannelByUrlAsync("http://rss.cnn.com/rss/cnn_topstories.rss");
            return null;
        }

        public bool AddChannel(string address, string name)
        {
            throw new NotImplementedException();
        }

        public bool RemoveChannel(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetChannelItemsList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
