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
            String result = "";
            String wantedResult = "";
            Assert.IsTrue(result == wantedResult, "Average Speed calculated incorrectly");
        }
    }

}
