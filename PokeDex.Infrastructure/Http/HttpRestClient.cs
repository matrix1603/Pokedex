using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;

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
			try
			{
				await LogRequestAsync(request);

				HttpClient _client = _httpClientFactory.CreateClient();

				using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
				{
					using (HttpContent content = response.Content)
					{
						string stringResponse = await content.ReadAsStringAsync();

						if (stringResponse != null)
						{
							try
							{
								return JsonConvert.DeserializeObject<T>(stringResponse);

							}
							catch (Exception ex)
							{
								Log.Error($"Unable to Deserialize string '{stringResponse}'", ex);
								throw;
							}
						}
					}

					throw new Exception(
						$"Request failed. URL '{request.RequestUri}' , StatusCode : '{response.StatusCode}'");
				}

			}
			catch (Exception ex)
			{
				Log.Error($"SendRequestAsync failed", ex);
				throw;
			}
		}

		private async Task LogRequestAsync(HttpRequestMessage request)
		{
			var body = request.Content != null ? await request.Content.ReadAsStringAsync() : null;

			var header = JsonConvert.SerializeObject(request.Headers.ToDictionary(h => h.Key, h => h.Value));

			var log = new
			{
				Url = request.RequestUri.AbsoluteUri,
				Headers = header,
				Body = body,
				Method = request.Method.Method
			};

			Log.Debug($"Log request", log);
		}
	}
}
