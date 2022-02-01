namespace TouringCars
{
    public class Route
    {
        private Tuple<PointOfInterest, int>[] waypoints;
        public bool hasFinished;
        public int atWaypointNumber;

        public Route(PointOfInterest[] points) : this()
        {
            this.waypoints = this.planRoute(points);
        }
        public Route()
        {
            PointOfInterest p = new PointOfInterest("No route added", new int[] { int.MaxValue, int.MaxValue }, POIType.start);
            this.waypoints = new Tuple<PointOfInterest, int>[] { Tuple.Create(p, int.MaxValue) };
            this.hasFinished = false;
            this.atWaypointNumber = 0;
        }

        public Tuple<int, int> getLength()
        {
            // returns the total amount of waypoints and the location of the final waypoint
            int total = 0;
            for (int i = 0; i < waypoints.Count(); i++)
            {
                total += waypoints[i].Item2;


            }
            return new Tuple<int, int>(waypoints.Count(), total);
        }

        public Tuple<int, int>[] getWaypointCoordinates()
        {
            Tuple<int, int>[] coords = new Tuple<int, int>[waypoints.Count()];
            for (int i = 0; i < coords.Count(); i++)
            {
                coords[i] = waypoints[i].Item1.getCoordinates();
            }
            return coords;
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
            return waypoints.Length;
        }

        public Tuple<PointOfInterest, int> getNextPoint()
        {
            // if there are any more waypoints left in the route, return the next waypoint. else, return a terminator waypoint
            if (this.atWaypointNumber < this.waypoints.Count())
            {
                return this.waypoints[atWaypointNumber];
            }
            // only happens when there was no finish and the route has run out.
            this.atWaypointNumber--;
            return Tuple.Create(new PointOfInterest("Route has finished", new int[] { -1, -1 }, POIType.terminator), 0);
        }

        private Tuple<PointOfInterest, int>[] planRoute(PointOfInterest[] points)
        {
            // initializing the final list as a Tuple<PointOfInterest, bool> array
            Tuple<PointOfInterest, int>[] sortedPoints = new Tuple<PointOfInterest, int>[points.Count()];

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
            sortedPoints[0] = Tuple.Create(points[0], getDistanceBetweenPoints(new PointOfInterest("Start", new int[] { 0, 0 }, POIType.start), points[0]));
            for (int i = 1; i < points.Count(); i++)
            {
                sortedPoints[i] = Tuple.Create(points[i], getDistanceBetweenPoints(points[i - 1], points[i]));
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

        public void arriveAtPoint()
        {
            this.atWaypointNumber++;
        }
    }

}