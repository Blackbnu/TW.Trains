using System.Collections.Generic;
using System.Linq;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices.StopFinders
{
    public class LimitedStopsRouteFinder : RouteFinder
    {
        private int maximumStops;

        protected override bool ThisRouteMatchWithTheRequisites(Route route)
        {
            return route.Cities.Last().Equals(FinalDestination);
        }
        protected override bool ShouldKeepRunningInThisRoute(Route route)
        {
            return route.TotalStops < maximumStops;
        }

        public virtual int Find(List<Track> railroad, City origin, City finaldestination, int maxStops)
        {
            maximumStops = maxStops;
            return FindNumberOfPossibleRoutes(railroad, origin, finaldestination);
        }
    }
}