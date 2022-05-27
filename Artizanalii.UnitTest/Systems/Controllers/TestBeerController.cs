using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Artizanalii_Api.Controllers;
using Artizanalii_Api.Entities.Beer;
using Artizanalii_Api.Repositories.Beers;
using Microsoft.AspNetCore.Mvc;

namespace Artizanalii.UnitTest.Systems.Controllers;

public class TestBeerController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrange
        var mockBeerRepository = new Mock<IBeerRepository>();
        mockBeerRepository.Setup(service => service.GetAllBeersAsync()).ReturnsAsync(new List<Beer>());
        var sut = new BeersController(mockBeerRepository.Object);
        //Act
        var result = await sut.GetAllBeersAsync();
        //Assert
        mockBeerRepository.Verify(service => service.GetAllBeersAsync(), Times.Once);
    }

    [Fact]
    public async Task Get_200Success()
    {   //Arrange
        var mockBeerRepository = new Mock<IBeerRepository>();
        mockBeerRepository.Setup(service => service.
                GetAllBeersAsync())
            .ReturnsAsync(new List<Beer>() {new Beer() {Name = "TestBeer"}});
        var sut = new BeersController(mockBeerRepository.Object);
        //Act
        var result = (OkObjectResult) await sut.GetAllBeersAsync();
        //Assert
        result.Should().BeOfType<OkObjectResult>();
        result.StatusCode.Should().Be(200);
    }
}