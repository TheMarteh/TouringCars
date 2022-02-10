namespace TouringCars
{
    public class TesterCars
    {
        private Car[] cars;
        private PointOfInterest[] testPoints;
        private String output;
        private int waypointsToMake;
        private int waypointsToUse;
        private int carAmount;
        private bool customPoints;
        private Sorter sorter;

        public TesterCars(PointOfInterest[]? testPoints = null, int? carAmount = null, int? waypointsToMake = null, int? waypointsToUse = null, Sorter sorter = Sorter.no_sorter)
        {
            this.carAmount = (carAmount != null) ? (int)carAmount : WorkingParams.testCars;
            this.waypointsToMake = (waypointsToMake != null) ? (int)waypointsToMake : WorkingParams.wayPoints;
            this.waypointsToUse = (waypointsToUse != null) ? (int)waypointsToUse : WorkingParams.routePoints;
            this.testPoints = (testPoints == null) ? createTestPoints() : testPoints;
            this.customPoints = (testPoints == null) ? false : true;
            this.sorter = sorter;
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
            Route testTour;
            for (int i = 0; i < carAmount; i++)
            {
                // populating the test route with random points. Each test car gets their own route
                if (!customPoints)
                {
                    PointOfInterest[] carPoints = new PointOfInterest[this.waypointsToUse];
                    for (int j = 0; j < this.waypointsToUse; j++)
                    {
                        carPoints[j] = this.testPoints[new Random().Next(0, testPoints.Count())];
                    }
                    testTour = new Route(points: carPoints, useZeroPointAsStart: WorkingParams.useZeroPointAsStart, sorter: sorter);
                }
                else
                {
                    testTour = new Route(points: testPoints, useZeroPointAsStart: WorkingParams.useZeroPointAsStart, sorter: sorter);
                }


                // setting up route and creating the car instance
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
        public String printSummaries(int? n = null)
        {
            n = (n == null) ? this.cars.Length : n;
            int i;
            for (i = 0; i < n; i++)
            {
                this.output += cars[i].printSummary(FixedParams.createLogFile);
            }
            output += (i < this.cars.Length) ? $"... limited to {i} out of {this.cars.Length} cars\n" : "";
            return this.output;

        }
    }
}