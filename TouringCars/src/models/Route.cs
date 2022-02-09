namespace TouringCars
{

    public class Route
    {
        private RoutePoint[] waypoints;
        public bool hasFinished;
        public bool useZeroPointAsStart;
        public int atWaypointNumber;
        private Sorter sorter;
        private Random rng;

        public Route(PointOfInterest[]? points = null, Sorter? sorter = null, bool? useZeroPointAsStart = null, int? seed = null)
        {

            this.sorter = (Sorter)((sorter != null) ? sorter : (Sorter)Sorter.randomSort);
            this.rng = (seed != null) ? new Random((int)seed) : new Random(Guid.NewGuid().GetHashCode());
            this.useZeroPointAsStart = (useZeroPointAsStart == null) ? false : (Boolean)useZeroPointAsStart;

            this.hasFinished = false;
            this.atWaypointNumber = 0;
            if (points != null)
            {
                this.waypoints = this.planRoute(points);
            }
            else
            {
                PointOfInterest p1 = new PointOfInterest("No route added", new int[] { 0, 0 }, POIType.start);
                PointOfInterest p2 = new PointOfInterest("No route added", new int[] { int.MaxValue, int.MaxValue }, POIType.terminator);
                this.waypoints = new RoutePoint[] { new RoutePoint(0, p1, 0, false, 0), new RoutePoint(1, p2, int.MaxValue, false, 0) };
            }
        }

        public Tuple<int, int> getLength()
        {
            // returns the total amount of waypoints and the total added distances until the final waypoint
            int total = 0;
            for (int i = 0; i < waypoints.Count(); i++)
            {
                total += waypoints[i].distanceToNextPoint < int.MaxValue ? waypoints[i].distanceToNextPoint : 0;
            }
            return new Tuple<int, int>(waypoints.Count() - 1, total);
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
            PointOfInterest[] sortedPoints = new PointOfInterest[points.Length];
            points.CopyTo(sortedPoints, 0);
            sortedPoints = sortPoints(sortedPoints);

            // debug

            // System.Console.WriteLine("Original list");
            // foreach (PointOfInterest point in points)
            // {
            //     System.Console.WriteLine(point.name + " " + point.type);
            // }
            // System.Console.WriteLine();
            // System.Console.WriteLine("After Sorting");
            // foreach (PointOfInterest point in sortedPoints)
            // {
            //     System.Console.WriteLine(point.name + " " + point.type);
            // }
            // System.Console.WriteLine();

            RoutePoint[] plannedPoints;
            int plannedPointsSize = useZeroPointAsStart ? sortedPoints.Length : sortedPoints.Length;
            plannedPoints = new RoutePoint[plannedPointsSize + 1];

            if (useZeroPointAsStart)
            {
                plannedPoints[0] = new RoutePoint(0, new PointOfInterest("Start", new int[] { 0, 0 }, POIType.start), 0, false, 0);
            }
            else
            {
                plannedPoints[0] = new RoutePoint(0, new PointOfInterest(sortedPoints[0].name, new int[] { sortedPoints[0].locationX, sortedPoints[0].locationY }, POIType.start), 0, false, 0);
            }
            for (int i = 1; i <= plannedPointsSize; i++)
            {
                plannedPoints[i] = new RoutePoint(i, sortedPoints[i - 1], getDistanceBetweenPoints(plannedPoints[i - 1].poi, sortedPoints[i - 1]), false, 0);
            }
            PointOfInterest finish = new PointOfInterest(sortedPoints.Last().name, new int[] { sortedPoints.Last().locationX, sortedPoints.Last().locationY }, POIType.terminator, 0, 0);

            RoutePoint final = new RoutePoint(id: plannedPoints[plannedPointsSize - 1].id, poi: finish, distanceToNextPoint: plannedPoints.Last().distanceToNextPoint, false, 0);
            plannedPoints[plannedPointsSize] = final;

            // debug
            // System.Console.WriteLine("After planning: ");
            // foreach (RoutePoint point in plannedPoints)
            // {
            //     System.Console.WriteLine(point.id + " " + point.poi.name + " " + point.poi.type);
            // }
            // System.Console.WriteLine();

            // System.Console.WriteLine("Original list after Sorting and planning");
            // foreach (PointOfInterest point in points)
            // {
            //     System.Console.WriteLine(point.name + " " + point.type);
            // }
            // System.Console.WriteLine();

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
                case Sorter.randomSort:
                    return randomSort(points);
                default:
                    return noSort(points);
            }
        }

        PointOfInterest[] noSort(PointOfInterest[] points)
        {
            return points;
        }

        public PointOfInterest[] randomSort(PointOfInterest[] points)
        {
            Random rng = this.rng;
            PointOfInterest[] randompoints = new PointOfInterest[points.Length];
            points.CopyTo(randompoints, 0);
            int n = points.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                PointOfInterest temp = randompoints[n];
                randompoints[n] = randompoints[k];
                randompoints[k] = temp;
            }
            // debug
            // if (randompoints.Last().locationX == 11)
            // {
            //     System.Console.WriteLine("Sorting made last element the same as previous last!");
            // }
            return randompoints;
        }


        private PointOfInterest[] bubbleSort(PointOfInterest[] points)
        {
            PointOfInterest temp;
            PointOfInterest[] sortedPoints = new PointOfInterest[points.Length];
            Array.Copy(points, sortedPoints, points.Length);
            for (int j = 0; j <= sortedPoints.Length - 2; j++)
            {
                for (int i = 0; i <= sortedPoints.Length - 2; i++)
                {
                    if (sortedPoints[i].locationX > sortedPoints[i + 1].locationX)
                    {
                        temp = sortedPoints[i + 1];
                        sortedPoints[i + 1] = sortedPoints[i];
                        sortedPoints[i] = temp;
                    }
                }
            }
            return sortedPoints;
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

        public Tuple<String, ValueChanger> arriveAtPoint(int usedFuelSoFar, int carFuelLeft, int kmDriven, int fuelUsedToGetToPoint)
        {
            RoutePoint p = this.waypoints[atWaypointNumber];
            var callback = p.arriveAtPoint(atWaypointNumber, usedFuelSoFar, carFuelLeft, kmDriven, fuelUsedToGetToPoint);
            this.atWaypointNumber++;
            return callback;
        }
    }

}