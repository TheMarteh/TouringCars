namespace TouringCars
{
    public class Analyzer
    {
        Car[] cars = new Car[0];
        int n = 0;
        public Analyzer(Car[] carsToAnalyze) : this()
        {
            this.setCars(carsToAnalyze);
        }
        public Analyzer()
        {
        }

        public String plotRoute(Car[]? cars = null)
        {
            Car[] carsToUse = cars == null ? this.cars : cars;
            String result = "Route Plotter v0.1\n";
            foreach (Car car in carsToUse)
            {
                result += $"Route van {car.owner}:\n";
                // var coords = car.route.getWaypointCoordinates();
                var drivenRoute = car.route.getDrivenRoute();

                int i = 1;
                foreach (var point in drivenRoute)
                {
                    if (point.Item3)
                    {
                        result += $"waypoint {i}: [{point.Item1.locationX}, {point.Item1.locationY}]. Distance to get here: {point.Item2} km, Fuel used so far: {point.Item4}\n";
                        i++;
                    }
                }
                result += $"Driven in this route: {car.getKMDriven()} km\n";
                result += $"Total route distance: {car.route.getLength().Item2} km\n";
            }
            return result += "\n";
        }

        public void setCars(Car[] cars)
        {
            this.cars = cars;
            this.n = cars.Length;
        }
        public Tuple<Automerken, int, int, int>[] AvgSpeedPerBrand()
        {
            int brandsTotal = Enum.GetNames(typeof(Automerken)).Length;

            Tuple<Automerken, int, int, int>[] result = new Tuple<Automerken, int, int, int>[brandsTotal];
            int[] amounts = new int[brandsTotal];
            int[] totalKilometers = new int[brandsTotal];
            Automerken[] merken = new Automerken[brandsTotal];

            foreach (Car car in this.cars)
            {
                for (int i = 0; i < brandsTotal; i++)
                {
                    merken[i] = (Automerken)i;
                    if (car.brand == merken[i])
                    {
                        amounts[i]++;
                        totalKilometers[i] += car.getKMDriven();
                    }
                }
            }

            for (int i = 0; i < brandsTotal; i++)
            {
                int avg = 0;
                if (amounts[i] > 0)
                {
                    avg = totalKilometers[i] / amounts[i];
                }
                result[i] = Tuple.Create(merken[i], amounts[i], totalKilometers[i], avg);
            }

            return result;
        }

        public String avgSpeedResults()
        {
            String result = "";
            var _avgspeedresults = AvgSpeedPerBrand();
            foreach (var item in _avgspeedresults)
            {
                result += $"{item.Item1}: {item.Item2} cars. They drove a combined {item.Item3} kilometers, averaging {item.Item4} km per car\n";
            }
            return result + "\n";
        }

        public String avgRouteLength()
        {
            String result = "";
            int total = 0;
            foreach (Car car in this.cars)
            {
                total += car.route.getLength().Item2;
            }
            result += $"Average Route Length: {total / n} km\n";
            return result + "\n";
        }
    }
}