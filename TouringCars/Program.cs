using System;
// using System.Threading;

namespace TouringCars
{

    public class Program
    {
        public static void Main()
        {
            // initializing outputlog
            String outputLog = "";

            // setting and running the testcars
            Car[] testerCars = new Car[WorkingParams.testCars];
            for (int i = 0; i < WorkingParams.testCars; i++)
            {
                // populating the test route with random points. Each test car gets their own route
                PointOfInterest[] testPoints = new PointOfInterest[WorkingParams.wayPoints];
                for (int j = 0; j < WorkingParams.wayPoints; j++)
                {
                    testPoints[j] = new PointOfInterest("Testpunt " + j, value: 15, cost: 10);
                }

                // setting up route and creating the car instance
                Route testTour = new Route();
                if (testPoints.Length > 0) { testTour = new Route(testPoints); }
                testerCars[i] = new Car($"Testauto #{i}") { route = testTour };

                // starting the drive
                testerCars[i].getIn(testerCars[i].owner);
                testerCars[i].go();
            }

            // printing a summary of all the testCars
            foreach (Car car in testerCars)
            {
                outputLog += car.printSummary();
            }

            // manually entered waypoints with specified type and location
            PointOfInterest p1 = new PointOfInterest("Benzinepomp", 46, 23, POIType.gas_station);
            PointOfInterest p2 = new PointOfInterest("McDonalds", 88, 55, POIType.food);
            PointOfInterest p3 = new PointOfInterest("Coolblue", 69, 71, POIType.work);
            PointOfInterest p4 = new PointOfInterest("Passing Shot", 48, 12, POIType.hangout);
            PointOfInterest p5 = new PointOfInterest("Vrienden Live", 10, 46, POIType.hangout);
            PointOfInterest[] points = new PointOfInterest[] { p1, p2, p3, p4, p5 };

            // instantiating the route
            Route tour = new Route(points);

            // instantiating a single car object
            Car tester = new Car("Fred", Automerken.Ferrari) { route = tour };

            // starting the drive without the owner entering the car will not work.
            tester.go(); // will not work, as the car first needs to be unlocked

            // getting in the car to start it
            tester.getIn(tester.owner);

            // starting the route
            tester.go();


            // a car can also drive without a route:
            Car driveTillTheSun = new Car("Pietje");
            driveTillTheSun.getIn("Pietje");
            driveTillTheSun.go();

            // printing summaries
            outputLog += tester.printSummary();
            outputLog += driveTillTheSun.printSummary();
            outputLog += Analyzer.avgSpeedResults(testerCars);

            Console.WriteLine(outputLog);

            // debug line
            Console.WriteLine();
        }
    }


    class NotUsed
    {
        public static void CountUp(int limit)
        {
            for (int i = 0; i <= limit; i++)
            {
                Console.WriteLine("Counting up until " + limit + ": " + i);
                Thread.Sleep(100);
            }
            Console.WriteLine("Finished!");
        }
        public static void CountDown(int limit)
        {
            for (int i = limit; i >= 0; i--)
            {
                Console.WriteLine("Counting down from " + limit + ": " + i);
                Thread.Sleep(100);

            }
            Console.WriteLine("Finished!");
        }
    }
}