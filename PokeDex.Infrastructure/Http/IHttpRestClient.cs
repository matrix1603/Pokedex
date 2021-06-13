using System.Net.Http;
using System.Threading.Tasks;

namespace PokeDex.Infrastructure.Http
{
	public interface IHttpRestClient
	{
		//public Task<T> GetAsync<T>(string url);
		public Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage);
	}
}
