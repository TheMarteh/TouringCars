namespace TouringCars
{

    public class Route
    {
        private RoutePoint[] waypoints;
        public bool hasFinished;
        public bool useZeroPointAsStart;
        public int atWaypointNumber;
        private Sorter sorter;

        public Route(PointOfInterest[] points, Sorter sorter = Sorter.no_sorter, bool useZeroPointAsStart = true) : this(sorter, useZeroPointAsStart)
        {
            this.waypoints = this.planRoute(points);
        }
        public Route(Sorter sorter = Sorter.no_sorter, bool useZeroPointAsStart = true)
        {
            PointOfInterest p1 = new PointOfInterest("No route added", new int[] { 0, 0 }, POIType.start);
            PointOfInterest p2 = new PointOfInterest("No route added", new int[] { int.MaxValue, int.MaxValue }, POIType.terminator);

            this.useZeroPointAsStart = useZeroPointAsStart;
            this.sorter = sorter;
            this.waypoints = new RoutePoint[] { new RoutePoint(0, p1, 0, false, 0), new RoutePoint(1, p2, int.MaxValue, false, 0) };
            this.hasFinished = false;
            this.atWaypointNumber = 0;
        }

        public Tuple<int, int> getLength()
        {
            // returns the total amount of waypoints and the total added distances until the final waypoint
            int total = 0;
            for (int i = 0; i < waypoints.Count(); i++)
            {
                total += waypoints[i].distanceToNextPoint < int.MaxValue ? waypoints[i].distanceToNextPoint : 0;
            }
            return new Tuple<int, int>(waypoints.Count(), total);
        }

        public RoutePoint[] getDrivenRoute()
        {
            return this.waypoints;
        }

        public static int getDistanceBetweenPoints(PointOfInterest p1, PointOfInterest p2)
        {
            int result = 0;
            int x1 = p1.locationX;
            int x2 = p2.locationX;
            int y1 = p1.locationY;
            int y2 = p2.locationY;

            result = (int)Math.Sqrt(Math.Pow(x2 - x1, 2.0) + Math.Pow(y2 - y1, 2.0));
            return result;
        }

        public int countWaypoints()
        {
            return waypoints.Count();
        }

        public RoutePoint getNextPoint()
        {
            // if there are any more waypoints left in the route, return the next waypoint. else, return a terminator waypoint

            // if (this.atWaypointNumber < this.waypoints.Count())
            // {
            return this.waypoints[atWaypointNumber];
            // }

        }

        private RoutePoint[] planRoute(PointOfInterest[] points)
        {
            // initializing the final list as a Tuple<PointOfInterest, bool> array

            // sorting the points based on distance to 0 ascending 
            // bubble sort
            // TODO: Build navigator.
            // Sorting breaks with locationX and locationY. For now just sorting by locationX

            PointOfInterest[] sortedPoints = sortPoints(points);
            RoutePoint[] plannedPoints;

            int plannedPointsSize = useZeroPointAsStart ? sortedPoints.Length : sortedPoints.Length;
            plannedPoints = new RoutePoint[plannedPointsSize];

            if (useZeroPointAsStart)
            {
                plannedPoints[0] = new RoutePoint(0, new PointOfInterest("Start", new int[] { 0, 0 }, POIType.start), 0, false, 0);
            }
            else
            {
                plannedPoints = new RoutePoint[sortedPoints.Length];
                plannedPoints[0] = new RoutePoint(0, new PointOfInterest(sortedPoints[0].name, new int[] { sortedPoints[0].locationX, sortedPoints[0].locationY }, POIType.start), 0, false, 0);
            }
            for (int i = 1; i < sortedPoints.Length; i++)
            {
                plannedPoints[i] = new RoutePoint(i, sortedPoints[i - 1], getDistanceBetweenPoints(plannedPoints[i - 1].poi, sortedPoints[i - 1]), false, 0);
            }
            plannedPoints.Last().setTerminator();
            return plannedPoints;
        }

        PointOfInterest[] sortPoints(PointOfInterest[] points)
        {
            switch (sorter)
            {
                case Sorter.bubbleSort:
                    return bubbleSort(points);
                case Sorter.no_sorter:
                    return noSort(points);
                default:
                    return randomSort(points);
            }
        }

        PointOfInterest[] noSort(PointOfInterest[] points)
        {
            return points;
        }

        PointOfInterest[] randomSort(PointOfInterest[] points)
        {
            PointOfInterest temp;
            int random = new Random().Next(0, 10);
            PointOfInterest[] randompoints = new PointOfInterest[points.Length];
            for (int i = 0; i < points.Length; i++)
            {

            }
            return randompoints;
        }



        private PointOfInterest[] bubbleSort(PointOfInterest[] points)
        {
            PointOfInterest temp;
            for (int j = 0; j <= points.Length - 2; j++)
            {
                for (int i = 0; i <= points.Length - 2; i++)
                {
                    if (points[i].locationX > points[i + 1].locationX)
                    {
                        temp = points[i + 1];
                        points[i + 1] = points[i];
                        points[i] = temp;
                    }
                }
            }
            return points;
        }
        public String getStranded()
        {
            this.hasFinished = true;
            return "Stranded..\n";
        }
        public String finish()
        {
            this.hasFinished = true;
            return "Finished route, well done!\n";
        }

        public Tuple<String, ValueChanger> arriveAtPoint(int usedFuel, int fuelLeft)
        {
            RoutePoint p = this.waypoints[atWaypointNumber];

            this.atWaypointNumber++;
            var callback = p.arriveAtPoint(usedFuel, fuelLeft);

            return callback;
        }
    }

}