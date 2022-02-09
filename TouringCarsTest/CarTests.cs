using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TouringCars;

namespace TouringCarsTests
{
    [TestClass]
    public class CarTests
    {
        static string name = "Sid";
        Car auto = new Car(name);

        [TestMethod]
        public void CreateNewCarWithOwner()
        {
            Car auto = new Car(name);
            Assert.IsTrue(auto.owner == name, "Car owner should be name");
        }

        [TestMethod]
        public void RouteShouldBeLen2()
        {
            Car auto = new Car(name);
            Assert.IsTrue(auto.route.countWaypoints() == 2, "Route should be only the last stop");
        }
        [TestMethod]
        public void DontDriveWithNoFuel()
        {
            Car auto = new Car(name);
            auto.emptyTank();
            auto.getIn(name);
            auto.drive();
            Console.Write(auto.checkLock());
            Assert.IsTrue(auto.getKMDriven() <= 0, "Car can\'t drive if there is no fuel");
        }
        [TestMethod]
        public void drive()
        {
            Car auto = new Car(name);
            auto.getIn(name);
            auto.drive();
            Console.Write(auto.checkLock());
            Assert.IsTrue(auto.getKMDriven() > 0, "Car can\'t drive if there is no fuel");
        }
        [TestMethod]
        public void DontDriveLocked()
        {
            Car auto = new Car(name);
            auto.drive();
            Assert.IsTrue(auto.getKMDriven() <= 0, "Car can\'t drive if it is still locked");
        }

        [TestMethod]
        public void NoUnlockWithWrongName()
        {
            Car auto = new Car(name);
            auto.getIn(name + "WRONGNAME");
            Assert.IsTrue(auto.checkLock().Item2, "Car can\'t be opened by the wrong person");
        }

        [TestMethod]
        public void refuel()
        {
            Car auto = new Car(name);
            auto.getIn(name);
            auto.addFuel(10);
            Assert.IsTrue(auto.getFuel() == (FixedParams.startingFuel + 10), "Fuel adding test has gone wrong, or you should start with less fuel");
        }

        [TestMethod]
        public void distanceLowerMaxDistance()
        {
            Boolean result = true;
            TesterCars testcars = new TesterCars(waypointsToMake: 50, waypointsToUse: 10, carAmount: 10);
            testcars.go(showOutput: false);
            foreach (Car carToTest in testcars.getCars())
            {
                int routelength = carToTest.route.getLength().Item2;
                Boolean b1 = (carToTest.route.atWaypointNumber > carToTest.route.countWaypoints() && carToTest.getKMDriven() >= routelength);
                Boolean b2 = ((carToTest.route.atWaypointNumber <= carToTest.route.countWaypoints() && carToTest.getKMDriven() < routelength) || routelength == 0);
                result = (result && (b1 || b2));
            }
            Assert.IsTrue(result, "Driven a wrong amount!");
        }
    }
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public void AveragePerBrandTest()
        {
            Car c1 = new Car("Sid", Automerken.Audi);
            Car c2 = new Car("Tester", Automerken.Audi);
            c1.getIn("Sid");
            c2.getIn("Tester");

            Analyzer a = new Analyzer(new Car[] { c1, c2 });
            Tuple<Automerken, int, int, int>[] result = a.AvgDistancePerBrand();
            Tuple<Automerken, int, int, int> wantedResult = Tuple.Create(Automerken.Audi, 2, 0, 0);
            Console.Write(result[0].ToString());
            Console.Write(wantedResult.ToString());
            Assert.IsTrue(result[0].ToString() == wantedResult.ToString() && result[0].Item2 == result[0].Item2, "Average Speed calculated incorrectly");
        }
    }
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void distanceTest()
        {
            PointOfInterest p1 = new PointOfInterest("p1", new int[] { 1, 1 });
            PointOfInterest p2 = new PointOfInterest("p2", new int[] { 4, 5 });
            System.Console.WriteLine($"Input: {p2.locationX} - {p1.locationX}, {p2.locationY} - {p1.locationY}");
            int result = Route.getDistanceBetweenPoints(p1, p2);
            System.Console.WriteLine($"Input: {p2.locationX - p1.locationX}, {p2.locationY - p1.locationY}");
            System.Console.WriteLine(result);
            Assert.IsTrue(result == 5, "Calculating distance test went wrong.");
        }

        [TestMethod]
        public void lenShouldBe0()
        {
            Route route = new Route();
            Assert.IsTrue(route.getLength().Item2 == 0, "Final routelength when not adding points is not 0");
        }
    }
}