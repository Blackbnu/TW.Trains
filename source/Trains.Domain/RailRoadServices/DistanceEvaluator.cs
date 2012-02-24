using System.Collections.Generic;
using System.Linq;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices
{
    public class DistanceEvaluator
    {
        private City currentCity;
        private City nextCity;

        public virtual int MeasureTheDistance(List<Track> railRoad, Route route)
        {
            var traveledDistance = 0;
            var routeToBeMeasured = (Route) route.Clone();
            while (routeToBeMeasured.Cities.Count > 1)
            {
                EvaluateOriginAndDestination(routeToBeMeasured);
                var connection = GetTheNextTrack(railRoad);
                traveledDistance += connection.Distance;
            }
            return traveledDistance;
        }

        private Track GetTheNextTrack(IEnumerable<Track> railRoad)
        {
            Track nextTrack = railRoad.FirstOrDefault(track => track.IsAConnectionBetween(currentCity, nextCity));
            if (nextTrack == null)
                throw new NoRouteException();

            return nextTrack;
        }

        private void EvaluateOriginAndDestination(Route route)
        {
            currentCity = route.Cities.Peek();
            route.Cities.Dequeue();
            nextCity = route.Cities.Peek();
        }
    }
}