using System;
using NUnit.Framework;
using FluentAssertions;
using Trains.Domain;
using Trains.Domain.Data;
using Trains.Domain.RailRoadServices;
using TrainsUnitTests.Helper;

namespace TrainsUnitTests.RailRoadServices
{
    public class DistanceEvaluatorTest
    {
        [TestCase("A-B", 5)]
        [TestCase("B-C", 4)]
        [TestCase("C-D", 8)]
        [TestCase("D-C", 8)]
        [TestCase("D-E", 6)]
        [TestCase("A-D", 5)]
        [TestCase("C-E", 2)]
        [TestCase("E-B", 3)]
        [TestCase("A-E", 7)]
        public void ShouldCalcuteTheCostOfASimpleRouteFrom_CityTo_City(string routeDescription, int expectedDistance)
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            var route = Route.Parse(routeDescription);
            int distance = new DistanceEvaluator().MeasureTheDistance(railroad, route);
            distance.Should().Be(expectedDistance);
        }

        [TestCase("A-E")]
        public void ShouldNotModifyTheRouteAfterMeasureTheDistance(string routeDescription)
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            var route = Route.Parse(routeDescription);
            new DistanceEvaluator().MeasureTheDistance(railroad, route);
            Route originalRoute = Route.Parse(routeDescription);
            route.Cities.Count.Should().Be(originalRoute.Cities.Count);
        }

        [TestCase("A-B-C", 9)]
        [TestCase("A-D", 5)]
        [TestCase("A-D-C", 13)]
        [TestCase("A-E-B-C-D", 22)]
        public void ShouldCalculateTheDistanceOfRoutesWithMultipleCities(string routeDescription, int expectedDistance)
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            int distance = new DistanceEvaluator().MeasureTheDistance(railroad, Route.Parse(routeDescription));
            distance.Should().Be(expectedDistance);
        }

        [TestCase("A-E-D", true)]
        [TestCase("A-D-C", false)]
        public void ShouldThrowANoRouteExceptionIfTheRouteDoesNotExist(string routeDescription, bool shouldThrowException)
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            Action act = () => new DistanceEvaluator().MeasureTheDistance(railroad, Route.Parse(routeDescription));
            if (shouldThrowException)
                act.ShouldThrow<NoRouteException>("There is no such route!");
            else
                act.ShouldNotThrow<NoRouteException>("This route exists!");
        }
    }
}
