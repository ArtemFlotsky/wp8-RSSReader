using System;
using Microsoft.Phone.Controls;
using nmbstrRssReader.Extensions;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Services
{
    public class NavigationService : INavigationService
    {

        public NavigationService(PhoneApplicationFrame frame)
        {
            Frame = frame;
        }

        public void Navigate(string pageToken, object parameter)
        {
            Frame.Navigate(new Uri(pageToken, UriKind.RelativeOrAbsolute), parameter);
        }

        public object GetNavigationData()
        {
            return Frame.GetNavigationData();
        }

        public void GoBack()
        {
            Frame.GoBack();
        }

        public PhoneApplicationFrame Frame { get; set; }
    }
}
