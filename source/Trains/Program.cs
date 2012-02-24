using System;
using Trains.Domain;
using Trains.Domain.Factories;
using Trains.Domain.RailRoadServices;
using Trains.Domain.RailRoadServices.DistanceFinders;
using Trains.Domain.RailRoadServices.StopFinders;

namespace Trains
{
    public class Program
    {
        static void Main()
        {
            new RailRoadService(new RailRoadFactory(),
                                new RailRoadIOService(),
                                new DistanceEvaluator(),
                                new LimitedStopsRouteFinder(),
                                new ExactNumberStopsRouteFinder(),
                                new DistanceOfShortestRouteFinder(),
                                new NumberOfRoutesWithDistanceLimitFinder()).Run();

            Console.ReadKey();
        }
    }
}
