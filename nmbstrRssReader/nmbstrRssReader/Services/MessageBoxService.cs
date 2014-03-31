using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using nmbstrRssReader.Services.Interfaces;

namespace nmbstrRssReader.Services
{
    public class MessageBoxService<T, TK> : IMessageBoxService<T, TK> where T : UserControl, IMessageBox<TK>, new()
    {
        private T _control;
        private PhoneApplicationFrame _frame;

        private bool _isOpened;
        private TaskCompletionSource<TK> _completionSource;

        public async Task<TK> ShowAsync(PhoneApplicationFrame rootFrame, params object[] prm)
        {
            _isOpened = true;
            _frame = rootFrame;
            var page = _frame.Content as PhoneApplicationPage;
            page.BackKeyPress += PageOnBackKeyPress_Callback;

            _control = new T();

            var grid = System.Windows.Media.VisualTreeHelper.GetChild(page, 0) as Grid;
            Canvas.SetZIndex(_control, 999);
            grid.Children.Add(_control);

            Grid.SetRowSpan(_control, 999);
            Grid.SetColumnSpan(_control, 999);

            var transitionIn = new SwivelTransition
                                {
                                    Mode = SwivelTransitionMode.BackwardIn
                                };

            var transition = transitionIn.GetTransition(_control);
            transition.Completed += (s, e) => transition.Stop();
            transition.Begin();

            if (page.ApplicationBar != null)
            {
                page.ApplicationBar.IsVisible = false;
            }

            var task = _control.ShowAsync(prm);
            _completionSource = new TaskCompletionSource<TK>();
            await Task.Factory.StartNew(() => Task.WaitAny(new Task[]
						{
							_completionSource.Task,
							task,
						}));
            Close();
            return task.IsCompleted ? task.Result : _completionSource.Task.Result;
        }
        private void PageOnBackKeyPress_Callback(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            _completionSource.SetResult(default(TK));
        }

        private void Remove()
        {
            var page = _frame.Content as PhoneApplicationPage;
            var grid = System.Windows.Media.VisualTreeHelper.GetChild(page, 0) as Grid;

            page.BackKeyPress -= PageOnBackKeyPress_Callback;

            // Create a transition like the regular MessageBox
            var transitionOut = new SwivelTransition
                                    {
                                        Mode = SwivelTransitionMode.BackwardOut
                                    };

            var transition = transitionOut.GetTransition(_control);
            transition.Completed += (s, e) =>
                                        {
                                            transition.Stop();
                                            grid.Children.Remove(_control);
                                            if (page.ApplicationBar != null)
                                            {
                                                page.ApplicationBar.IsVisible = true;
                                            }
                                            _isOpened = false;
                                        };
            transition.Begin();
        }

        public void Close()
        {
            if (_isOpened)
            {
                Remove();
            }
        }

        public bool IsShown
        {
            get { return _isOpened; }
        }
    }
}