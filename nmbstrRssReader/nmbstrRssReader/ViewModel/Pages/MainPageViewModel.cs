using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using nmbstrRssReader.Common;

namespace nmbstrRssReader.ViewModel.Pages
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            GoToChannel = new DelegateCommand(() =>
            {
                _navigationService.Navigate("/View/ChannelPage.xaml", null);
            });
        }
        public ICommand GoToChannel { get; set; }
    }
}
