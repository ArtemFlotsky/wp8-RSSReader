using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using nmbstrRssReader.Common;
using nmbstrRssReader.Services;

namespace nmbstrRssReader.View.Popups
{
    public partial class AddChannelPopup : UserControl, IMessageBox<AddChannelPopupResult>
    {
        private string _url;

        private List<string> Urls;

        public AddChannelPopup()
        {
            InitializeComponent();
#if DEBUG
            ChannelUrlTextBox.Text = "http://feeds.nytimes.com/nyt/rss/HomePage";
#endif
            Urls = new List<string>
            {
                "http://www.ksl.com/xml/148.rss",
                "http://le.utah.gov/rss/utleg.xml",
                "http://www.utah.gov/whatsnew/rss.xml",
                "http://feeds.feedburner.com/standard/frontpage",
                "http://www.thespectrum.com/rssfeeds/topstories.xml",
                "http://www.uen.org/feeds/rss/news.xml.php",
                "http://www2.ed.gov/rss/edgov.xml",
                "http://feeds.nytimes.com/nyt/rss/Education",
                "http://www.smartbrief.com/servlet/rss?b=ASCD",
                "http://www.pbs.org/teachers/learning.now/rss2/index.xml",
                "http://www.npr.org/rss/rss.php?id=1013",
                "http://www.techlearning.com/RSS"
            };
        }

        private TaskCompletionSource<AddChannelPopupResult> _task;
        public Task<AddChannelPopupResult> ShowAsync(params object[] prm)
        {
            _task = new TaskCompletionSource<AddChannelPopupResult>();
            return _task.Task;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _task.SetResult(new AddChannelPopupResult()
            {
                Text = _url,
                MessageBoxResult = MessageBoxResult.OK
            });
        }

        private void NoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            ChannelUrlTextBox.Text = Urls.ElementAt(rand.Next(Urls.Count - 1));
            _url = ChannelUrlTextBox.Text;
        }

        private void ChannelUrlTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            _url = ((TextBox) sender).Text;
        }
    }
}
