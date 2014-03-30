using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmbstrRssReader.ViewModel.Pages;

namespace nmbstrRssReader.ViewModel
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                return new MainPageViewModel();
            }
        }

        public ChannelPageViewModel ChannelPageViewModel
        {
            get
            {
                return new ChannelPageViewModel();
            }
        }

        public ItemPageViewModel ItemPageViewModel
        {
            get
            {
                return new ItemPageViewModel();
            }
        }
    }
}
