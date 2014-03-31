using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;

namespace nmbstrRssReader.Services.Interfaces
{
    public interface IMessageBoxService<T, TK>
    {
        Task<TK> ShowAsync(PhoneApplicationFrame rootFrame, params object[] prm);
    }
}
