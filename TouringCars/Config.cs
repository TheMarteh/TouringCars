namespace TouringCars
{
    public class WorkingParams
    {
        // determines whether to add the testercar outputs to the outputlog
        public static bool showOutput = true;
        // determines the amount of cars should run in the simulation
        public static int testCars = 1;
        // the amount of randomly generated waypoints
        public static int wayPoints = 100;
        // the amount of used waypoints for a route
        public static int routePoints = 10;
        // the maximum distance a randomly generated waypoint is away from 0
        public static int maxDistance = 32;

    }

    public class FixedParams
    {
        // the default fuel tank size of a car
        public static int maxCarFuel = 60;
        // the amount of fuel a car starts with by default
        public static int startingFuel = 25;
        public static bool createLogFile = true;
    }

}