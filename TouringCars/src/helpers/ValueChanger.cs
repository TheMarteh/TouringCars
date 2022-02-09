namespace TouringCars
{
    public class ValueChanger
    {
        // helper class to make a callback to the car.
        public int fuelToChange { get; }
        public int costToChange { get; }
        public int famineToChange { get; }
        public int sleepToChange { get; }
        public bool isFinished { get; }
        public bool isStarting { get; }

        public ValueChanger(int fuelToChange = 0, int costToChange = 0, int famineToChange = 0, int sleepToChange = 0, bool isFinished = false, bool isStarting = false)
        {
            this.fuelToChange = fuelToChange;
            this.costToChange = costToChange;
            this.famineToChange = famineToChange;
            this.sleepToChange = sleepToChange;
            this.isFinished = isFinished;
            this.isStarting = isStarting;
        }
    }
}