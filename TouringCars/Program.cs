using System;
// using System.Threading;

namespace TouringCars
{

    public class Program
    {
        public static void Main()
        {
            // initializing variables
            String outputLog = "";
            Car[] logCars;
            PointOfInterest[] points = WorkingParams.points;

            // Start typing your code here //

            // instantiating the route
            Route tour = new Route(points, useZeroPointAsStart: WorkingParams.useZeroPointAsStart, sorter: Sorter.randomSort);

            // setting and running the testcars
            // these are the cars that respond to the variables in Config.cs
            // uses the default points if WorkingParams.useCustomRoute == false. Otherwise it creates random points
            // based on the other parameters in WorkingParams.
            TesterCars testcars = new TesterCars(testPoints: WorkingParams.useCustomRoute ? points : null);
            testcars.go();

            // instantiating a single car object with a brand and setting our custom route
            Car tester = new Car(owner: "Fred", brand: Automerken.Ferrari, route: tour);

            // starting the drive without the owner entering the car will not work.
            tester.go(); // will not work, as the car first needs to be unlocked

            // getting in the car to unlock it
            outputLog += tester.getIn(tester.owner, showOverride: true);

            // starting the car along the route
            outputLog += tester.go(showOverride: true);

            // a car can also drive without a route:
            Car driveTillTheSun = new Car(owner: "Pietje");
            outputLog += driveTillTheSun.getIn("Pietje");
            outputLog += driveTillTheSun.go(showOverride: false);

            // printing output
            Analyzer a = new Analyzer();
            a.addCars(testcars.getCars());
            // a.addCars(tester);


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
                outputLog += a.plotRoutes();


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