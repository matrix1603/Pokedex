using PokeDex.FunTranslations.Provider.Messaging;
using PokeDex.Services.Pokemons.DTO;

namespace PokeDex.Services.Pokemons.Mapper
{
	public class TranslationMapper
	{
		public static GetTranslationDto ConvertToGetTranslationDto(TranslateResponse translateResponse,
			GetPokemonDto getPokemonDto)
		{
			return new GetTranslationDto
			{
				Name = getPokemonDto.Name,
				IsLegendary = getPokemonDto.IsLegendary,
				Habitat = getPokemonDto.Habitat,
				Description = translateResponse.Translation.Contents.Translated,
			};
		}
	}
}
