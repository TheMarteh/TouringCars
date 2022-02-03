namespace TouringCars
{
    public class WorkingParams
    {
        // determines whether to add the testercar outputs to the outputlog
        public static bool showOutput = false;
        // determines the amount of cars should run in the simulation
        public static int testCars = 1;
        // the amount of randomly generated waypoints
        public static int wayPoints = 100;
        // the amount of used waypoints for a route
        public static int routePoints = 20;
        // the maximum distance a randomly generated waypoint is away from 0
        public static int maxDistance = 10;

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
        public static int maxScreenWidth = 80;
        //
        public static int outputLogCelwidth = 3;
    }

}