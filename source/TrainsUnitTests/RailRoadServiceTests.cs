using Moq;
using NUnit.Framework;
using Trains.Domain;
using Trains.Domain.Data;
using Trains.Domain.Factories;
using Trains.Domain.RailRoadServices;
using Trains.Domain.RailRoadServices.DistanceFinders;
using Trains.Domain.RailRoadServices.StopFinders;
using TrainsUnitTests.Helper;

namespace TrainsUnitTests
{
    public class RailRoadServiceTests
    {
        private Mock<RailRoadFactory> railRoadFactoryMock;
        private Mock<RailRoadIOService> railRoadIOServiceMock;
        private RailRoadService railRoadService;
        private Mock<DistanceEvaluator> distanceEvaluator;
        private Mock<LimitedStopsRouteFinder> limitedStopsRouteFinderMock;
        private Mock<ExactNumberStopsRouteFinder> exactNuberStopsRouteFinderMock;
        private Mock<DistanceOfShortestRouteFinder> shortestRoutFinderMock;
        private Mock<NumberOfRoutesWithDistanceLimitFinder> numberOfRoutesWithDistanceLimitFinderMock;

        [SetUp]
        public void SetUp()
        {
            railRoadFactoryMock = new Mock<RailRoadFactory>();
            railRoadIOServiceMock = new Mock<RailRoadIOService>();
            distanceEvaluator = new Mock<DistanceEvaluator>();
            limitedStopsRouteFinderMock = new Mock<LimitedStopsRouteFinder>();
            exactNuberStopsRouteFinderMock = new Mock<ExactNumberStopsRouteFinder>();
            shortestRoutFinderMock = new Mock<DistanceOfShortestRouteFinder>();
            numberOfRoutesWithDistanceLimitFinderMock = new Mock<NumberOfRoutesWithDistanceLimitFinder>();
            railRoadService = new RailRoadService(railRoadFactoryMock.Object, 
                                                  railRoadIOServiceMock.Object,
                                                  distanceEvaluator.Object, 
                                                  limitedStopsRouteFinderMock.Object,
                                                  exactNuberStopsRouteFinderMock.Object, 
                                                  shortestRoutFinderMock.Object,
                                                  numberOfRoutesWithDistanceLimitFinderMock.Object);
        }

        [Test]
        public void Should_Ask_For_An_Input()
        {
            railRoadService.Run();
            railRoadIOServiceMock.Verify(x=>x.GetInput(), Times.Once());
        }

        [Test]
        public void Should_Create_A_RailRoad_Using_The_Input_From_The_Provider()
        {
            const string input = "any input";
            railRoadIOServiceMock.Setup(x => x.GetInput()).Returns(input);

            railRoadService.Run();
            railRoadFactoryMock.Verify(x => x.Create(input), Times.Once());
        }

        [TestCase("A-B-C")]
        [TestCase("A-D")]
        [TestCase("A-D-C")]
        [TestCase("A-E-B-C-D")]
        [TestCase("A-E-D")]
        public void Should_Ask_For_Distance_Measurment_Of_The_Predetermined_Routes(string route)
        {
            var railRoad = GivenA.SimpleRailRoad();
            railRoadFactoryMock.Setup(x => x.Create(It.IsAny<string>())).Returns(railRoad);
            
            railRoadService.Run();
            
            distanceEvaluator.Verify(x=>x.MeasureTheDistance(railRoad, Route.Parse(route)), Times.Once());
        }

        [Test]
        public void Should_Ask_To_Discover_The_Number_Of_Trips_From_C_To_C_With_A_Limit_Of_3_Stops()
        {
            var railRoad = GivenA.SimpleRailRoad();
            railRoadFactoryMock.Setup(x => x.Create(It.IsAny<string>())).Returns(railRoad);

            railRoadService.Run();

            limitedStopsRouteFinderMock.Verify(x => x.Find(railRoad, new City("C"), new City("C"), 3), Times.Once());
        }
        
        [Test]
        public void Should_Ask_To_Discover_The_Number_Of_Trips_From_A_To_C_With_Exactly_4_Stops()
        {
            var railRoad = GivenA.SimpleRailRoad();
            railRoadFactoryMock.Setup(x => x.Create(It.IsAny<string>())).Returns(railRoad);

            railRoadService.Run();

            exactNuberStopsRouteFinderMock.Verify(x => x.FindPossibleRoutes(railRoad, new City("A"), new City("C"), 4), Times.Once());
        }

        [TestCase("A", "C")]
        [TestCase("B", "B")]
        public void Should_Ask_For_The_Length_Of_Shortest_Route_From_City_To_City(string origin, string destination)
        {
            var railRoad = GivenA.SimpleRailRoad();
            railRoadFactoryMock.Setup(x => x.Create(It.IsAny<string>())).Returns(railRoad);

            railRoadService.Run();

            shortestRoutFinderMock.Verify(x=>x.GetDistanceOfShortestRoute(railRoad, new City(origin), new City(destination)), Times.Once());
        }

        [Test]
        public void Should_Ask_To_Discover_The_Number_Of_Different_Routes_From_C_To_C_With_A_Distance_Less_Than_30()
        {
            var railRoad = GivenA.RailRoadWithMultipleTracks();
            railRoadFactoryMock.Setup(x => x.Create(It.IsAny<string>())).Returns(railRoad);

            railRoadService.Run();
            numberOfRoutesWithDistanceLimitFinderMock.Verify(x => x.GetNumberOfRoutesWithDistanceLimit(railRoad, new City("C"), new City("C"), 30), Times.Once());
        }
    }
}