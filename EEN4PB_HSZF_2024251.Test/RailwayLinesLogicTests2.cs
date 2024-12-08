using Xunit;
using Moq;
using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
using System.Collections.Generic;
using System.Linq;

namespace EEN4PB_HSZF_2024251.Tests
{
    public class RailwayLinesLogicTests
    {
        private readonly Mock<IRailwayLinesDataProvider> _mockProvider;
        private readonly RailwayLinesLogic _railwayLinesLogic;

        public RailwayLinesLogicTests()
        {
            _mockProvider = new Mock<IRailwayLinesDataProvider>();
            _railwayLinesLogic = new RailwayLinesLogic(_mockProvider.Object);
        }

        [Fact]
        public void CreateRailwayLine_ShouldCallProviderCreate()
        {
            // Arrange
            var railwayLine = new RailwayLine { LineName = "Test Line", LineNumber = "123" };

            // Act
            _railwayLinesLogic.CreateRailwayLine(railwayLine);

            // Assert
            _mockProvider.Verify(p => p.CreateRailwayLine(railwayLine), Times.Once);
        }

        [Fact]
        public void DeleteRailwayLine_ShouldCallProviderDeleteById()
        {
            // Arrange
            var id = "test-id";

            // Act
            _railwayLinesLogic.DeleteRailwayLine(id);

            // Assert
            _mockProvider.Verify(p => p.DeleteById(id), Times.Once);
        }

        [Fact]
        public void GetAllRailwayLines_ShouldReturnAllRailwayLines()
        {
            // Arrange
            var railwayLines = new List<RailwayLine>
            {
                new RailwayLine { LineName = "Line 1", LineNumber = "001" },
                new RailwayLine { LineName = "Line 2", LineNumber = "002" }
            }.AsQueryable();

            _mockProvider.Setup(p => p.GetAll()).Returns(railwayLines);

            // Act
            var result = _railwayLinesLogic.GetAllRailwayLines();

            // Assert
            Assert.Equal(railwayLines, result);
        }

        [Fact]
        public void FindById_ShouldReturnCorrectRailwayLine()
        {
            // Arrange
            var id = "test-id";
            var railwayLine = new RailwayLine { Id = id, LineName = "Test Line", LineNumber = "123" };

            _mockProvider.Setup(p => p.FindById(id)).Returns(railwayLine);

            // Act
            var result = _railwayLinesLogic.FindById(id);

            // Assert
            Assert.Equal(railwayLine, result);
        }

        [Fact]
        public void ServicesLessThan5_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var railwayLines = new List<RailwayLine>
            {
                new RailwayLine { Id = "1", LineName = "Line 1", LineNumber = "001", Services = new List<Service>
                {
                    new Service { DelayAmount = 3, RailwayLineId = "1" },
                    new Service { DelayAmount = 4, RailwayLineId = "1" }
                }},
                new RailwayLine { Id = "2", LineName = "Line 2", LineNumber = "002", Services = new List<Service>
                {
                    new Service { DelayAmount = 6, RailwayLineId = "2" }
                }}
            }.AsQueryable();

            _mockProvider.Setup(p => p.GetAll()).Returns(railwayLines);

            // Act
            var result = _railwayLinesLogic.ServicesLessThan5();

            // Assert
            Assert.Contains("001 - Line 1", result[0]);
            Assert.Contains("has 2 services that delayed less than 5 minutes", result[0]);
        }

        [Fact]
        public void CreateRailwayLinesConsole_ShouldCreateNewRailwayLine()
        {
            // Arrange
            var lineName = "New Line";
            var lineNumber = "123";
            var railwayLine = new RailwayLine { LineName = lineName, LineNumber = lineNumber };

            _mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>().AsQueryable());

            // Act
            _railwayLinesLogic.CreateRailwayLinesConsole(lineName, lineNumber);

            // Assert
            _mockProvider.Verify(p => p.CreateRailwayLine(It.Is<RailwayLine>(rl => rl.LineName == lineName && rl.LineNumber == lineNumber)), Times.Once);
        }

        [Fact]
        public void CreateRailwayLinesConsole_ShouldNotCreateDuplicateRailwayLine()
        {
            // Arrange
            var lineName = "Existing Line";
            var lineNumber = "123";
            var existingRailwayLine = new RailwayLine { LineName = lineName, LineNumber = lineNumber };

            _mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine> { existingRailwayLine }.AsQueryable());

            // Act
            _railwayLinesLogic.CreateRailwayLinesConsole(lineName, lineNumber);

            // Assert
            _mockProvider.Verify(p => p.CreateRailwayLine(It.IsAny<RailwayLine>()), Times.Never);
        }

        [Fact]
        public void UpdateRailwayLineConsole_ShouldUpdateRailwayLine()
        {
            // Arrange
            var id = "test-id";
            var lineName = "Updated Line";
            var lineNumber = "456";
            var railwayLine = new RailwayLine { Id = id, LineName = "Old Line", LineNumber = "123" };

            _mockProvider.Setup(p => p.FindById(id)).Returns(railwayLine);

            // Act
            _railwayLinesLogic.UpdateRailwayLineConsole(id, lineName, lineNumber);

            // Assert
            _mockProvider.Verify(p => p.UpdateRailwayLineNameAndNumber(railwayLine, lineName, lineNumber), Times.Once);
        }

        [Fact]
        public void GetAllServices_ShouldReturnAllServices()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service { From = "A", To = "B", TrainNumber = 1, DelayAmount = 5, TrainType = "Type1", RailwayLineId = "1" },
                new Service { From = "C", To = "D", TrainNumber = 2, DelayAmount = 10, TrainType = "Type2", RailwayLineId = "2" }
            };

            var railwayLines = new List<RailwayLine>
            {
                new RailwayLine { Id = "1", Services = new List<Service> { services[0] } },
                new RailwayLine { Id = "2", Services = new List<Service> { services[1] } }
            }.AsQueryable();

            _mockProvider.Setup(p => p.GetAll()).Returns(railwayLines);

            // Act
            var result = _railwayLinesLogic.GetAllServices();

            // Assert
            Assert.Equal(services, result);
        }

        [Fact]
        public void AverageDelays_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service { From = "A", To = "B", TrainNumber = 1, DelayAmount = 5, TrainType = "Type1", RailwayLineId = "1" },
                new Service { From = "C", To = "D", TrainNumber = 2, DelayAmount = 10, TrainType = "Type2", RailwayLineId = "1" }
            };

            var railwayLines = new List<RailwayLine>
            {
                new RailwayLine { Id = "1", LineName = "Line 1", LineNumber = "001", Services = services }
            }.AsQueryable();

            _mockProvider.Setup(p => p.GetAll()).Returns(railwayLines);

            // Act
            var result = _railwayLinesLogic.AverageDelays();

            // Assert
            Assert.Contains("001 - Line 1", result[0]);
            Assert.Contains("Average delay: 7.5 minutes", result[0]);
        }

        [Fact]
        public void MostDelayedDestinations_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service { From = "A", To = "B", TrainNumber = 1, DelayAmount = 5, TrainType = "Type1", RailwayLineId = "1" },
                new Service { From = "C", To = "D", TrainNumber = 2, DelayAmount = 10, TrainType = "Type2", RailwayLineId = "1" }
            };

            var railwayLines = new List<RailwayLine>
            {
                new RailwayLine { Id = "1", LineName = "Line 1", LineNumber = "001", Services = services }
            }.AsQueryable();

            _mockProvider.Setup(p => p.GetAll()).Returns(railwayLines);

            // Act
            var result = _railwayLinesLogic.MostDelayedDestinations();

            // Assert
            Assert.Contains("001 - Line 1", result[0]);
            Assert.Contains("Destination of the most delayed service: D", result[0]);
        }
    }
}
