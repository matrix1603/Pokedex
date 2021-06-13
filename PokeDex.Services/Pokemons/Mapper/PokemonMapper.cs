using System.Linq;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.Services.Pokemons.DTO;

namespace PokeDex.Services.Pokemons.Mapper
{
	public class PokemonMapper
	{
		public static GetPokemonDto ConvertToGetPokemonDto(GetPokemonResponse pokemonResponse)
		{
			
			return new GetPokemonDto()
			{
				Name = pokemonResponse.Pokemon.Name,
				Habitat = pokemonResponse.Pokemon.Habitat.Name,
				IsLegendary = pokemonResponse.Pokemon.IsLegendary,
				Description = pokemonResponse.Pokemon.FlavorTextEntries.FirstOrDefault(x => x.Language.Name == "en")?.FlavorText
			};
		}
	}
}
