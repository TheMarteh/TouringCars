namespace TouringCars
{
    public class RoutePoint
    {
        public PointOfInterest poi;
        public int id;
        public int distanceToNextPoint;
        public Boolean hasReached;
        public int fuelUsedSoFar;
        public int fuelUsedSinceLast = 0;
        public int distanceSoFar = 0;

        public RoutePoint(int id, PointOfInterest poi, int distanceToNextPoint, Boolean hasReached, int fuelUsedSoFar)
        {
            this.id = id;
            this.poi = poi;
            this.distanceToNextPoint = distanceToNextPoint;
            this.hasReached = hasReached;
            this.fuelUsedSoFar = fuelUsedSoFar;
        }

        public Tuple<String, ValueChanger> arriveAtPoint(int waypointNumber, int carTotalUsedFuel, int fuelLeft, int drivenSoFar, int fuelUsedToGetHere)
        {
            this.hasReached = true;
            this.fuelUsedSoFar = carTotalUsedFuel;
            this.fuelUsedSinceLast = fuelUsedToGetHere;

            String result = "";
            result += $"Arrived at {waypointNumber}) ({this.poi.type}) {this.poi.name}. Fuel left: {fuelLeft}, Travelled since previous: {this.distanceToNextPoint} Total: {drivenSoFar}\n";
            ValueChanger valueChanger;

            switch (this.poi.type)
            {
                case POIType.start:
                    valueChanger = new ValueChanger(isStarting: true);
                    return Tuple.Create(result, valueChanger);
                case POIType.terminator:
                    valueChanger = new ValueChanger(isFinished: true);
                    return Tuple.Create(result, valueChanger);
                case POIType.gas_station:
                    valueChanger = new ValueChanger(fuelToChange: this.poi.value, costToChange: -this.poi.cost);
                    return Tuple.Create(result, valueChanger);
                case POIType.food:
                    valueChanger = new ValueChanger(costToChange: -this.poi.cost, famineToChange: -this.poi.value);
                    return Tuple.Create(result, valueChanger);
                case POIType.work:
                    valueChanger = new ValueChanger(costToChange: this.poi.cost);
                    return Tuple.Create(result, valueChanger);
                case POIType.hangout:
                    valueChanger = new ValueChanger(famineToChange: this.poi.value);
                    return Tuple.Create(result, valueChanger);
                default:
                    valueChanger = new ValueChanger();
                    return Tuple.Create(result, valueChanger);
            }
        }
        public RoutePoint setTerminator()
        {
            RoutePoint terminator = new RoutePoint(this.id, new PointOfInterest(this.poi.name, new int[] { this.poi.locationX, this.poi.locationY }, POIType.terminator), this.distanceToNextPoint, false, 0);
            return terminator;
        }
    }
}