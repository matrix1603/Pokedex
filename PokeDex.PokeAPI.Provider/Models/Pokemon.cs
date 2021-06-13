using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeDex.PokeAPI.Provider.Models
{
	public class Pokemon
	{
		public string Name { get; set; }
		public Habitat Habitat { get; set; }

		[JsonProperty("is_legendary")]
		public bool IsLegendary { get; set; }

		[JsonProperty("flavor_text_entries")]
		public List<FlavorTextEntries> FlavorTextEntries { get; set; }
	}
}
