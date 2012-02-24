using System;

namespace Trains.Domain.Data
{
    public class Track
    {
        public Track(City origin, City destination, int distance)
        {
            Origin = origin;
            Destination = destination;
            Distance = distance;
        }

        public City Origin { get; private set; }
        public City Destination { get; private set; }
        public int Distance { get; private set; }

        public static Track Parse(string track)
        {
            try
            {
                var origin = new City(track[0].ToString());
                var destination = new City(track[1].ToString());
                var distance = Convert.ToInt32(track.Substring(2, track.Length - 2));
                return new Track(origin, destination, distance);
            }
            catch (Exception e)
            {
                throw new FormatException("The input route is incorrect.", e);
            }
        }

        public bool IsAConnectionBetween(City origin, City destination)
        {
            return Origin.Equals(origin) && Destination.Equals(destination);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var track = (Track)obj;
            return track.Destination.Equals(Destination) &&
                         track.Distance.Equals(Distance) &&
                         track.Origin.Equals(Origin);
        }
    }
}