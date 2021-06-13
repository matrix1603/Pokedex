using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PokeDex.FunTranslations.Provider.Messaging;
using PokeDex.FunTranslations.Provider.Models;
using PokeDex.Infrastructure.Http;

namespace PokeDex.FunTranslations.Provider
{
	public class FunTranslationsProvider : IFunTranslationsProvider
	{
		private const string API_URL = "https://api.funtranslations.com/";
		public IHttpRestClient _httpRestClient { get; }

		public FunTranslationsProvider(IHttpRestClient httpRestClient)
		{
			_httpRestClient = httpRestClient;
		}

		public async Task<TranslateResponse> TranslateAsync(TranslateRequest request)
		{
			var response = new TranslateResponse();

			var requestMessage = BuildWebRequest(request);

			response.Translation = await _httpRestClient.SendRequestAsync<Translation>(requestMessage); ;

			return response;
		}

		private HttpRequestMessage BuildWebRequest(TranslateRequest request)
		{
			var url = API_URL + $"translate/{request.Translator}.json";

			var postFormData = new FormUrlEncodedContent(new Dictionary<string, string> { { "text", request.Text } });

			var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
			requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			requestMessage.Content = postFormData;

			return requestMessage;
		}
	}
}