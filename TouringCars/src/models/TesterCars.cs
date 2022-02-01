namespace TouringCars
{
    public class TesterCars
    {
        private Car[] cars;
        private PointOfInterest[] testPoints;
        private String output;
        private int waypoints;

        public TesterCars()
        {
            this.waypoints = WorkingParams.wayPoints;
            this.testPoints = createTestPoints();
            this.cars = createTestCars();
            this.output = "";
        }

        private PointOfInterest[] createTestPoints(int n = -1)
        {
            n = n < 0 ? this.waypoints : n;

            PointOfInterest[] points = new PointOfInterest[n];
            for (int j = 0; j < n; j++)
            {
                points[j] = new PointOfInterest("Testpunt " + j, value: 15, cost: 10);
            }
            return points;
        }
        private Car[] createTestCars()
        {
            Car[] testerCars = new Car[WorkingParams.testCars];
            for (int i = 0; i < WorkingParams.testCars; i++)
            {
                // populating the test route with random points. Each test car gets their own route
                PointOfInterest[] carPoints = new PointOfInterest[WorkingParams.routePoints];
                for (int j = 0; j < WorkingParams.routePoints; j++)
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
                this.output += car.getIn(car.owner, showOutput);
                this.output += car.go(showOutput);
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