using System.Threading.Tasks;
using PokeDex.FunTranslations.Provider.Messaging;
using PokeDex.Infrastructure.Http;

namespace PokeDex.FunTranslations.Provider
{
	public class FunTranslationsProvider :IFunTranslationsProvider
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

			//TODO
			return response;
		}
	}
}
