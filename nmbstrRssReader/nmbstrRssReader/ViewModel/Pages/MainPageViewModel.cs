using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using nmbstrRssReader.Common;
using nmbstrRssReader.Model;

namespace nmbstrRssReader.ViewModel.Pages
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            GoToChannelCommand = new DelegateCommand(() =>
            {
                NavigationService.Navigate("/View/ChannelPage.xaml", null);
            });
            AddChannelCommand = new DelegateCommand(async () =>
            {
                var success = await DataRepository.AddChannel("http://rss.cnn.com/rss/cnn_topstories.rss");
                if (success)
                {
                    var channels = await DataRepository.GetChannelsList();
                    Channels = channels;
                }
            });
            UpdateChannelsCommand = new DelegateCommand(async () =>
            {
                await DataRepository.UpdateChannelsList();
            });
        }

        public async override Task OnNavigated()
        {
            await base.OnNavigated();
            var channels = await DataRepository.GetChannelsList();
            Channels = channels;
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
