using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains.Domain.Data
{
    public class Route : ICloneable
    {
        public Route()
        {
            Cities = new Queue<City>();
        }

        public Queue<City> Cities { get; private set; }

        public static Route Parse(string route)
        {
            var result = new Route();

            string[] citiesIds = route.Split('-');
            foreach (string cityId in citiesIds)
                result.AppendCity(new City(cityId));

            return result;
        }

        public void AppendCity(City city)
        {
            Cities.Enqueue(city);
        }

        public object Clone()
        {
            var clone = new Route();
            foreach (var city in Cities)
                clone.AppendCity(city);
            return clone;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var route = (Route)obj;
            if (!route.Cities.Count.Equals(Cities.Count))
                return false;

            return Cities.All(city => route.Cities.Contains(city));
        }

        public override string ToString()
        {
            return string.Join("-", Cities);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int TotalStops { get { return Cities.Count - 1; } 
        }
    }
}