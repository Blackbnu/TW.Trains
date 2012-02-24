using System;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using Trains.Domain;
using Trains.Domain.Data;
using Trains.Domain.RailRoadServices.StopFinders;
using TrainsUnitTests.Helper;

namespace TrainsUnitTests.RailRoadServices.StopFinders
{
    public class LimitedStopsRouteFinderTests
    {
        [Test]
        public void ShouldReturnZeroRoutesIfTheOriginIsALeaf()
        {
            var railroad = GivenA.SimpleRailRoad();
            var routes = new LimitedStopsRouteFinder().Find(railroad, new City("C"), new City("A"), 10);
            routes.Should().Be(0);
        }

        [Test]
        public void ShouldReturnZeroRoutesIfTheDestinationIsALeaf()
        {
            var railroad = GivenA.SimpleRailRoad();
            var routes = new LimitedStopsRouteFinder().Find(railroad, new City("A"), new City("C"), 10);
            routes.Should().Be(0);
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Null()
        {
            Action act = () => new LimitedStopsRouteFinder().Find(null, new City("A"), new City("A"), 1);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Empty()
        {
            Action act = () => new LimitedStopsRouteFinder().Find(new List<Track>(), new City("A"), new City("A"), 1);
            act.ShouldThrow<NoRouteException>();
        }

        [TestCase("A", "A", 2, 1)]
        [TestCase("A", "A", 3, 1)]
        [TestCase("A", "A", 4, 2)]
        [TestCase("A", "A", 5, 2)]
        [TestCase("A", "A", 6, 3)]
        [TestCase("A", "A", 7, 3)]
        [TestCase("B", "A", 1, 1)]
        [TestCase("A", "B", 1, 1)]
        [TestCase("B", "B", 10, 5)]
        public void ShouldGetAListOfPossibleRoutesFromCityToCityInASimpleRailRoad(string origin, string destination, int maximumStops, int possibleRoutes)
        {
            var railroad = GivenA.SimpleRailRoad();
            var routes = new LimitedStopsRouteFinder().Find(railroad, new City(origin), new City(destination), maximumStops);
            routes.Should().Be(possibleRoutes);
        }

        [TestCase("C", "C", 3, 2)]
        [TestCase("C", "C", 4, 4)]
        [TestCase("C", "C", 5, 6)]
        [TestCase("A", "E", 1, 1)]
        public void ShouldGetAListOfPossibleRoutesFromCityToCityInAComplexRailRoad(string origin, string destination, int maximumStops, int possibleRoutes)
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            var routes = new LimitedStopsRouteFinder().Find(railroad, new City(origin), new City(destination), maximumStops);
            routes.Should().Be(possibleRoutes);
        }
    }
}