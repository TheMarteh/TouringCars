namespace TouringCars
{
    public class PointOfInterest
    {
        public String name { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }
        public POIType type;
        public int value;
        public int cost;

        public PointOfInterest(String name, int[]? location = null, POIType? type = null, int value = 0, int cost = 0)
        {
            Random rnd = new Random();
            this.type = (POIType)((type != null) ? type : (POIType)rnd.Next(0, Enum.GetNames(typeof(POIType)).Length - 2));
            this.locationX = (location != null) ? location[0] : rnd.Next(0, WorkingParams.maxDistance);
            this.locationY = (location != null) ? location[1] : rnd.Next(0, WorkingParams.maxDistance);
            this.name = name;
            this.value = value;
            this.cost = cost;
        }

        public Tuple<int, int> getCoordinates()
        {
            Tuple<int, int> coords = Tuple.Create(this.locationX, this.locationY);
            return coords;
        }

    }

}