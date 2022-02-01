using System;
using OxyPlot;
// using System.Threading;

namespace TouringCars
{

    public class Program
    {
        public static void Main()
        {
            // initializing outputlog
            Analyzer a = new Analyzer();
            String outputLog = "";

            // setting and running the testcars
            TesterCars testcars = new TesterCars();
            testcars.go(showOutput: true);

            // manually entered waypoints with specified type and location
            PointOfInterest p1 = new PointOfInterest("Benzinepomp", new int[] { 46, 23 }, POIType.gas_station);
            PointOfInterest p2 = new PointOfInterest("McDonalds", new int[] { 88, 55 }, POIType.food);
            PointOfInterest p3 = new PointOfInterest("Coolblue", new int[] { 69, 71 }, POIType.work);
            PointOfInterest p4 = new PointOfInterest("Passing Shot", new int[] { 48, 12 }, POIType.hangout);
            PointOfInterest p5 = new PointOfInterest("Vrienden Live", new int[] { 10, 46 }, POIType.hangout);
            PointOfInterest[] points = new PointOfInterest[] { p1, p2, p3, p4, p5 };

            // instantiating the route
            Route tour = new Route(points);

            // instantiating a single car object
            Car tester = new Car("Fred", Automerken.Ferrari) { route = tour };

            // starting the drive without the owner entering the car will not work.
            tester.go(); // will not work, as the car first needs to be unlocked

            // getting in the car to start it
            outputLog += tester.getIn(tester.owner, showOverride: true);

            // starting the route
            outputLog += tester.go(showOverride: true);


            // a car can also drive without a route:
            Car driveTillTheSun = new Car("Pietje");
            outputLog += driveTillTheSun.getIn("Pietje", showOverride: true);
            outputLog += driveTillTheSun.go(showOverride: true);

            // printing output
            a.setCars(testcars.getCars());
            if (FixedParams.createLogFile)
            {
                // testcar output
                outputLog += testcars.getOutput();

                // route summaries
                outputLog += tester.printSummary();
                outputLog += driveTillTheSun.printSummary();
                outputLog += "\n";

                // Analyzer results
                outputLog += a.avgSpeedResults();
                outputLog += a.avgRouteLength();

                // Static Analyzer use
                outputLog += a.plotRoute(new Car[] { tester });
            }

            Console.Write(outputLog);

            // debug line
            Console.Write("");
        }
    }


    class NotUsed
    {
        public static void CountUp(int limit)
        {
            for (int i = 0; i <= limit; i++)
            {
                Console.Write("Counting up until " + limit + ": " + i);
                Thread.Sleep(100);
            }
            Console.Write("Finished!");
        }
        public static void CountDown(int limit)
        {
            for (int i = limit; i >= 0; i--)
            {
                Console.Write("Counting down from " + limit + ": " + i);
                Thread.Sleep(100);

            }
            Console.Write("Finished!");
        }
    }
}