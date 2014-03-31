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

        internal readonly Dictionary<string, string> _isInProgressKeys = new Dictionary<string, string>();

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

        private bool _isInProgress;
        public bool IsInProgress
        {
            get
            {
                return _isInProgress;
            }

            set
            {
                _isInProgress = value;
                this.OnPropertyChanged("IsInProgress");
            }
        }

        protected void AddProgressKey(string key)
        {
            this._isInProgressKeys[key] = key;
            this.IsInProgress = true;

        }

        protected void RemoveProgressKey(string key)
        {
            this._isInProgressKeys.Remove(key);
            if (this._isInProgressKeys.Keys.Count == 0)
            {
                this.IsInProgress = false;
            }
        }
    }
}
