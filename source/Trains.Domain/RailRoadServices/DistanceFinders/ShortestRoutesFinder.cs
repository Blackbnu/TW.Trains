using System.Collections.Generic;
using Trains.Domain.Data;

namespace Trains.Domain.RailRoadServices.DistanceFinders
{
    public abstract class ShortestRoutesFinder
    {
        private List<RouteWithDistanceFromStart> possibleRoutes = new List<RouteWithDistanceFromStart>();
        protected City FinalDestination;
        protected abstract bool ShouldKeepRunningInThisRoute(RouteWithDistanceFromStart runningRoute, Track track);

        protected List<RouteWithDistanceFromStart> FindAllPossibleRoutes(List<Track> railRoad, City origin, City finalDestination)
        {
            if (RailRoadHasNoTracks(railRoad))
                throw new NoRouteException();
            FinalDestination = finalDestination;
            possibleRoutes = new List<RouteWithDistanceFromStart>();
            var initialRoute = StartRoute(origin);
            FindAllPossibleRoutes(railRoad, origin, initialRoute);
            return possibleRoutes;
        }

        private void FindAllPossibleRoutes(List<Track> railRoad, City origin, RouteWithDistanceFromStart runningRoute)
        {
            var possibleTracks = railRoad.FindAll(track => track.Origin.Equals(origin));
            foreach (var track in possibleTracks)
            {
                var route = (RouteWithDistanceFromStart)runningRoute.Clone();
                route.AppendCity(track.Destination, track.Distance);

                if (ThisTrackWillLeadMeToDestination(track))
                    possibleRoutes.Add(route);
                
                if (ShouldKeepRunningInThisRoute(runningRoute, track))
                    FindAllPossibleRoutes(railRoad, track.Destination, route);
            }
        }

        private static RouteWithDistanceFromStart StartRoute(City origin)
        {
            var initialRoute = new RouteWithDistanceFromStart();
            initialRoute.AppendCity(origin, 0);
            return initialRoute;
        }

        private bool ThisTrackWillLeadMeToDestination(Track track)
        {
            return track.Destination.Equals(FinalDestination);
        }

        private static bool RailRoadHasNoTracks(List<Track> railRoad)
        {
            return railRoad == null || railRoad.Count == 0;
        }

        protected class RouteWithDistanceFromStart
        {
            public RouteWithDistanceFromStart()
            {
                DistanceFromStart = 0;
                Cities = new Queue<City>();
            }

            public Queue<City> Cities { get; private set; }
            public void AppendCity(City city, int distance)
            {
                DistanceFromStart += distance;
                Cities.Enqueue(city);
            }

            public int DistanceFromStart { get; private set; }

            public object Clone()
            {
                var clone = new RouteWithDistanceFromStart();
                foreach (var city in Cities)
                    clone.AppendCity(city, 0);
                clone.DistanceFromStart = DistanceFromStart;
                return clone;
            }
        }
    }
}