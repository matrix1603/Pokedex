using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeDex.Services.Pokemons;

namespace PokeDexWeb.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PokemonController : ControllerBase
	{
		private readonly IPokemonService _pokemonService;
		
		public PokemonController(IPokemonService pokemonService)
		{
			_pokemonService = pokemonService;
		}

		[HttpGet]
		[Route("{pokemonName}")]
		public async Task<IActionResult> Get(string pokemonName)
		{
			//// TODO log request parameters

			var result = await _pokemonService.GetPokemonAsync(pokemonName);

			if (result.HasError)
			{
				return BadRequest(result.ErrorMessage);
			}

			return Ok(result.Entity);
		}
    }
}
