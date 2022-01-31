namespace TouringCars
{
    public class PointOfInterest
    {
        public String name { get; set; }
        public int location { get; set; }
        public POIType type;

        public int cost;
        public int value;

        public PointOfInterest(String name, int location, POIType type, int value = 0, int cost = 0) : this(name, location, value, cost)
        {
            this.type = type;
        }
        public PointOfInterest(String name, int location, int value = 0, int cost = 0) : this(name, value, cost)
        {
            this.location = location;
        }

        public PointOfInterest(String name, int value = 0, int cost = 0)
        {
            this.name = name;
            Random rnd = new Random();
            this.location = rnd.Next(0, WorkingParams.maxDistance);
            this.type = (POIType)rnd.Next(0, Enum.GetNames(typeof(POIType)).Length - 1);
        }

    }

}