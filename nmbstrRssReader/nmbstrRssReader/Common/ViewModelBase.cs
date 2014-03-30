using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using nmbstrRssReader.Annotations;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ICacheService _cacheService;
        protected INetworkService _networkService;
        protected INavigationService _navigationService;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {
            var app = Application.Current as App;
            if (app != null)
            {
                var container = app.IocContainer;
                _cacheService = container.Resolve<ICacheService>();
                _networkService = container.Resolve<INetworkService>();
                _navigationService = container.Resolve<INavigationService>();
            }
        }
    }
}
