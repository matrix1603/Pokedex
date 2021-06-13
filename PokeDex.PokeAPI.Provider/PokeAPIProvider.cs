using System.Threading.Tasks;
using PokeDex.Infrastructure.Http;
using PokeDex.PokeAPI.Provider.Messaging;

namespace PokeDex.PokeAPI.Provider
{
	public class PokeAPIProvider :IPokeAPIProvider
	{
		private const string POKEAPI_URL = "https://pokeapi.co/api/v2/";
		public IHttpRestClient _httpRestClient { get; }
		
		public PokeAPIProvider(IHttpRestClient httpRestClient)
		{
			_httpRestClient = httpRestClient;
		}

		public async Task<GetPokemonResponse> GetPokemonAsync(GetPokemonRequest request)
        {
            ////TODO
            return new GetPokemonResponse();
        }
    }
}
