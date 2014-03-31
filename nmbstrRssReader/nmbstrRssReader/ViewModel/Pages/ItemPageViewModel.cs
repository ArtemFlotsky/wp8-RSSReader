using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using Microsoft.Practices.Prism.Commands;
using nmbstrRssReader.Common;
using nmbstrRssReader.Model;

namespace nmbstrRssReader.ViewModel.Pages
{
    public class ItemPageViewModel : ViewModelBase
    {
        public ItemPageViewModel()
        {
            GoToLink = new DelegateCommand(() =>
            {
                var webBrowserTask = new WebBrowserTask {Uri = new Uri(HyperLink, UriKind.Absolute)};
                webBrowserTask.Show();
            });
        }

        public async override Task OnNavigated()
        {
            await base.OnNavigated();
            var item = NavigationService.GetNavigationData() as Item;
            if (item != null)
            {
                Title = item.Title;
                Text = item.Text;
                HyperLink = item.Url;
            }

            
        }

        public ICommand GoToLink { get; set; }

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

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private string _hyperLink;

        public string HyperLink
        {
            get { return _hyperLink; }
            set
            {
                _hyperLink = value;
                OnPropertyChanged();
            }
        }
    }
}
