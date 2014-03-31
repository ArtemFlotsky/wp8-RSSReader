using System.Threading.Tasks;

namespace nmbstrRssReader.Services
{
	public interface IMessageBox<T>
	{
		Task<T> ShowAsync(params object[] prm);
	}
}
