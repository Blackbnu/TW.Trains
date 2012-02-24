using Trains.Domain.Data;
using Trains.Domain.Factories;
using Trains.Domain.RailRoadServices;
using Trains.Domain.RailRoadServices.DistanceFinders;
using Trains.Domain.RailRoadServices.StopFinders;

namespace Trains.Domain
{
    public class RailRoadService
    {
        private readonly RailRoadFactory railRoadFactory;
        private readonly RailRoadIOService railRoadIoService;
        private readonly DistanceEvaluator distanceEvaluator;
        private readonly LimitedStopsRouteFinder limitedStopsRouteFinder;
        private readonly ExactNumberStopsRouteFinder exactNumberStopsRouteFinder;
        private readonly DistanceOfShortestRouteFinder shortestRoutesFinder;
        private readonly NumberOfRoutesWithDistanceLimitFinder numberOfRoutesWithDistanceLimitFinder;
        
        public RailRoadService(RailRoadFactory railRoadFactory, RailRoadIOService railRoadIoService, DistanceEvaluator distanceEvaluator, LimitedStopsRouteFinder limitedStopsRouteFinder, ExactNumberStopsRouteFinder exactNumberStopsRouteFinder, DistanceOfShortestRouteFinder shortestRoutesFinder, NumberOfRoutesWithDistanceLimitFinder numberOfRoutesWithDistanceLimitFinder)
        {
            this.railRoadFactory = railRoadFactory;
            this.railRoadIoService = railRoadIoService;
            this.distanceEvaluator = distanceEvaluator;
            this.limitedStopsRouteFinder = limitedStopsRouteFinder;
            this.exactNumberStopsRouteFinder = exactNumberStopsRouteFinder;
            this.shortestRoutesFinder = shortestRoutesFinder;
            this.numberOfRoutesWithDistanceLimitFinder = numberOfRoutesWithDistanceLimitFinder;
        }

        public void Run()
        {
            var input = railRoadIoService.GetInput();
            var railroad = railRoadFactory.Create(input);

            railRoadIoService.OutPut(1, () => distanceEvaluator.MeasureTheDistance(railroad, Route.Parse("A-B-C")));
            railRoadIoService.OutPut(2, () => distanceEvaluator.MeasureTheDistance(railroad, Route.Parse("A-D")));
            railRoadIoService.OutPut(3, () => distanceEvaluator.MeasureTheDistance(railroad, Route.Parse("A-D-C")));
            railRoadIoService.OutPut(4, () => distanceEvaluator.MeasureTheDistance(railroad, Route.Parse("A-E-B-C-D")));
            railRoadIoService.OutPut(5, () => distanceEvaluator.MeasureTheDistance(railroad, Route.Parse("A-E-D")));
            railRoadIoService.OutPut(6, () => limitedStopsRouteFinder.Find(railroad, new City("C"), new City("C"), 3));
            railRoadIoService.OutPut(7, () => exactNumberStopsRouteFinder.FindPossibleRoutes(railroad, new City("A"), new City("C"), 4));
            railRoadIoService.OutPut(8, () => shortestRoutesFinder.GetDistanceOfShortestRoute(railroad, new City("A"), new City("C")));
            railRoadIoService.OutPut(9, () => shortestRoutesFinder.GetDistanceOfShortestRoute(railroad, new City("B"), new City("B")));
            railRoadIoService.OutPut(10, () => numberOfRoutesWithDistanceLimitFinder.GetNumberOfRoutesWithDistanceLimit(railroad, new City("C"), new City("C"), 30));
        }
    }
}