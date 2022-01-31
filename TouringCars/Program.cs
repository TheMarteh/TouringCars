﻿using System;
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
                    testPoints[j] = new PointOfInterest("Testpunt " + j);
                }

                // setting up route and creating the car instance
                Route testTour = new Route(testPoints);
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
            PointOfInterest p1 = new PointOfInterest("Benzinepomp", 46, POIType.gas_station);
            PointOfInterest p2 = new PointOfInterest("McDonalds", 88, POIType.food);
            PointOfInterest p3 = new PointOfInterest("Coolblue", 69, POIType.work);
            PointOfInterest p4 = new PointOfInterest("Passing Shot", 48, POIType.hangout);
            PointOfInterest p5 = new PointOfInterest("Vrienden Live", 10, POIType.hangout);
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
            Console.WriteLine(outputLog);

            // debug line
            Console.WriteLine();
        }
    }

    public enum Automerken
    {
        Audi,
        Mercedes,
        Ferrari
    }

    public enum POIType
    {
        gas_station,
        food,
        hangout,
        work,
        terminator
    }

    public class PointOfInterest
    {
        public String name { get; set; }
        public int location { get; set; }
        public POIType type;

        public int cost;
        public int value;

        public PointOfInterest(String name, int location, POIType type, int value = 0, int cost = 0) : this(name, location, value, cost)
        {
            this.type = type;
        }
        public PointOfInterest(String name, int location, int value = 0, int cost = 0) : this(name, value, cost)
        {
            this.location = location;
        }

        public PointOfInterest(String name, int value = 0, int cost = 0)
        {
            this.name = name;
            Random rnd = new Random();
            this.location = rnd.Next(0, WorkingParams.maxDistance);
            this.type = (POIType)rnd.Next(0, Enum.GetNames(typeof(POIType)).Length - 1);
        }

    }

    public class Route
    {
        private Tuple<PointOfInterest, bool>[] waypoints;
        public bool hasFinished;
        public int atWaypointNumber;

        public Route(PointOfInterest[] points) : this()
        {
            this.waypoints = this.planRoute(points);
        }
        public Route()
        {
            this.waypoints = new Tuple<PointOfInterest, bool>[] { Tuple.Create(new PointOfInterest("No route added", int.MaxValue, POIType.terminator), false) };
            this.hasFinished = false;
            this.atWaypointNumber = 0;
        }

        public Tuple<int, int> getLength()
        {
            // returns the total amount of waypoints and the location of the final waypoint
            return new Tuple<int, int>(waypoints.Count(), waypoints.Last().Item1.location);
        }

        public int countWaypoints()
        {
            return waypoints.Length;
        }

        public PointOfInterest getNextPoint()
        {
            // if there are any more waypoints left in the route, return the next waypoint. else, return a terminator waypoint
            if (this.atWaypointNumber < this.waypoints.Count())
            {
                return this.waypoints[atWaypointNumber].Item1;
            }
            // only happens when there was no finish and the route has run out.
            this.atWaypointNumber--;
            return new PointOfInterest("Route has finished", -1, POIType.terminator);
        }

        private Tuple<PointOfInterest, bool>[] planRoute(PointOfInterest[] points)
        {
            // initializing the final list as a Tuple<PointOfInterest, bool> array
            Tuple<PointOfInterest, bool>[] sortedPoints = new Tuple<PointOfInterest, bool>[points.Count()];

            // sorting the points based on distance to 0 ascending 
            // bubble sort
            PointOfInterest temp;
            for (int j = 0; j <= points.Length - 2; j++)
            {
                for (int i = 0; i <= points.Length - 2; i++)
                {
                    if (points[i].location > points[i + 1].location)
                    {
                        temp = points[i + 1];
                        points[i + 1] = points[i];
                        points[i] = temp;
                    }
                }
            }

            // converting PointOfInterest[] to Tuple<PointOfInterest, bool>[]
            for (int i = 0; i < points.Count(); i++)
            {
                sortedPoints[i] = Tuple.Create(points[i], false);
            }
            return sortedPoints;
        }
        public void getStranded()
        {
            this.hasFinished = true;
        }
        public void finish()
        {
            Console.WriteLine("Finished route, well done!");
            this.hasFinished = true;
        }

        public void arriveAtPoint()
        {
            this.atWaypointNumber++;
        }
    }

    public class Car
    {
        public String owner;
        public Automerken brand;
        public Route route;

        private int kmDriven;
        private int fuel;
        private bool locked;

        public Car(String owner)
        {
            this.owner = owner;
            this.kmDriven = 0;
            this.fuel = WorkingParams.startingFuel;
            this.locked = true;
            this.brand = (Automerken)new Random().Next(0, Enum.GetNames(typeof(Automerken)).Length);
            this.route = new Route();
        }

        public Car(String owner, Automerken brand) : this(owner)
        {
            this.brand = brand;
        }

        public int getKMDriven()
        {
            return this.kmDriven;
        }

        public int getFuel()
        {
            return this.fuel;
        }

        public void emptyTank()
        {
            this.fuel = 0;
        }

        public void go()
        {
            Console.WriteLine($"{this.owner} is starting the tour..");
            if (!checkLock())
            {
                while (!route.hasFinished)
                {
                    PointOfInterest next = route.getNextPoint();
                    while (this.kmDriven < next.location && !this.route.hasFinished)
                    {
                        this.drive();
                    }
                    if (!route.hasFinished)
                    {
                        this.route.arriveAtPoint();
                        switch (next.type)
                        {
                            case POIType.terminator:
                                Console.WriteLine("You've finished!");
                                route.finish();
                                break;
                            case POIType.gas_station:
                                Console.WriteLine($"Arrived at waypoint {next.name} at {next.location}km!\nFuel left: {this.fuel}");
                                this.addFuel(next.value);
                                break;
                            case POIType.food:
                                Console.WriteLine($"Arrived at waypoint {next.name} at {next.location}km!\nFuel left: {this.fuel}");
                                Console.WriteLine("Nom nom, lekker eten");
                                break;
                            default:
                                Console.WriteLine($"Arrived at waypoint {next.name} at {next.location}km!\nFuel left: {this.fuel}");
                                break;
                        }
                        // Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("We\'re done, getting out of the car..\n");
                this.locked = true;
            }
        }

        public void getIn(String name)
        {
            Console.WriteLine($"Person {name} is trying to get into the car of {this.owner}");
            if (name == this.owner)
            {
                Console.WriteLine("    The car is now unlocked!");
                this.locked = false;
            }
            else
            {
                Console.WriteLine("    The car doesn't open..");
            }
        }

        public void drive()
        {
            if (!this.locked && !this.route.hasFinished && this.fuel > 0)
            {
                Random rnd = new Random();
                switch (this.brand)
                {
                    case Automerken.Audi:
                        // Console.WriteLine("Jaa wir gehen von Vroom Vroom\n");
                        this.kmDriven += new Random().Next(3, 7);
                        this.fuel -= new Random().Next(1, 3);
                        if (this.fuel <= 0)
                        {
                            Console.WriteLine("Stranded..");
                            this.route.getStranded();
                        }
                        break;

                    case Automerken.Ferrari:
                        // Console.WriteLine("Ciao bella, vruomo vruomo!\n");
                        this.kmDriven += new Random().Next(4, 8);
                        this.fuel -= new Random().Next(1, 3);
                        if (this.fuel <= 0)
                        {
                            Console.WriteLine("Stranded..");
                            this.route.getStranded();
                        }
                        break;

                    case Automerken.Mercedes:
                        // Console.WriteLine("Noo noo, this is so not vroom!\n");
                        this.kmDriven += new Random().Next(1, 8);
                        this.fuel -= new Random().Next(1, 4);
                        if (this.fuel <= 0)
                        {
                            Console.WriteLine("Stranded..");
                            this.route.getStranded();
                        }
                        break;
                }
            }
        }

        public bool checkLock()
        // returns true if the car is locked, returns false if the car is unlocked
        {
            if (this.locked)
            {
                return true;
            }
            else
            {
                Console.WriteLine("The car is still locked.. Please unlock first!");
                return false;
            }
        }

        public int addFuel(int amount)
        {
            // can't add fuel if the car is locked
            if (!this.locked)
            {
                if (this.checkFuel())
                {
                    // add fuel if needed
                    this.fuel += amount;
                    Console.WriteLine("Done, you can now drive some more! " + fuel + " liters left");
                }
                Console.WriteLine($"You\'re still good!\nOff you go, with {fuel} liters left!");
            }
            // return the fuel amount
            return this.getFuel();
        }

        public bool checkFuel()
        {
            if (this.fuel > 10)
            {
                // no need to refuel
                return false;
            }
            else
            {
                // we need to refuel
                return true;
            }
        }

        public String printSummary()
        {
            String carSummary = $"{this.owner}'s auto ({this.brand}):";
            while (carSummary.Length < 35)
            {
                carSummary += " ";
            }
            return $"{carSummary} {this.route.atWaypointNumber} van de {this.route.getLength().Item1} ({this.kmDriven} / {this.route.getLength().Item2} km)\n";
        }
    }

    public class WorkingParams
    {
        public static int testCars = 50;
        public static int wayPoints = 20;
        public static int maxDistance = 250;
        public static int startingFuel = 20;
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