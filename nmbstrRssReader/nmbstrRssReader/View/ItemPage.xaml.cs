using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using nmbstrRssReader.ViewModel.Pages;

namespace nmbstrRssReader.View
{
    public partial class ItemPage : PhoneApplicationPage
    {
        public ItemPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var dataContext = (ItemPageViewModel)DataContext;
            await dataContext.OnNavigated();
        }
    }
}