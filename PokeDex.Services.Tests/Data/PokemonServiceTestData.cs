using System.Collections.Generic;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.PokeAPI.Provider.Models;

namespace PokeDex.Services.Tests.Data
{
	public class PokemonServiceTestData
	{
		internal static GetPokemonResponse GetDefaultPokemonResponseStub(string habitat, string pokemonName, string flavorText, bool isLegendary)
		{
			return  new GetPokemonResponse
			{
				Pokemon = new Pokemon
				{
					Name = pokemonName,
					IsLegendary = isLegendary,
					Habitat = new Habitat { Name = habitat },
					FlavorTextEntries = new List<FlavorTextEntries>
						{new FlavorTextEntries {FlavorText = flavorText, Language = new Language() {Name = "en"}}}
				}
			};
		}
        internal static string GetPokemonFlavorText()
        {
            var text = "It was created by a scientist after years of horrific gene-splicing and DNA-engineering experiments.";
            return text;
        }
	}
}
