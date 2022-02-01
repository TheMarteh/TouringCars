namespace TouringCars
{
    public class WorkingParams
    {
        // determines the amount of cars should run in the simulation
        public static int testCars = 50;
        // the amount of randomly generated waypoints
        public static int wayPoints = 50;
        // the maximum distance a randomly generated waypoint is away from 0
        public static int maxDistance = 20;
        // the amount of fuel a car starts with by default
        public static int startingFuel = 25;
    }

    public class FixedParams
    {
        // the default fuel tank size of a car
        public static int maxCarFuel = 50;
    }

}