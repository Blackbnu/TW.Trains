using System.Collections.Generic;
using System.Linq;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices.DistanceFinders
{
    public class NumberOfRoutesWithDistanceLimitFinder : ShortestRoutesFinder
    {
        private int distanceLimit;
        public virtual int GetNumberOfRoutesWithDistanceLimit(List<Track> railRoad, City origin, City destination, int distanceLimit)
        {
            this.distanceLimit = distanceLimit;
            var possibleRoutes = FindAllPossibleRoutes(railRoad, origin, destination);

            if (possibleRoutes.Count() == 0)
                throw new NoRouteException();

            return possibleRoutes.Where(x => x.DistanceFromStart < distanceLimit).Count();
        }

        protected override bool ShouldKeepRunningInThisRoute(RouteWithDistanceFromStart runningRoute, Track track)
        {
            return !(runningRoute.DistanceFromStart >= distanceLimit);
        }
    }
}