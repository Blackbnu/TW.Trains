using System.Collections.Generic;
using System.Linq;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices.StopFinders
{
    public class ExactNumberStopsRouteFinder : RouteFinder
    {
        private int exactNumberOfStopsExpected;

        public virtual int FindPossibleRoutes(List<Track> railRoad, City origin, City finalDestionation, int exactNumberOfStops)
        {
            exactNumberOfStopsExpected = exactNumberOfStops;
            return FindNumberOfPossibleRoutes(railRoad, origin, finalDestionation);
        }

        protected override bool ShouldKeepRunningInThisRoute(Route route)
        {
            return route.TotalStops < exactNumberOfStopsExpected;
        }

        protected override bool ThisRouteMatchWithTheRequisites(Route route)
        {
            return route.TotalStops.Equals(exactNumberOfStopsExpected) && route.Cities.Last().Equals(FinalDestination);
        }
    }
}