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
    public class DistanceOfShortestRouteFinderTests
    {
        private readonly DistanceOfShortestRouteFinder routesFinder = new DistanceOfShortestRouteFinder();

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Null()
        {
            Action act = () => routesFinder.GetDistanceOfShortestRoute(null, new City("A"), new City("A") );
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_RailRoad_Is_Empty()
        {
            Action act = () => routesFinder.GetDistanceOfShortestRoute(new List<Track>(), new City("A"), new City("A"));
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_Origin_Is_A_Leaf()
        {
            var railRoad = GivenA.SimpleRailRoad();
            Action act = () => routesFinder.GetDistanceOfShortestRoute(railRoad, new City("Z"), new City("A"));
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Throw_NoRouteException_If_The_Destination_Is_A_Leaf()
        {
            var railRoad = GivenA.SimpleRailRoad();
            Action act = () => routesFinder.GetDistanceOfShortestRoute(railRoad, new City("A"), new City("Z"));
            act.ShouldThrow<NoRouteException>();
        }

        [Test]
        public void Should_Find_Shortest_Track_From_A_To_B_In_A_RailRoad_With_Some_Direct_Tracks_From_A_To_B()
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

            routesFinder.GetDistanceOfShortestRoute(railRoad, A, B).Should().Be(1);
        }

        [Test]
        public void Should_Find_Shortest_Track_From_A_To_B_In_A_RailRoad_With_Some_Tracks_From_A_To_B_And_Others()
        {
            var A = new City("A");
            var B = new City("B");
            var C = new City("C");
            var D = new City("D");
            var railRoad = new List<Track>();
            railRoad.Add(new Track(A, B, 5));
            railRoad.Add(new Track(A, B, 8));
            railRoad.Add(new Track(A, B, 2));
            railRoad.Add(new Track(A, B, 50));
            railRoad.Add(new Track(A, B, 21));
            railRoad.Add(new Track(A, B, 1));
            railRoad.Add(new Track(A, B, 17));
            railRoad.Add(new Track(B, C, 1));
            railRoad.Add(new Track(B, D, 1));
            railRoad.Add(new Track(D, A, 1));
            railRoad.Add(new Track(B, A, 1));
            routesFinder.GetDistanceOfShortestRoute(railRoad, A, B).Should().Be(1);
        }

        [Test]
        public void Should_Find_Shortest_Track_From_A_Railroad_Without_A_Direct_Track_From_A_To_C()
        {
            var A = new City("A");
            var B = new City("B");
            var C = new City("C");
            var D = new City("D");
            var railRoad = new List<Track>();
            railRoad.Add(new Track(A, B, 5));
            railRoad.Add(new Track(B, A, 8));
            railRoad.Add(new Track(D, C, 2));
            railRoad.Add(new Track(B, C, 50));
            railRoad.Add(new Track(A, B, 21));
            railRoad.Add(new Track(A, B, 1));
            railRoad.Add(new Track(A, B, 17));
            railRoad.Add(new Track(B, C, 1));
            railRoad.Add(new Track(D, A, 1));
            routesFinder.GetDistanceOfShortestRoute(railRoad, A, C).Should().Be(2);
        }

        [TestCase("A", "C", 9)]
        [TestCase("B", "B", 9)]
        public void Should_Find_Shortest_Track_From_A_RailRoad_With_Multiple_Tracks(string origin, string destination, int shortestRoute)
        {
            var railroad = GivenA.RailRoadWithMultipleTracks();
            routesFinder.GetDistanceOfShortestRoute(railroad, new City(origin), new City(destination)).Should().Be(shortestRoute);
        }
    }
}