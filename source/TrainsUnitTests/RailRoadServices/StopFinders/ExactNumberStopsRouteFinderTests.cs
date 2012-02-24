using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Trains.Domain;
using Trains.Domain.Data;
using Trains.Domain.RailRoadServices.StopFinders;
using TrainsUnitTests.Helper;

namespace TrainsUnitTests.RailRoadServices.StopFinders
{
    public class ExactNumberStopsRouteFinderTests
    {
        private readonly ExactNumberStopsRouteFinder routeFinder = new ExactNumberStopsRouteFinder();

        [Test]
        public void Should_Return_Zero_Routes_If_The_Origin_Is_A_Leaf()
        {
            var railRoad = GivenA.SimpleRailRoad();
            var leaf = new City("Z");
            routeFinder.FindPossibleRoutes(railRoad, leaf, new City("A"), 1).Should().Be(0);
        }

        [Test]
        public void Should_Return_Zero_Routes_If_The_Destination_Is_A_Leaf()
        {
            var railRoad = GivenA.SimpleRailRoad();
            var leaf = new City("Z");
            routeFinder.FindPossibleRoutes(railRoad, new City("A"), leaf, 1).Should().Be(0);
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Null()
        {
            Action act = () => routeFinder.FindPossibleRoutes(null, new City("A"), new City("A"), 1);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Empty()
        {
            Action act = () => routeFinder.FindPossibleRoutes(new List<Track>(), new City("A"), new City("A"), 1);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Find_A_Single_Possible_Route_On_Simple_RailRoad_With_A_Single_Stop()
        {
            var railroad = GivenA.SimpleRailRoad();
            var possibleRoutes = routeFinder.FindPossibleRoutes(railroad, new City("A"), new City("B"), 1);
            possibleRoutes.Should().Be(1);
        }

        [Test]
        public void Should_Find_All_The_Possible_Routes_From_A_To_C_With_Exactly_4_Stops()
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            var possibleRoutes = routeFinder.FindPossibleRoutes(railroad, new City("A"), new City("C"), 4);
            possibleRoutes.Should().Be(3);
        }
    }
}