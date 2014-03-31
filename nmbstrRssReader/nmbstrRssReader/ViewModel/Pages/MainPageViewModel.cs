using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using nmbstrRssReader.Common;
using nmbstrRssReader.Model;
using nmbstrRssReader.Repositories.Interfaces;
using nmbstrRssReader.Services;
using nmbstrRssReader.Services.Interfaces;
using nmbstrRssReader.View.Popups;

namespace nmbstrRssReader.ViewModel.Pages
{
    public class MainPageViewModel : ViewModelBase
    {
        protected MessageBoxService<AddChannelPopup, AddChannelPopupResult> MessageBoxService; 

        public MainPageViewModel()
        {
            var app = Application.Current as App;
            if (app != null)
            {
                var container = app.IocContainer;
                MessageBoxService = container.Resolve<MessageBoxService<AddChannelPopup, AddChannelPopupResult>>();
            }

            GoToChannelCommand = new DelegateCommand<Channel>(channel => NavigationService.Navigate("/View/ChannelPage.xaml", channel));
            AddChannelCommand = new DelegateCommand(async () =>
            {
                var result = await MessageBoxService.ShowAsync(NavigationService.Frame, null);
                if (result != null && result.MessageBoxResult == MessageBoxResult.OK)
                {
                    AddProgressKey("add");
                    var success = await DataRepository.AddChannel(result.Text);
                    if (success)
                    {
                        var channels = await DataRepository.GetChannelsList();
                        Channels = channels;
                    }
                    RemoveProgressKey("add");
                }
            });
            UpdateChannelsCommand = new DelegateCommand(async () =>
            {
                AddProgressKey("update");
                var channels = await DataRepository.UpdateChannelsList();
                Channels = channels;
                RemoveProgressKey("update");
            });
        }

        public async override Task OnNavigated()
        {
            AddProgressKey("nav");
            await base.OnNavigated();
            var channels = await DataRepository.GetChannelsList();
            Channels = channels;
            RemoveProgressKey("nav");
            if (Channels == null || !Channels.Any())
            {
                var success = await DataRepository.AddChannel("http://feeds.nytimes.com/nyt/rss/Technology");
                if (success)
                {
                    channels = await DataRepository.GetChannelsList();
                    Channels = channels;
                }
                success = await DataRepository.AddChannel("http://www.npr.org/rss/rss.php?id=1019");
                if (success)
                {
                    channels = await DataRepository.GetChannelsList();
                    Channels = channels;
                }
                success = await DataRepository.AddChannel("http://feeds.surfnetkids.com/SurfingTheNetWithKids");
                if (success)
                {
                    channels = await DataRepository.GetChannelsList();
                    Channels = channels;
                }
            }
        }

        public ICommand GoToChannelCommand { get; set; }
        public ICommand AddChannelCommand { get; set; }
        public ICommand UpdateChannelsCommand { get; set; }

        private IEnumerable<Channel> _channels; 
        public IEnumerable<Channel> Channels
        {
            get
            {
                return _channels;
                
            }
            set
            {
                _channels = value;
                OnPropertyChanged();
            }
        }
    }
}
