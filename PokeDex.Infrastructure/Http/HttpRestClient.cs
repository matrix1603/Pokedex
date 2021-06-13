using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PokeDex.Infrastructure.Http
{
	public class HttpRestClient : IHttpRestClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		
        public HttpRestClient(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
        
        public async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
        {
			throw new NotImplementedException();
		}
	}
}
