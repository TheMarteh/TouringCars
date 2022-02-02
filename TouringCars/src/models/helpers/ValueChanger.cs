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
        public ValueChanger(int fuelToChange, int costToChange, int famineToChange, int sleepToChange, bool isFinished)
        {
            this.fuelToChange = fuelToChange;
            this.costToChange = costToChange;
            this.famineToChange = famineToChange;
            this.sleepToChange = sleepToChange;
            this.isFinished = isFinished;
        }
    }
}