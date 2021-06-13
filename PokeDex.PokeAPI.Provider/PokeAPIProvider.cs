using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PokeDex.Infrastructure.Http;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.PokeAPI.Provider.Models;

namespace PokeDex.PokeAPI.Provider
{
	public class PokeAPIProvider : IPokeAPIProvider
	{
		private const string POKEAPI_URL = "https://pokeapi.co/api/v2/";
		public IHttpRestClient _httpRestClient { get; }

		public PokeAPIProvider(IHttpRestClient httpRestClient)
		{
			_httpRestClient = httpRestClient;
		}

		public async Task<GetPokemonResponse> GetPokemonAsync(GetPokemonRequest request)
		{
			var webRequest = BuildWebRequest(request);

			var pokemon = await _httpRestClient.SendRequestAsync<Pokemon>(webRequest);

			var response = new GetPokemonResponse { Pokemon = pokemon };

			return response;
		}

		private HttpRequestMessage BuildWebRequest(GetPokemonRequest request)
		{
			var url = POKEAPI_URL + "pokemon-species/" + request.Name;

			var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
			requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return requestMessage;
		}
	}
}