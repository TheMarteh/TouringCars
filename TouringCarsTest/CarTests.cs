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
        public void RouteShouldBeLen1()
        {
            Car auto = new Car(name);
            Assert.IsTrue(auto.route.countWaypoints() == 1, "Route should be only the last stop");
        }
        [TestMethod]
        public void DontDriveWithNoFuel()
        {
            Car auto = new Car(name);
            auto.emptyTank();
            auto.getIn(name);
            auto.drive();
            Console.WriteLine(auto.checkLock());
            Assert.IsTrue(auto.getKMDriven() <= 0, "Car can\'t drive if there is no fuel");
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
            Assert.IsTrue(auto.checkLock(), "Car can\'t be opened by the wrong person");
        }

        [TestMethod]
        public void refuel()
        {
            Car auto = new Car(name);
            auto.getIn(name);
            auto.addFuel(10);
            Assert.IsTrue(auto.getFuel() == (WorkingParams.startingFuel + 10), "Fuel adding test has gone wrong, or you should start with less fuel");
        }

    }
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public void AveragePerBrandTest()
        {
            Car c1 = new Car("Sid", Automerken.Ferrari);
            Car c2 = new Car("Tester", Automerken.Ferrari);
            c1.getIn("Sid");
            c2.getIn("Tester");
            Tuple<Automerken, int>[] result = Analyzer.AvgSpeedPerBrand(new Car[] { c1, c2 });
            Tuple<Automerken, int> wantedResult = Tuple.Create(Automerken.Audi, 2);
            Console.WriteLine(result[0]);
            Console.WriteLine(wantedResult);

            Assert.IsTrue(result[0] == wantedResult, "Average Speed calculated incorrectly");
        }
    }

}
