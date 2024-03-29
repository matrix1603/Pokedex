﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PokeDex.Services;
using PokeDex.Services.Pokemons;
using PokeDex.Services.Pokemons.DTO;
using PokeDex.Web.API.Controllers;

namespace PokeDex.Web.API.Tests.Controllers
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private PokemonController _pokemonController;
        private Mock<IPokemonService> _mockPokemonService;

        [SetUp]
        public void Setup()
        {
            _mockPokemonService = new Mock<IPokemonService>();
            _pokemonController = new PokemonController(_mockPokemonService.Object);
        }

        [Test]
        public async Task Get_WhenPokemonNameIsNull_ReturnBadRequestObjectResult()
        {
            // Arrange
            var pokemonName = "";
            var pokemonServiceResult = new ServiceResult<GetPokemonDto>()
            {
                Errors = new List<string> { "Pokemon Name is required" }
            };

            SetUpGetPokemonAsync(pokemonServiceResult);

            // Act
            var actionResult = await _pokemonController.Get(pokemonName);

            // Assert
            _mockPokemonService.Verify(x => x.GetPokemonAsync(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        [Test]
        public async Task Get_WhenAValidNamePassed_ShouldReturnPokemonObject()
        {
            // Arrange
            var pokemonName = "mewtwo";
            var pokemonServiceResult = GetGetPokemonAsyncServiceResult(pokemonName);
            SetUpGetPokemonAsync(pokemonServiceResult);

            // Act
            var actionResult = await _pokemonController.Get(pokemonName) as OkObjectResult;

            // Assert
            _mockPokemonService.Verify(x => x.GetPokemonAsync(It.IsAny<string>()), Times.Once);

            Assert.IsInstanceOf<GetPokemonDto>(actionResult.Value);
            
            var pokemonObject = (GetPokemonDto) actionResult.Value;
            Assert.AreEqual(pokemonObject.Name, pokemonName, "Name should not be empty");
            Assert.AreEqual(pokemonObject.IsLegendary, true, "IsLegendary should be true");
            Assert.AreEqual(pokemonObject.Habitat, pokemonServiceResult.Entity.Habitat, $"Habitat should be {pokemonServiceResult.Entity.Habitat}");
            Assert.AreEqual(pokemonObject.Description, pokemonServiceResult.Entity.Description, $"Description should be {pokemonServiceResult.Entity.Description}");
        }

        [Test]
        public async Task Translated_WhenPokemonNameIsNull_ReturnBadRequestObjectResult()
        {
            // Arrange
            var pokemonName = "";
            var pokemonServiceResult = new ServiceResult<GetTranslationDto>()
            {
                Errors = new List<string> { "Pokemon Name is required" }
            };

            SetUpTranslatedAsync(pokemonServiceResult);

            // Act
            var actionResult = await _pokemonController.Translated(pokemonName);

            // Assert
            _mockPokemonService.Verify(x => x.TranslatedAsync(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        [Test]
        public async Task Translated_WhenAValidNamePassed_ShouldReturnPokemonObject()
        {
            // Arrange
            var pokemonName = "mewtwo";
            var pokemonServiceResult = GetTranslatedAsyncServiceResult(pokemonName);
            SetUpTranslatedAsync(pokemonServiceResult);

            // Act
            var actionResult = await _pokemonController.Translated(pokemonName) as OkObjectResult;

            // Assert
            _mockPokemonService.Verify(x => x.TranslatedAsync(It.IsAny<string>()), Times.Once);

            Assert.IsInstanceOf<GetTranslationDto>(actionResult.Value);

            var pokemonObject = (GetTranslationDto)actionResult.Value;
            Assert.AreEqual(pokemonObject.Name, pokemonName, "Name should not be empty");
            Assert.AreEqual(pokemonObject.IsLegendary, true, "IsLegendary should be true");
            Assert.AreEqual(pokemonObject.Habitat, pokemonServiceResult.Entity.Habitat, $"Habitat should be {pokemonServiceResult.Entity.Habitat}");
            Assert.AreEqual(pokemonObject.Description, pokemonServiceResult.Entity.Description, $"Description should be {pokemonServiceResult.Entity.Description}");
        }

        private void SetUpGetPokemonAsync(ServiceResult<GetPokemonDto> serviceResult)
        {
            _mockPokemonService.Setup(x => x.GetPokemonAsync(It.IsAny<string>())).ReturnsAsync(serviceResult);
        }

        private void SetUpTranslatedAsync(ServiceResult<GetTranslationDto> serviceResult)
        {
            _mockPokemonService.Setup(x => x.TranslatedAsync(It.IsAny<string>())).ReturnsAsync(serviceResult);
        }

        private ServiceResult<GetPokemonDto> GetGetPokemonAsyncServiceResult(string pokemonName)
        {
            return new ServiceResult<GetPokemonDto>()
            {
                Entity = new GetPokemonDto
                {
                    Name = pokemonName,
                    Description = "It was created by a scientist after years of horrific gene-splicing and DNA-engineering experiments.",
                    Habitat = "cave",
                    IsLegendary = true
                }
            };
        }

        private ServiceResult<GetTranslationDto> GetTranslatedAsyncServiceResult(string pokemonName)
        {
            return new ServiceResult<GetTranslationDto>()
            {
                Entity = new GetTranslationDto
                {
                    Name = pokemonName,
                    Description = "It was created by a scientist after years of horrific gene-splicing and DNA-engineering experiments.",
                    Habitat = "cave",
                    IsLegendary = true
                }
            };
        }
    }
}
