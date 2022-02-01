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
            this.waypoints = new Tuple<PointOfInterest, int>[] { Tuple.Create(new PointOfInterest("No route added", int.MaxValue, POIType.terminator), -1) };
            this.hasFinished = false;
            this.atWaypointNumber = 0;
        }

        public Tuple<int, int> getLength()
        {
            // returns the total amount of waypoints and the location of the final waypoint
            return new Tuple<int, int>(waypoints.Count(), waypoints.Last().Item1.location);
        }

        public int countWaypoints()
        {
            return waypoints.Length;
        }

        public PointOfInterest getNextPoint()
        {
            // if there are any more waypoints left in the route, return the next waypoint. else, return a terminator waypoint
            if (this.atWaypointNumber < this.waypoints.Count())
            {
                return this.waypoints[atWaypointNumber].Item1;
            }
            // only happens when there was no finish and the route has run out.
            this.atWaypointNumber--;
            return new PointOfInterest("Route has finished", -1, POIType.terminator);
        }

        private Tuple<PointOfInterest, int>[] planRoute(PointOfInterest[] points)
        {
            // initializing the final list as a Tuple<PointOfInterest, bool> array
            Tuple<PointOfInterest, int>[] sortedPoints = new Tuple<PointOfInterest, int>[points.Count()];

            // sorting the points based on distance to 0 ascending 
            // bubble sort
            PointOfInterest temp;
            for (int j = 0; j <= points.Length - 2; j++)
            {
                for (int i = 0; i <= points.Length - 2; i++)
                {
                    if (points[i].location > points[i + 1].location)
                    {
                        temp = points[i + 1];
                        points[i + 1] = points[i];
                        points[i] = temp;
                    }
                }
            }

            // converting PointOfInterest[] to Tuple<PointOfInterest, bool>[]
            for (int i = 0; i < points.Count(); i++)
            {
                sortedPoints[i] = Tuple.Create(points[i], 10);
            }
            return sortedPoints;
        }
        public void getStranded()
        {
            this.hasFinished = true;
        }
        public void finish()
        {
            Console.WriteLine("Finished route, well done!");
            this.hasFinished = true;
        }

        public void arriveAtPoint()
        {
            this.atWaypointNumber++;
        }
    }

}