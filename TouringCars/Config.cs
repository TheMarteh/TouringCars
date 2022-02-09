namespace TouringCars
{
    public class WorkingParams
    {
        // determines whether to add the testercar outputs to the outputlog
        public static bool showOutput = false;
        public static bool showLocationCallbackDetails = true;
        public static bool useCustomRoute = true;
        // determines the amount of cars should run in the simulation
        public static int testCars = 5;
        // the amount of randomly generated waypoints for a random route
        public static int wayPoints = 100;
        // the amount of used waypoints for a random route
        public static int routePoints = 20;
        // the maximum distance a randomly generated waypoint is away from 0
        public static int maxDistance = 20;

        public static PointOfInterest[] points = new PointOfInterest[] {
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

        public static bool useZeroPointAsStart = false;


    }

    public class FixedParams
    {
        // the default fuel tank size of a car
        public static int maxCarFuel = 60;
        // the amount of fuel a car starts with by default
        public static int startingFuel = 25;
        // determines whether to create a log file.
        public static bool createLogFile = true;
        // Maximum screen width to occupy
        public static int maxScreenWidth = 90;
        //
        public static int outputLogCelwidth = 3;
    }

}