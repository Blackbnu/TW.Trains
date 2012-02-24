using System;
using FluentAssertions;
using NUnit.Framework;
using Trains.Domain.Data;

namespace TrainsUnitTests.Data
{
    public class TrackTests
    {
        [TestCase("AB5", "A", "B", 5)]
        [TestCase("CB1", "C", "B", 1)]
        [TestCase("ab3", "a", "b", 3)]
        [TestCase("CD0", "C", "D", 0)]
        [TestCase("CD12", "C", "D", 12)]
        public void ATrackShouldBeCreatedFromAString(string inputRoute, string origin, string destination, int distance)
        {
            var track = Track.Parse(inputRoute);
            track.Origin.Should().Be(new City(origin));
            track.Destination.Should().Be(new City(destination));
            track.Distance.Should().Be(distance);
        }

        [TestCase("A")]
        [TestCase("AB")]
        [TestCase("ABC")]
        [TestCase("A1")]
        [TestCase("ABC1")]
        public void AnExceptionMustBeRaisedIfTheInputStringIsNotInTheCorrectFormat(string inputRoute)
        {
            Action act = () => Track.Parse(inputRoute);
            act.ShouldThrow<FormatException>().WithMessage("The input route is incorrect.");
        }

        [Test]
        public void ShouldIdentifyIfTheTrackConnectTheGivenCities()
        {
            var origin = new City("A");
            var destination = new City("B");
            var otherCity = new City("C");

            var track = new Track(origin, destination, 4);

            track.IsAConnectionBetween(origin, destination).Should().BeTrue();
            track.IsAConnectionBetween(origin, otherCity).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnTrueIfTracksAreEquals()
        {
            var aTrack = Track.Parse("AB3");
            var sameTrack = Track.Parse("AB3");
            aTrack.Equals(sameTrack).Should().BeTrue();
        }

        [Test]
        public void EqualsShouldReturnFalseIfTracksHaveDifferentDistances()
        {
            var aTrack = Track.Parse("AB3");
            var aTrackWithAnotherDistance = Track.Parse("AB2");
            aTrack.Equals(aTrackWithAnotherDistance).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnFalseIfTracksHaveDifferentOrigins()
        {
            var aTrack = Track.Parse("AB3");
            var aTrackWithAnotherDistance = Track.Parse("CB3");
            aTrack.Equals(aTrackWithAnotherDistance).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnFalseIfTracksHaveDifferentDestinations()
        {
            var aTrack = Track.Parse("AB3");
            var aTrackWithAnotherDistance = Track.Parse("AC3");
            aTrack.Equals(aTrackWithAnotherDistance).Should().BeFalse();
        }
    }
}