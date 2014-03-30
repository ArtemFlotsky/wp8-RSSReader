﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmbstrRssReader.Services.Interfaces
{
    public interface INavigationService
    {
        void Navigate(string pageToken, object parameter);
        void GoBack();
    }
}