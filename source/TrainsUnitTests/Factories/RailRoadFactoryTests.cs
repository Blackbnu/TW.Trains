using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Trains.Domain.Data;
using Trains.Domain.Factories;

namespace TrainsUnitTests.Factories
{
    public class RailRoadFactoryTests
    {
        [Test]
        public void Should_Return_A_Not_Null_List_Of_Tracks_If_The_Input_Is_Correct()
        {
            var result = new RailRoadFactory().Create("AB5");
            result.Should().NotBeNull();
        }

        [Test]
        public void Should_Return_A_Correct_List_Of_Tracks()
        {
            var trackList = new List<Track>();
            trackList.Add(Track.Parse("AB5"));
            trackList.Add(Track.Parse("BC4"));
            trackList.Add(Track.Parse("CD8"));
            trackList.Add(Track.Parse("DC8"));
            trackList.Add(Track.Parse("DE6"));
            trackList.Add(Track.Parse("AD5"));
            
            var result = new RailRoadFactory().Create("AB5, BC4, CD8, DC8, DE6, AD5");
            result.Count.Should().Be(trackList.Count);
            foreach (var track in result)
                result.Contains(track).Should().BeTrue();
        }
    }
}