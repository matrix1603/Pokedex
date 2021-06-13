namespace PokeDex.Services.Pokemons.DTO
{
	public class GetTranslationDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Habitat { get; set; }
		public bool IsLegendary { get; set; }
	}
}
