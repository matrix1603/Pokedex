using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PokeDex.FunTranslations.Provider;
using PokeDex.Infrastructure.Cache;
using PokeDex.PokeAPI.Provider;
using PokeDex.PokeAPI.Provider.Messaging;
using PokeDex.Services.Pokemons;
using PokeDex.Services.Tests.Data;

namespace PokeDex.Services.Tests
{
	[TestFixture]
	public class Tests
	{
		private IPokemonService _pokemonService;
		private Mock<IPokeAPIProvider> _mockPokeAPIProvider;
        private Mock<IFunTranslationsProvider> _mockFunTranslationsProvider;
        private Mock<ICacheStorage> _mockICacheStorage;
        
        [SetUp]
		public void Setup()
		{
			_mockPokeAPIProvider = new Mock<IPokeAPIProvider>();
            _mockFunTranslationsProvider = new Mock<IFunTranslationsProvider>();
            _mockICacheStorage = new Mock<ICacheStorage>();
            _pokemonService = new PokemonService(_mockPokeAPIProvider.Object, _mockFunTranslationsProvider.Object, _mockICacheStorage.Object);
		}

        [Test]
        public async Task GetPokemonAsync_WhenAValidPokemonNameIsPassed_PokemonObjectShouldNotBeNull()
        {
            // Arrange
            var pokemonName = "mewtwo";
            var text = PokemonServiceTestData.GetPokemonFlavorText();
            var rs = PokemonServiceTestData.GetDefaultPokemonResponseStub("cave", pokemonName, text, true);

            SetUpMockPokeAPIProvider(rs);

            // Act
            var result = await _pokemonService.GetPokemonAsync(pokemonName);

            // Assert
            _mockPokeAPIProvider.Verify(x => x.GetPokemonAsync(It.IsAny<GetPokemonRequest>()), Times.Once);

            Assert.NotNull(result.Entity);
            Assert.AreEqual(result.Entity.Name, pokemonName, "Name should not be empty");
            Assert.AreEqual(result.Entity.IsLegendary, true, "IsLegendary should be true");
            Assert.AreEqual(result.Entity.Habitat, rs.Pokemon.Habitat.Name, $"Habitat should be {rs.Pokemon.Habitat.Name}");
            Assert.AreEqual(result.Entity.Description, text, $"Description should be {text}");
        }

        [Test]
        public async Task GetPokemonAsync_WhenThereIsAnExceptionDuringRequest_AFriendlyMessageShouldBeReturned()
        {
            // Arrange
            var pokemonName = "mewtwo";

            // Act
            var result = await _pokemonService.GetPokemonAsync(pokemonName);

            // Assert
            _mockPokeAPIProvider.Verify(x => x.GetPokemonAsync(It.IsAny<GetPokemonRequest>()), Times.AtLeast(1));

            Assert.AreEqual(result.Errors.Count, 1, "Error count should be 1");
            Assert.AreEqual(result.ErrorMessage, $"Unable to find pokemon '{pokemonName}'");
        }

        [Test]
        public async Task GetPokemonAsync_WhenPokemonNameIsNull_ValidationMessageShouldBeReturned()
        {
            // Arrange
            var pokemonName = "";

            // Act
            var result = await _pokemonService.GetPokemonAsync(pokemonName);

            // Assert

            Assert.Greater(result.Errors.Count, 0);
            Assert.AreEqual(result.ErrorMessage, $"Pokemon Name is required");
        }


		private void SetUpMockPokeAPIProvider(GetPokemonResponse rs)
		{
			_mockPokeAPIProvider.Setup(x => x.GetPokemonAsync(It.IsAny<GetPokemonRequest>())).ReturnsAsync(rs);
		}
	}
}