namespace TouringCars
{
    public class ValueChanger
    {
        // helper class to make a callback to the car.
        public int fuelToChange;
        public int costToChange;
        public int famineToChange;
        public int sleepToChange;
        public bool isFinished;
        public bool isStarting;

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