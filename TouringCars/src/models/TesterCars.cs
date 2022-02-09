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
        private bool customPoints;

        public TesterCars(PointOfInterest[]? testPoints = null, int? carAmount = null, int? waypointsToMake = null, int? waypointsToUse = null)
        {
            this.carAmount = (carAmount != null) ? (int)carAmount : WorkingParams.testCars;
            this.waypointsToMake = (waypointsToMake != null) ? (int)waypointsToMake : WorkingParams.wayPoints;
            this.WaypointsToUse = (waypointsToUse != null) ? (int)WaypointsToUse : WorkingParams.routePoints;
            this.testPoints = (testPoints == null) ? createTestPoints() : testPoints;
            this.customPoints = (testPoints == null) ? false : true;
            this.cars = createTestCars();
            this.output = "";
        }

        private PointOfInterest[] createTestPoints()
        {
            int n = this.waypointsToMake;

            PointOfInterest[] points = new PointOfInterest[n];
            for (int j = 0; j < n; j++)
            {
                points[j] = new PointOfInterest(name: "Testpunt " + j, value: 15, cost: 10);
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
                Route testTour = (customPoints) ? new Route(points: testPoints, useZeroPointAsStart: WorkingParams.useZeroPointAsStart) : new Route(points: carPoints, useZeroPointAsStart: WorkingParams.useZeroPointAsStart);
                testerCars[i] = new Car(owner: $"Testauto #{i}", route: testTour);

            }
            return testerCars;
        }

        public void go(Boolean? showOutput = null)
        {
            showOutput = (showOutput == null) ? WorkingParams.showOutput : showOutput;
            // looping over all cars in the system
            foreach (Car car in cars)
            {
                // starting the drive
                this.output += car.getIn(car.owner, (Boolean)showOutput);
                this.output += car.go((Boolean)showOutput);

                // change addToLog to true to print the summary after an individual testcar has driven. Defaults to false
                // because we will print out all the summaries at once in the output log creation stage. 
                this.output += car.printSummary(addToLog: false);
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