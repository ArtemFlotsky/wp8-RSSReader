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
            
        }

        public async Task<List<Channel>> GetChannels()
        {
            return dbConn.Table<Channel>().ToList();
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

        public async Task<bool> AddItem(Item item)
        {
            dbConn.Insert(item);
            return true;
        }

        public Task<bool> RemoveItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
