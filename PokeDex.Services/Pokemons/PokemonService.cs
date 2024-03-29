﻿using System;
using System.Threading.Tasks;
using PokeDex.FunTranslations.Provider;
using PokeDex.FunTranslations.Provider.Messaging;
using PokeDex.Infrastructure.Cache;
using PokeDex.PokeAPI.Provider;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.PokeAPI.Provider.Models;
using PokeDex.Services.Pokemons.DTO;
using PokeDex.Services.Pokemons.Mapper;
using Serilog;

namespace PokeDex.Services.Pokemons
{
	public class PokemonService : IPokemonService
	{
        private const string HABITAT = "cave";

        private readonly IPokeAPIProvider _pokeApiProvider;
        private readonly IFunTranslationsProvider _funTranslationsProvider;
        private readonly ICacheStorage _cacheStorage;

        public PokemonService(IPokeAPIProvider pokeApiProvider, IFunTranslationsProvider funTranslationsProvider, ICacheStorage cacheStorage)
        {
            _pokeApiProvider = pokeApiProvider;
            _funTranslationsProvider = funTranslationsProvider;
            _cacheStorage = cacheStorage;
        }
		public async Task<ServiceResult<GetPokemonDto>> GetPokemonAsync(string pokemonName)
		{
			Log.Debug($"Get GetPokemonAsync, pokemonName:'{pokemonName}'");

            var result = new ServiceResult<GetPokemonDto>();

			try
			{
				if (string.IsNullOrWhiteSpace(pokemonName))
				{
					result.Errors.Add("Pokemon Name is required");
					return result;
				}

				var pokemonResponse = _cacheStorage.Retrieve<GetPokemonResponse>(pokemonName);

				if (pokemonResponse == null)
				{
					pokemonResponse = await _pokeApiProvider.GetPokemonAsync(new GetPokemonRequest { Name = pokemonName });
					_cacheStorage.Store(pokemonName, 10, pokemonResponse);
				}

                result.Entity = PokemonMapper.ConvertToGetPokemonDto(pokemonResponse);
            }
			catch (Exception ex)
			{
				Log.Error($"GetPokemonAsync failed , pokemonName: '{pokemonName}'", ex);

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

                var translationCacheKey = $"{pokemonName}-{translateRequest.Translator}".ToLower();

                var pokemonResponse = _cacheStorage.Retrieve<GetTranslationDto>(translationCacheKey);

                if (pokemonResponse != null)
                {
	                result.Entity = pokemonResponse;
	                return result;
                }

                var response = await _funTranslationsProvider.TranslateAsync(translateRequest);

                if (response.Translation.Contents.Translation != translateRequest.Translator)
                {
                    result.Errors.Add($"Translator does not match, '{response.Translation.Contents.Translation}'");
                    return result;
                }

                if (response.Translation.Success != null && response.Translation.Success.Total > 0)
                {
                    result.Entity = TranslationMapper.ConvertToGetTranslationDto(response, pokemonResult.Entity);
                    
                    _cacheStorage.Store(translationCacheKey, 60, result.Entity);

                    return result;
                }

                result.Errors.Add($"Error Code '{response.Translation.Error.Code}', Error Message: '{response.Translation.Error.Message}'");

                return result;
            }
            catch (Exception ex)
            {
	            Log.Error($"TranslatedAsync failed , pokemonName: '{pokemonName}'", ex);

                result.Errors.Add($"Unable to translate, pokemonName : '{pokemonName}'");
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
