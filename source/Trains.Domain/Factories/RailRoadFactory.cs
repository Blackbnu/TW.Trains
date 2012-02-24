using System.Collections.Generic;
using System.Linq;
using Trains.Domain.Data;

namespace Trains.Domain.Factories
{
    public class RailRoadFactory
    {
        public virtual List<Track> Create(string input)
        {
            var tracks = input.Split(',');
            return tracks.Select(track => Track.Parse(track.Trim())).ToList();
        }
    }
}