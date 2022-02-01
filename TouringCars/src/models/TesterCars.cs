namespace TouringCars
{
    public class TesterCars
    {
        private Car[] cars;
        private PointOfInterest[] testPoints;
        private String output;
        private int waypointsToMake;
        private int WaypointsToUse;
        private int carAmount;

        public TesterCars(int customWaypoints, int customRouteLength, int customCarAmount)
        {
            this.carAmount = customCarAmount;
            this.WaypointsToUse = customRouteLength;
            this.waypointsToMake = customWaypoints;
            this.testPoints = createTestPoints();
            this.cars = createTestCars();
            this.output = "";
        }
        public TesterCars()
        {
            this.carAmount = WorkingParams.testCars;
            this.waypointsToMake = WorkingParams.wayPoints;
            this.WaypointsToUse = WorkingParams.routePoints;
            this.testPoints = createTestPoints();
            this.cars = createTestCars();
            this.output = "";
        }

        private PointOfInterest[] createTestPoints()
        {
            int n = this.waypointsToMake;

            PointOfInterest[] points = new PointOfInterest[n];
            for (int j = 0; j < n; j++)
            {
                points[j] = new PointOfInterest("Testpunt " + j, value: 15, cost: 10);
            }
            return points;
        }
        private Car[] createTestCars()
        {
            Car[] testerCars = new Car[carAmount];
            for (int i = 0; i < carAmount; i++)
            {
                // populating the test route with random points. Each test car gets their own route
                PointOfInterest[] carPoints = new PointOfInterest[this.WaypointsToUse];
                for (int j = 0; j < this.WaypointsToUse; j++)
                {
                    carPoints[j] = this.testPoints[new Random().Next(0, testPoints.Count())];
                }

                // setting up route and creating the car instance
                Route testTour = new Route();
                if (testPoints.Length > 0) { testTour = new Route(carPoints); }
                testerCars[i] = new Car($"Testauto #{i}") { route = testTour };

            }
            return testerCars;
        }

        public void go(Boolean showOutput)
        {
            // looping over all cars in the system
            foreach (Car car in cars)
            {
                // starting the drive
                System.Console.WriteLine("Getting in");
                this.output += car.getIn(car.owner, showOutput);
                System.Console.WriteLine("Starting trip");
                this.output += car.go(showOutput);
                System.Console.WriteLine("Printing results");
                this.output += car.printSummary(showOutput);
            }
        }

        public Car[] getCars()
        {
            return this.cars;
        }
        public String getOutput()
        {

            foreach (Car car in cars)
            {
                this.output += car.printSummary(FixedParams.createLogFile);
            }
            return this.output;

        }
    }
}