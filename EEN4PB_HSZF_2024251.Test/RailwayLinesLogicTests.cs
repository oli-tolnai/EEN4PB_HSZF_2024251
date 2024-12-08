using Xunit;
using Moq;
using System.Collections.Generic;
using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;

public class RailwayLinesLogicTests
{
    private readonly Mock<IRailwayLinesDataProvider> _mockDataProvider;
    private readonly RailwayLinesLogic _railwayLinesLogic;

    public RailwayLinesLogicTests()
    {
        _mockDataProvider = new Mock<IRailwayLinesDataProvider>();
        _railwayLinesLogic = new RailwayLinesLogic(_mockDataProvider.Object);
    }

    [Fact]
    public void GetAllRailwayLines_ShouldReturnAllRailwayLines()
    {
        // Arrange
        var railwayLines = new List<RailwayLine>
        {
            new RailwayLine { Id = 1, LineNumber = "1", LineName = "Line 1" },
            new RailwayLine { Id = 2, LineNumber = "2", LineName = "Line 2" }
        };
        _mockDataProvider.Setup(dp => dp.GetAllRailwayLines()).Returns(railwayLines);

        // Act
        var result = _railwayLinesLogic.GetAllRailwayLines();

        // Assert
        Assert.Equal(railwayLines, result);
    }

    [Fact]
    public void GetAllServices_ShouldReturnAllServices()
    {
        // Arrange
        var services = new List<Service>
        {
            new Service { Id = 1, RailwayLineId = 1, DelayAmount = 3 },
            new Service { Id = 2, RailwayLineId = 2, DelayAmount = 10 }
        };
        _mockDataProvider.Setup(dp => dp.GetAllServices()).Returns(services);

        // Act
        var result = _railwayLinesLogic.GetAllServices();

        // Assert
        Assert.Equal(services, result);
    }

    [Fact]
    public void ServicesLessThan5_ShouldReturnServicesWithLessThan5MinutesDelay()
    {
        // Arrange
        var services = new List<Service>
        {
            new Service { Id = 1, RailwayLineId = 1, DelayAmount = 3 },
            new Service { Id = 2, RailwayLineId = 2, DelayAmount = 10 }
        };
        _mockDataProvider.Setup(dp => dp.GetAllServices()).Returns(services);

        // Act
        var result = _railwayLinesLogic.ServicesLessThan5();

        // Assert
        Assert.Single(result);
        Assert.Contains(result, s => s.DelayAmount < 5);
    }
}
