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

        public PointOfInterest(String name, int[] location, POIType type, int value = 0, int cost = 0) : this(name, location, value, cost)
        {
            this.type = type;
        }
        public PointOfInterest(String name, int[] location, int value = 0, int cost = 0) : this(name, value, cost)
        {
            this.locationX = location[0];
            this.locationY = location[1];
        }

        public PointOfInterest(String name, int value, int cost)
        {
            this.name = name;
            this.value = value;
            this.cost = cost;
            Random rnd = new Random();
            this.locationX = rnd.Next(0, WorkingParams.maxDistance);
            this.locationY = rnd.Next(0, WorkingParams.maxDistance);
            this.type = (POIType)rnd.Next(0, Enum.GetNames(typeof(POIType)).Length - 2);
        }

    }

}