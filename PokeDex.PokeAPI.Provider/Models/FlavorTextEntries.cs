using Newtonsoft.Json;

namespace PokeDex.PokeAPI.Provider.Models
{
	public class FlavorTextEntries
	{
		[JsonProperty("flavor_text")]
		public string FlavorText { get; set; }

		public Language Language { get; set; }
    }
}
