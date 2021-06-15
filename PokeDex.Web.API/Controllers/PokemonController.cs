using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeDex.Services.Pokemons;
using PokeDex.Services.Pokemons.DTO;
using Serilog;

namespace PokeDex.Web.API.Controllers
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
		[ProducesResponseType(typeof(GetPokemonDto), StatusCodes.Status200OK)]
		public async Task<IActionResult> Get(string pokemonName)
		{
			Log.Debug($"Get called, pokemonName: '{pokemonName}'");

			//// TODO log request parameters

			var result = await _pokemonService.GetPokemonAsync(pokemonName);

			if (result.HasError)
			{
				return BadRequest(result.ErrorMessage);
			}

			return Ok(result.Entity);
		}

        [HttpGet]
        [Route("Translated/{pokemonName}")]
        [ProducesResponseType(typeof(GetTranslationDto), StatusCodes.Status200OK)]
		public async Task<IActionResult> Translated(string pokemonName)
        {
			Log.Debug($"Translated called, pokemonName: '{pokemonName}'");

			var result = await _pokemonService.TranslatedAsync(pokemonName);

            if (result.HasError)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Entity);
        }
	}
}
