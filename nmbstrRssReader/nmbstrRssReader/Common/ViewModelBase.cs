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
using nmbstrRssReader.Repositories.Interfaces;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected IDataRepository DataRepository;
        protected INavigationService NavigationService;

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
                DataRepository = container.Resolve<IDataRepository>();
                NavigationService = container.Resolve<INavigationService>();
            }
        }

        public virtual async Task OnNavigated()
        {
            await DataRepository.Initialize();
        }
    }
}
