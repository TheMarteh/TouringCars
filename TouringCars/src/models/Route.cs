namespace TouringCars
{
    public class RoutePoint
    {
        public PointOfInterest poi;
        public int distanceToNextPoint;
        public Boolean hasReached;
        public int fuelUsedSoFar;

        public RoutePoint(PointOfInterest poi, int distanceToNextPoint, Boolean hasReached, int fuelUsedSoFar)
        {
            this.poi = poi;
            this.distanceToNextPoint = distanceToNextPoint;
            this.hasReached = hasReached;
            this.fuelUsedSoFar = fuelUsedSoFar;
        }
    }
    public class Route
    {
        private RoutePoint[] waypoints;
        public bool hasFinished;
        public int atWaypointNumber;

        public Route(PointOfInterest[] points) : this()
        {
            this.waypoints = this.planRoute(points);
        }
        public Route()
        {
            PointOfInterest p1 = new PointOfInterest("No route added", new int[] { 0, 0 }, POIType.start);
            PointOfInterest p2 = new PointOfInterest("No route added", new int[] { int.MaxValue, int.MaxValue }, POIType.terminator);

            this.waypoints = new RoutePoint[] { new RoutePoint(p1, 0, false, 0), new RoutePoint(p2, int.MaxValue, false, 0) };
            this.hasFinished = false;
            this.atWaypointNumber = 0;
        }

        public Tuple<int, int> getLength()
        {
            // returns the total amount of waypoints and the total added distances until the final waypoint
            int total = 0;
            for (int i = 0; i < waypoints.Count(); i++)
            {
                total += waypoints[i].distanceToNextPoint;


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
            if (this.atWaypointNumber < this.waypoints.Count())
            {
                return this.waypoints[atWaypointNumber];
            }
            // only happens when there was no finish and the route has run out.
            this.atWaypointNumber--;
            return new RoutePoint(new PointOfInterest("Route has finished", new int[] { this.waypoints[atWaypointNumber].poi.locationX, this.waypoints[atWaypointNumber].poi.locationY }, POIType.terminator), 0, false, 0);
        }

        private RoutePoint[] planRoute(PointOfInterest[] points)
        {
            // initializing the final list as a Tuple<PointOfInterest, bool> array
            RoutePoint[] sortedPoints = new RoutePoint[points.Count()];

            // sorting the points based on distance to 0 ascending 
            // bubble sort
            // TODO: Build navigator.
            // Sorting breaks with locationX and locationY. For now just sorting by locationX


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

            // converting PointOfInterest[] to Tuple<PointOfInterest, int>[]
            sortedPoints[0] = new RoutePoint(points[0], getDistanceBetweenPoints(new PointOfInterest("Start", new int[] { 0, 0 }, POIType.start), points[0]), false, 0);
            for (int i = 1; i < points.Count(); i++)
            {
                sortedPoints[i] = new RoutePoint(points[i], getDistanceBetweenPoints(points[i - 1], points[i]), false, 0);
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

        public void arriveAtPoint(int usedFuel, PointOfInterest waypoint)
        {
            this.waypoints[atWaypointNumber] = new RoutePoint(waypoint, waypoints[atWaypointNumber].distanceToNextPoint, true, usedFuel);
            this.atWaypointNumber++;
        }
    }

}