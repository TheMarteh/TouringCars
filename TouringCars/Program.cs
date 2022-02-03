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
            Car[] logCars;




            // Start typing your code here //

            // example 
            // manually entered waypoints with specified type and location
            PointOfInterest[] points = new PointOfInterest[] {
                new PointOfInterest("Shell", new int[] { 1, 13 }, POIType.gas_station),
                new PointOfInterest("McDonalds", new int[] { 9, 12 }, POIType.food),
                new PointOfInterest("Albert Heijn", new int[] { 10, 2 }, POIType.food),
                new PointOfInterest("Google HQ", new int[] { 18, 5 }, POIType.work),
                new PointOfInterest("Coolblue", new int[] { 4, 8 }, POIType.work),
                new PointOfInterest("Passing Shot", new int[] { 12, 17 }, POIType.hangout),
                new PointOfInterest("Vrienden Live", new int[] { 6, 3 }, POIType.hangout),
                new PointOfInterest("Coolsingel", new int[] { 18, 13 }, POIType.hangout),
                new PointOfInterest("BP", new int[] { 17, 15 }, POIType.gas_station),
                new PointOfInterest("Erasmus University", new int[] { 11, 12 }, POIType.hangout),
             };

            // instantiating the route
            Route tour = new Route(points, useZeroPointAsStart: true, sorter: Sorter.randomSort);

            // setting and running the testcars
            // these are the cars that respond to the variables in Config.cs
            TesterCars testcars = new TesterCars(points);
            testcars.go(showOutput: true);

            // instantiating a single car object with a brand and setting our custom route
            Car tester = new Car("Fred", Automerken.Ferrari) { route = tour };

            // starting the drive without the owner entering the car will not work.
            tester.go(); // will not work, as the car first needs to be unlocked

            // getting in the car to unlock it
            outputLog += tester.getIn(tester.owner, showOverride: true);

            // starting the car along the route
            outputLog += tester.go(showOverride: true);

            // a car can also drive without a route:
            Car driveTillTheSun = new Car("Pietje");
            outputLog += driveTillTheSun.getIn("Pietje", showOverride: true);
            outputLog += driveTillTheSun.go(showOverride: true);

            // printing output
            Analyzer a = new Analyzer();
            a.setCars(testcars.getCars());



            // add your Cars here //
            logCars = new Car[] {
                tester,
                // driveTillTheSun,
                // testcars.getCars()[0],
                };



            // creating logs
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
                outputLog += a.plotRoute(logCars);
                foreach (var car in logCars)
                {
                    outputLog += Plotter.plotPoints(car.route.getDrivenRoute());
                }
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