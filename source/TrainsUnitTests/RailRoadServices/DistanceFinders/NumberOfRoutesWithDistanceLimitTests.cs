using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Trains.Domain;
using Trains.Domain.Data;
using Trains.Domain.RailRoadServices.DistanceFinders;
using TrainsUnitTests.Helper;

namespace TrainsUnitTests.RailRoadServices.DistanceFinders
{
    public class NumberOfRoutesWithDistanceLimitTests
    {
        private readonly NumberOfRoutesWithDistanceLimitFinder routesFinder = new NumberOfRoutesWithDistanceLimitFinder();

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Null()
        {
            Action act = () => routesFinder.GetNumberOfRoutesWithDistanceLimit(null, new City("A"), new City("A"), 120);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Empty()
        {
            Action act = () => routesFinder.GetNumberOfRoutesWithDistanceLimit(new List<Track>(), new City("A"), new City("A"), 120);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_Origin_Is_A_Leaf()
        {
            var railRoad = GivenA.SimpleRailRoad();
            Action act = () => routesFinder.GetNumberOfRoutesWithDistanceLimit(railRoad, new City("Z"), new City("A"), 120);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_Destination_Is_A_Leaf()
        {
            var railRoad = GivenA.SimpleRailRoad();
            Action act = () => routesFinder.GetNumberOfRoutesWithDistanceLimit(railRoad, new City("A"), new City("Z"), 120);
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Find_Number_Of_Routes_With_Limited_Distances_On_A_Simple_RailRoad()
        {
            var A = new City("A");
            var B = new City("B");
            var railRoad = new List<Track>();
            railRoad.Add(new Track(A, B, 5));
            railRoad.Add(new Track(A, B, 8));
            railRoad.Add(new Track(A, B, 2));
            railRoad.Add(new Track(A, B, 50));
            railRoad.Add(new Track(A, B, 21));
            railRoad.Add(new Track(A, B, 1));
            railRoad.Add(new Track(A, B, 17));
            railRoad.Add(new Track(A, B, 6));

            routesFinder.GetNumberOfRoutesWithDistanceLimit(railRoad, A, B, 6).Should().Be(3);
        }

        [Test]
        public void Should_Find_Simple_Routes_With_Limited_Distances_On_A_Complex_RailRoad()
        {
            var railRoad = GivenA.RailRoadWithMultipleTracks();

            routesFinder.GetNumberOfRoutesWithDistanceLimit(railRoad, new City("C"), new City("C"), 30).Should().Be(7);
        }
    }
}