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
            GoToChannel = new DelegateCommand(() =>
            {
                NavigationService.Navigate("/View/ChannelPage.xaml", null);
            });
        }

        public async override Task OnNavigated()
        {
            await base.OnNavigated();
            var channels = await DataRepository.GetChannelsList();
            Channels = channels;
        }

        public ICommand GoToChannel { get; set; }

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
