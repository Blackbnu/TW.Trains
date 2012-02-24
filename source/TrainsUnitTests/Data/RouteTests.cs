using FluentAssertions;
using NUnit.Framework;
using Trains.Domain.Data;
using TrainsUnitTests.Helper;

namespace TrainsUnitTests.Data
{
    public class RouteTests
    {
        [Test]
        public void ShouldAppendCitiesInTheRoute()
        {
            var route = new Route();
            var expectedCities = GivenA.ListOfCities();
            
            foreach (var city in expectedCities)
                route.AppendCity(city);

            route.Cities.Count.Should().Be(expectedCities.Count);
            foreach (var expectedCity in expectedCities)
                route.Cities.Should().Contain(expectedCity);
        }

        [Test]
        public void EqualsShouldReturnFalseForRoutesWithDiferentSizes()
        {
            var aRoute = Route.Parse("A-B-C-D-Z");
            var aSmallerRoute = Route.Parse("A-B-C-D");
            aRoute.Equals(aSmallerRoute).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnFalseForRoutesWithDiferentCitiesAndSameSize()
        {
            var aRoute = Route.Parse("A-B-C-D-Z");
            var anotherRoute = Route.Parse("A-B-C-D-X");
            aRoute.Equals(anotherRoute).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnTrueForEqualRoutes()
        {
            var aRoute = Route.Parse("A-B-C-D-Z");
            var anotherRoute = Route.Parse("A-B-C-D-Z");
            aRoute.Equals(anotherRoute).Should().BeTrue();
        }

        [Test]
        public void ToString_Should_Return_A_String_With_All_The_Cities()
        {
            var routeAsString = "A-B-C-D-Z";
            Route.Parse(routeAsString).ToString().Should().Be(routeAsString);
        }

        [TestCase("A-B", 1)]
        [TestCase("A-B-C", 2)]
        [TestCase("A-B-C-D", 3)]
        [TestCase("A-B-C-D-A", 4)]
        [TestCase("A-B-C-D-A-z", 5)]
        public void Should_Return_The_Number_Of_Stops_Of_The_Route(string route, int stopsExpected)
        {
            Route.Parse(route).TotalStops.Should().Be(stopsExpected);
        }
    }
}