using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
using Moq;
using NUnit.Framework;

namespace EEN4PB_HSZF_2024251.Test
{
    [TestFixture]
    public class RailwayLinesLogicTester
    {
        private RailwayLinesLogic logic;
        private Mock<IRailwayLinesDataProvider> mockProvider;

        [SetUp]
        public void Init()
        {
            mockProvider = new Mock<IRailwayLinesDataProvider>();

            /*
            mockProvider.Setup(r => r.GetAll()).Returns(new List<RailwayLine>()
            {
                new RailwayLine { Id = "1", LineName = "Line 1", LineNumber = "001", Services = new List<Service>
                {
                    new Service {Id = "1", From= "A", To= "B", DelayAmount = 3, RailwayLineId = "1" , TrainNumber = 1, TrainType = "Inter"},
                    new Service {Id = "2", From= "B", To= "C", DelayAmount = 1, RailwayLineId = "1" , TrainNumber = 2, TrainType = "Inter"},
                    new Service {Id = "3", From= "C", To= "D", DelayAmount = 2, RailwayLineId = "1" , TrainNumber = 3, TrainType = "Inter"}
                }},
                new RailwayLine { Id = "2", LineName = "Line 2", LineNumber = "002", Services = new List<Service>
                {
                    new Service {Id = "4", From= "A", To= "B", DelayAmount = 3, RailwayLineId = "2" , TrainNumber = 1, TrainType = "Inter"},
                    new Service {Id = "5", From= "B", To= "C", DelayAmount = 1, RailwayLineId = "2" , TrainNumber = 2, TrainType = "Inter"},
                    new Service {Id = "6", From= "C", To= "D", DelayAmount = 2, RailwayLineId = "2" , TrainNumber = 3, TrainType = "Inter"}
                }},
                new RailwayLine { Id = "3", LineName = "Line 3", LineNumber = "003", Services = new List<Service>
                {
                    new Service {Id = "7", From= "A", To= "B", DelayAmount = 3, RailwayLineId = "3" , TrainNumber = 1, TrainType = "Inter"},
                    new Service {Id = "8", From= "B", To= "C", DelayAmount = 1, RailwayLineId = "3" , TrainNumber = 2, TrainType = "Inter"},
                    new Service {Id = "9", From= "C", To= "D", DelayAmount = 2, RailwayLineId = "3" , TrainNumber = 3, TrainType = "Inter"}
                }},
            }.AsQueryable());*/

            mockProvider.Setup(m => m.GetAll()).Returns(new List<RailwayLine>()
            {
                new RailwayLine("Budapest->Vac", "120A"),
                new RailwayLine("Budapest->Szeged", "120B"),
                new RailwayLine("Budapest->Pecs", "120C"),
                new RailwayLine("Budapest->Sopron", "120D"),
                new RailwayLine("Budapest->Gyor", "120E")
            }.AsQueryable());
            logic = new RailwayLinesLogic(mockProvider.Object);
        }

        [Test]
        public void GetAllRailwayLines_ShouldReturnAllRailwayLines()
        {
            // Arrange
            var railwayLines = logic.GetAllRailwayLines();

            // Assert
            Assert.AreEqual(5, railwayLines.Count());
        }
        
        [Test]
        public void CreateRailwayLine_ShouldCallProviderCreate()
        {
            // Arrange
            var railwayLine = new RailwayLine { LineName = "Test Line", LineNumber = "123" };

            // Act
            logic.CreateRailwayLine(railwayLine);

            // Assert
            mockProvider.Verify(p => p.CreateRailwayLine(railwayLine), Times.Once);
        }

        [Test]
        public void DeleteRailwayLine_ShouldCallProviderDelete()
        {
            // Arrange
            var id = "test-id";

            // Act
            logic.DeleteRailwayLine(id);

            // Assert
            mockProvider.Verify(p => p.DeleteById(id), Times.Once);
        }

        [Test]
        public void FindById_ShouldReturnCorrectRailwayLine()
        {
            // Arrange
            var id = "test-id";
            var railwayLine = new RailwayLine { Id = id, LineName = "Test Line", LineNumber = "123" };

            mockProvider.Setup(p => p.FindById(id)).Returns(railwayLine);

            // Act
            var result = logic.FindById(id);

            // Assert
            Assert.AreEqual(railwayLine, result);
        }
        [Test]
        public void UpdateRailwayLineConsole_ShouldCallProviderUpdateRailwayLineNameAndNumber()
        {
            // Arrange
            var id = "test-id";
            var lineName = "Test Line";
            var lineNumber = "123";

            // Act
            logic.UpdateRailwayLineConsole(id, lineName, lineNumber);

            // Assert
            mockProvider.Verify(p => p.UpdateRailwayLineNameAndNumber(It.IsAny<RailwayLine>(), lineName, lineNumber), Times.Once);
        }
        [Test]
        public void CreateRailwayLinesConsole_ShouldCallProviderCreateRailwayLine()
        {
            // Arrange
            var lineName = "Test Line";
            var lineNumber = "123";

            // Act
            logic.CreateRailwayLinesConsole(lineName, lineNumber);

            // Assert
            mockProvider.Verify(p => p.CreateRailwayLine(It.IsAny<RailwayLine>()), Times.Once);
        }
        [Test]
        public void AlreadyExists_ShouldReturnTrue()
        {
            // Arrange
            var railwayLine = new RailwayLine { LineName = "Test Line", LineNumber = "123" }; //true bc both are the same
            mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>
            {
                new RailwayLine { LineName = "Test Line", LineNumber = "123" }
            }.AsQueryable());

            // Act
            var result = logic.AlreadyExists(railwayLine);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void AlreadyExists_ShouldReturnTrue2()
        {
            // Arrange
            var railwayLine = new RailwayLine { LineName = "Test Line", LineNumber = "1234" }; //true bc one of them is different
            mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>
            {
                new RailwayLine { LineName = "Test Line", LineNumber = "123" }
            }.AsQueryable());

            // Act
            var result = logic.AlreadyExists(railwayLine);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void AlreadyExists_ShouldReturnFalse()
        {
            // Arrange
            var railwayLine = new RailwayLine { LineName = "Tests Line", LineNumber = "123" }; //both have to be different
            mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>
            {
                new RailwayLine { LineName = "Test Line", LineNumber = "124" }
            }.AsQueryable());

            // Act
            var result = logic.AlreadyExists(railwayLine);

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void FillDatabase_ShouldCallProviderFillDatabase()
        {
            // Arrange
            var path = "test-path";

            // Act
            logic.FillDatabase(path);

            // Assert
            mockProvider.Verify(p => p.FillDatabase(path), Times.Once);
        }
        [Test]
        public void GetAllServices_ShouldReturnAllServices()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service {Id = "1", From= "A", To= "B", DelayAmount = 3, RailwayLineId = "1" , TrainNumber = 1, TrainType = "Inter"},
                new Service {Id = "2", From= "B", To= "C", DelayAmount = 1, RailwayLineId = "1" , TrainNumber = 2, TrainType = "Inter"},
                new Service {Id = "3", From= "C", To= "D", DelayAmount = 2, RailwayLineId = "1" , TrainNumber = 3, TrainType = "Inter"}
            };

            mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>
            {
                new RailwayLine { Id = "1", Services = new List<Service> { services[0], services[1], services[2] } },
            }.AsQueryable());

            // Act
            var result = logic.GetAllServices();

            // Assert
            Assert.AreEqual(services, result);
        }
        [Test]
        public void ServicesLessThan5_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service {Id = "1", From= "A", To= "B", DelayAmount = 3, RailwayLineId = "1" , TrainNumber = 1, TrainType = "Inter"},
                new Service {Id = "2", From= "B", To= "C", DelayAmount = 1, RailwayLineId = "1" , TrainNumber = 2, TrainType = "Inter"},
                new Service {Id = "3", From= "C", To= "D", DelayAmount = 2, RailwayLineId = "1" , TrainNumber = 3, TrainType = "Inter"}
            };

            mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>
            {
                new RailwayLine { Id = "1", LineName = "Line 1", LineNumber = "001", Services = services },
            }.AsQueryable());

            // Act
            var result = logic.ServicesLessThan5();

            // Assert
            Assert.AreEqual("001 - Line 1:\n\thas 3 services that delayed less than 5 minutes.\n", result[0]);
        }
        [Test]
        public void AverageDelays_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service {Id = "1", From= "A", To= "B", DelayAmount = 3, RailwayLineId = "1" , TrainNumber = 1, TrainType = "Inter"},
                new Service {Id = "2", From= "B", To= "C", DelayAmount = 1, RailwayLineId = "1" , TrainNumber = 2, TrainType = "Inter"},
                new Service {Id = "3", From= "C", To= "D", DelayAmount = 2, RailwayLineId = "1" , TrainNumber = 3, TrainType = "Inter"}
            };

            mockProvider.Setup(p => p.GetAll()).Returns(new List<RailwayLine>
            {
                new RailwayLine { Id = "1", LineName = "Line 1", LineNumber = "001", Services = services },
            }.AsQueryable());

            // Act
            var result = logic.AverageDelays();

            var shouldReturn = $"001 - Line 1:" +
                        $"\n\tAverage delay: 2 minutes" +
                        $"\n\tLeast delayed service:" +
                        $"\n\t\tFrom: B" +
                        $"\n\t\tTo: C" +
                        $"\n\t\tTrain Number: 2" +
                        $"\n\t\tDelay: 1 minutes" +
                        $"\n\t\tTrain Type: Inter" +
                        $"\n\tMost delayed service:" +
                        $"\n\t\tFrom: A" +
                        $"\n\t\tTo: B" +
                        $"\n\t\tTrain Number: 1" +
                        $"\n\t\tDelay: 3 minutes" +
                        $"\n\t\tTrain Type: Inter\n";

            // Assert
            Assert.AreEqual(shouldReturn, result[0]);
        }        
    }
}
