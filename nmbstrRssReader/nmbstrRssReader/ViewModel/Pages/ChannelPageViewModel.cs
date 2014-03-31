using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using nmbstrRssReader.Common;
using nmbstrRssReader.Extensions;
using nmbstrRssReader.Model;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.ViewModel.Pages
{
    public class ChannelPageViewModel : ViewModelBase
    {
        public ChannelPageViewModel()
        {
            RefreshCommand = new DelegateCommand(async () =>
            {
                if (_channel != null)
                {
                    AddProgressKey("refresh");
                    var newChannel = await DataRepository.UpdateChannel(_channel);
                    if (newChannel != null)
                    {
                        Items = newChannel.Items;
                    }
                    RemoveProgressKey("refresh");
                }
            });

            GoToItemCommand = new DelegateCommand<Item>(
                item => NavigationService.Navigate("/View/ItemPage.xaml", item));
        }

        public async override Task OnNavigated()
        {
            await base.OnNavigated();
            var channel = NavigationService.GetNavigationData() as Channel;
            if (channel != null)
            {
                _channel = channel;
                Title = channel.Title.TrimHtml();
                var items = await DataRepository.GetChannelItemsList(channel.ExternalId);
                Items = items;
            }
        }

        public ICommand RefreshCommand { get; set; }

        public ICommand GoToItemCommand { get; set; }

        private Channel _channel;

        private IEnumerable<Item> _items;
        public IEnumerable<Item> Items
        {
            get
            {
                return _items;

            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}
