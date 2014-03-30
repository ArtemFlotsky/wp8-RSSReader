using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace nmbstrRssReader.Extensions
{
    public static class NavigationServiceExtension
    {
        private static object _data;

        private static PhoneApplicationFrame _rootFrame;

        public static void SetRootFrame(PhoneApplicationFrame frame)
        {
            _rootFrame = frame;
        }

        public static void Navigate(this PhoneApplicationFrame navigationService,
                                Uri source, object data)
        {
            _data = data;
            navigationService.Navigate(source);
        }

        public static object GetNavigationData(this PhoneApplicationFrame service)
        {
            return _data;
        }
    }
}
