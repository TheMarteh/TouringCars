namespace TouringCars
{
    public class RoutePoint
    {
        public PointOfInterest poi;
        public int id;
        public int distanceToNextPoint;
        public Boolean hasReached;
        public int fuelUsedSoFar;

        public RoutePoint(int id, PointOfInterest poi, int distanceToNextPoint, Boolean hasReached, int fuelUsedSoFar)
        {
            this.id = id;
            this.poi = poi;
            this.distanceToNextPoint = distanceToNextPoint;
            this.hasReached = hasReached;
            this.fuelUsedSoFar = fuelUsedSoFar;
        }

        public Tuple<String, ValueChanger> arriveAtPoint(int usedFuel, int fuelLeft)
        {
            this.hasReached = true;
            this.fuelUsedSoFar = usedFuel;

            String result = "";
            ValueChanger valueChanger;

            switch (this.poi.type)
            {
                case POIType.start:
                    result += "Start: Let\'s go!\n";
                    valueChanger = new ValueChanger(0, 0, 0, 0, false);
                    return Tuple.Create(result, valueChanger);
                case POIType.terminator:
                    valueChanger = new ValueChanger(0, 0, 0, 0, true);
                    return Tuple.Create(result, valueChanger);
                case POIType.gas_station:
                    valueChanger = new ValueChanger(this.poi.value, this.poi.cost, 0, 0, false);
                    result += $"Arrived at waypoint {this.poi.name} at {this.distanceToNextPoint}km!\nFuel left: {fuelLeft}\n";
                    return Tuple.Create(result, valueChanger);
                case POIType.food:
                    valueChanger = new ValueChanger(0, this.poi.cost, this.poi.value, 0, false);
                    result += $"Arrived at waypoint {this.poi.name} at {this.distanceToNextPoint}km!\nFuel left: {fuelLeft}\n";
                    result += "Nom nom, lekker eten\n";
                    return Tuple.Create(result, valueChanger);
                default:
                    valueChanger = new ValueChanger(0, 0, 0, 0, false);
                    result += $"Arrived at waypoint {this.poi.name} at {this.distanceToNextPoint}km!\nFuel left: {fuelLeft}\n";
                    return Tuple.Create(result, valueChanger);
            }
        }
        public void setTerminator()
        {
            this.poi.type = POIType.terminator;
        }
    }
}