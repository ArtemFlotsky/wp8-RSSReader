using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;
using nmbstrRssReader.Model;
using nmbstrRssReader.Services.Interfaces;
using SQLite;

namespace nmbstrRssReader.Services
{
    class CacheService : ICacheService
    {
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.DBName));

        private SQLiteConnection dbConn;

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
            dbConn.CreateTable<Item>();
            
#if DEBUG
            dbConn.DeleteAll<Channel>();
            dbConn.DeleteAll<Item>();
#endif
        }

        public async Task<List<Channel>> GetChannels()
        {
            var channels = dbConn.Table<Channel>().ToList();
            return channels;
        }

        public async Task<bool> AddChannel(Channel channel)
        {
            dbConn.Insert(channel);
#if DEBUG
            foreach (var source in dbConn.Table<Channel>().ToList())
            {
                Debug.WriteLine(string.Format("{0} {1} {2} {3}", source.Id, source.Title, source.ImageUrl, source.Url));
            }
#endif
            return true;
        }

        public Task<bool> RemoveChannel(Channel channel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateChannel(Channel channel)
        {
            try
            {
                var items =
                dbConn.Query<Channel>(
                    string.Format(
                        "update Channel set Title = \"{0}\", ImageUrl = \"{1}\", Description = \"{2}\" where ExternalId = \"{3}\"",
                        channel.Title,
                        channel.ImageUrl,
                        channel.Description,
                        channel.ExternalId));
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public async Task<bool> IsChannelExists(string id)
        {
            try
            {
                var items = dbConn.Query<Channel>(string.Format("select * from Channel where ExternalId = \"{0}\"", id));
                return items.Count != 0;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> AddItem(Item item)
        {
            dbConn.Insert(item);
            Debug.WriteLine(string.Format("{0} {1} {2}", item.Id, item.Title, item.Url));
            return true;
        }

        public Task<bool> RemoveItem(Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveItemsByChannelId(string channelId)
        {
            dbConn.Query<Item>(string.Format("delete from Item where ChannelId = \"{0}\"", channelId));
#if DEBUG
            Debug.WriteLine("Deleted item with ChannelId = {0}", channelId);
            var list = dbConn.Table<Item>().ToList();
            if (list != null && list.Any())
            {
                foreach (var source in list)
                {
                    Debug.WriteLine(string.Format("{0} {1} {2}", source.Id, source.Title, source.Url));
                }
            }
#endif
            return true;
        }

        public async Task<bool> IsItemExists(string id)
        {
            try
            {
                var items = dbConn.Query<Item>(string.Format("select * from Item where ExternalId = \"{0}\"", id));
                return items.Count != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
