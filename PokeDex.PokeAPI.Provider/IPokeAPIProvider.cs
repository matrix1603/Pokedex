using System.Threading.Tasks;
using PokeDex.PokeAPI.Provider.Messaging;

namespace PokeDex.PokeAPI.Provider
{
	public interface IPokeAPIProvider
	{
		public Task<GetPokemonResponse> GetPokemonAsync(GetPokemonRequest request);
	}
}
