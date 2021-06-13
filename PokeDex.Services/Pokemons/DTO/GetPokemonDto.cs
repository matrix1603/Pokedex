using System;
using System.Collections.Generic;
using System.Text;
using PokeDex.PokeAPI.Provider.Models;

namespace PokeDex.Services.Pokemons.DTO
{
	public class GetPokemonDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Habitat { get; set; }
		public bool IsLegendary { get; set; }
	}
}
