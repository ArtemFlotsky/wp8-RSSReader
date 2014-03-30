using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using nmbstrRssReader.Extensions;

namespace nmbstrRssReader.Services.Interfaces
{
    public class NavigationService : INavigationService
    {
        private readonly PhoneApplicationFrame _frame;

        public NavigationService(PhoneApplicationFrame frame)
        {
            _frame = frame;
        }

        public void Navigate(string pageToken, object parameter)
        {
            _frame.Navigate(new Uri(pageToken, UriKind.RelativeOrAbsolute), parameter);
        }

        public void GoBack()
        {
            _frame.GoBack();
        }
    }
}
