using System.Collections.Generic;
using System.Linq;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices.DistanceFinders
{
    public class DistanceOfShortestRouteFinder : ShortestRoutesFinder
    {
        public virtual int GetDistanceOfShortestRoute(List<Track> railRoad, City origin, City destination)
        {
            var possibleRoutes = FindAllPossibleRoutes(railRoad, origin, destination);

            if (possibleRoutes.Count() == 0)
                throw new NoRouteException();

            return possibleRoutes.OrderBy(x => x.DistanceFromStart).First(x => x.DistanceFromStart != 0).DistanceFromStart;
        }

        protected override bool ShouldKeepRunningInThisRoute(RouteWithDistanceFromStart runningRoute, Track track)
        {
            return !(runningRoute.Cities.Contains(track.Destination) && !track.Destination.Equals(FinalDestination));
        }
    }
}