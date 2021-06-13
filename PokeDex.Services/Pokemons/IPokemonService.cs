using System.Threading.Tasks;
using PokeDex.Services.Pokemons.DTO;

namespace PokeDex.Services.Pokemons
{
	public interface IPokemonService
	{
		Task<ServiceResult<GetPokemonDto>> GetPokemonAsync(string pokemonName);
        Task<ServiceResult<GetTranslationDto>> TranslatedAsync(string pokemonName);
    }
}
