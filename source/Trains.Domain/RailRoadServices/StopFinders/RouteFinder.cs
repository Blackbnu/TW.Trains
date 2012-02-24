using System.Collections.Generic;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices.StopFinders
{
    public abstract class RouteFinder
    {
        protected abstract bool ShouldKeepRunningInThisRoute(Route route);
        protected abstract bool ThisRouteMatchWithTheRequisites(Route route);
        private int possibleRoutesCounter;
        protected City FinalDestination;

        protected virtual int FindNumberOfPossibleRoutes(List<Track> railRoad, City origin, City finalDestination)
        {
            if (RailRoadHasNoTracks(railRoad))
                throw new NoRouteException();
            possibleRoutesCounter = 0;
            FinalDestination = finalDestination;
            var initialRoute = StartRoute(origin);
            FindNumberOfPossibleRoutes(railRoad, origin, initialRoute);
            return possibleRoutesCounter;
        }

        private void FindNumberOfPossibleRoutes(List<Track> railRoad, City origin, Route runningRoute)
        {
            var possibleTracks = railRoad.FindAll(track => track.Origin.Equals(origin));
            foreach (var track in possibleTracks)
            {
                var route = (Route)runningRoute.Clone();
                route.AppendCity(track.Destination);
                
                if (ThisRouteMatchWithTheRequisites(route))
                    possibleRoutesCounter++;

                if (ShouldKeepRunningInThisRoute(route))
                    FindNumberOfPossibleRoutes(railRoad, track.Destination, route);
            }
        }

        private static Route StartRoute(City origin)
        {
            var initialRoute = new Route();
            initialRoute.AppendCity(origin);
            return initialRoute;
        }

        private static bool RailRoadHasNoTracks(List<Track> railRoad)
        {
            return railRoad == null || railRoad.Count == 0;
        }
    }
}