using System;
using System.Threading.Tasks;
using PokeDex.PokeAPI.Provider;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.Services.Pokemons.DTO;
using PokeDex.Services.Pokemons.Mapper;

namespace PokeDex.Services.Pokemons
{
	public class PokemonService : IPokemonService
	{

		private readonly IPokeAPIProvider _pokeApiProvider;

		public PokemonService(IPokeAPIProvider pokeApiProvider)
		{
			_pokeApiProvider = pokeApiProvider;
		}
		public async Task<ServiceResult<GetPokemonDto>> GetPokemonAsync(string pokemonName)
		{
			var result = new ServiceResult<GetPokemonDto>();

			try
			{
				if (string.IsNullOrWhiteSpace(pokemonName))
				{
					result.Errors.Add("Pokemon Name is required");
					return result;
				}

				var	pokemonResponse = await _pokeApiProvider.GetPokemonAsync(new GetPokemonRequest { Name = pokemonName });

				result.Entity = PokemonMapper.ConvertToGetPokemonDto(pokemonResponse);
            }
			catch (Exception e)
			{
				// log
				result.Errors.Add($"Unable to find pokemon '{pokemonName}'");
			}

			return result;
		}
	}
}
