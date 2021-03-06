﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;

namespace nmbstrRssReader.Services.Interfaces
{
    public interface INavigationService
    {
        void Navigate(string pageToken, object parameter);
        object GetNavigationData();
        void GoBack();

        PhoneApplicationFrame Frame { get; set; }
    }
}
