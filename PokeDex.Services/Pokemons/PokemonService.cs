using System;
using System.Threading.Tasks;
using PokeDex.FunTranslations.Provider;
using PokeDex.FunTranslations.Provider.Messaging;
using PokeDex.PokeAPI.Provider;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.PokeAPI.Provider.Models;
using PokeDex.Services.Pokemons.DTO;
using PokeDex.Services.Pokemons.Mapper;

namespace PokeDex.Services.Pokemons
{
	public class PokemonService : IPokemonService
	{
        private const string HABITAT = "cave";

        private readonly IPokeAPIProvider _pokeApiProvider;
        private readonly IFunTranslationsProvider _funTranslationsProvider;

        public PokemonService(IPokeAPIProvider pokeApiProvider, IFunTranslationsProvider funTranslationsProvider)
        {
            _pokeApiProvider = pokeApiProvider;
            _funTranslationsProvider = funTranslationsProvider;
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

        public async Task<ServiceResult<GetTranslationDto>> TranslatedAsync(string pokemonName)
        {
            var result = new ServiceResult<GetTranslationDto>();

            try
            {
                var pokemonResult = await GetPokemonAsync(pokemonName);

                if (pokemonResult.HasError)
                {
                    result.Errors.Add(pokemonResult.ErrorMessage);
                    return result;
                }

                var translateRequest = BuildTranslateRequest(pokemonResult.Entity);

                var response = await _funTranslationsProvider.TranslateAsync(translateRequest);

                if (response.Translation.Contents.Translation != translateRequest.Translator)
                {
                    result.Errors.Add($"Translator does not match, '{response.Translation.Contents.Translation}'");
                    return result;
                }

                if (response.Translation.Success != null && response.Translation.Success.Total > 0)
                {
                    result.Entity = TranslationMapper.ConvertToGetTranslationDto(response, pokemonResult.Entity);

                    return result;
                }

                result.Errors.Add($"Error Code '{response.Translation.Error.Code}', Error Message: '{response.Translation.Error.Message}'");

                return result;
            }
            catch (Exception e)
            {
                // log
                result.Errors.Add("");
            }

            return result;
        }
        private TranslateRequest BuildTranslateRequest(GetPokemonDto pokemon)
        {
            var translateRequest = new TranslateRequest { Text = pokemon.Description };

            if (pokemon.Habitat == HABITAT || pokemon.IsLegendary)
            {
                translateRequest.Translator = TranslatorConstants.Yoda;
            }
            else
            {
                translateRequest.Translator = TranslatorConstants.Shakespeare;
            }

            return translateRequest;
        }
	}
}
