using System.Collections.Generic;
using System.IO;
using Trains.Domain.Data;

namespace TrainsUnitTests.Helper
{
    public static class GivenA
    {
        public static List<Track> RailRoadWithMultipleTracks()
        {
            var railroad = new List<Track>
                               {
                                   Track.Parse("AB5"),
                                   Track.Parse("BC4"),
                                   Track.Parse("CD8"),
                                   Track.Parse("DC8"),
                                   Track.Parse("DE6"),
                                   Track.Parse("AD5"),
                                   Track.Parse("CE2"),
                                   Track.Parse("EB3"),
                                   Track.Parse("AE7")
                               };
            return railroad;
        
        }

        public static List<City> ListOfCities()
        {
            return new List<City> {new City("A"), new City("B"), new City("D"), new City("C")};
        }

        public static List<Track> SimpleRailRoad()
        {
            var railRoad = new List<Track>
                               {
                                   Track.Parse("AB5"),
                                   Track.Parse("BA4"),
                               };
            return railRoad;
        }

        public static string InputFile { get { return Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Fixture"), "input.txt"); }
        }
    }
}