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
            String result = "      ##$$%%//   Route Plotter v0.1   \\\\%%$$##\n\n";
            foreach (Car car in carsToUse)
            {
                RoutePoint[] drivenRoute = car.route.getDrivenRoute();
                result += $"Route van {car.owner}";
                result += !drivenRoute.Last().hasReached ? " ( not " : " ( ";
                result += "finished. )\n";
                // var coords = car.route.getWaypointCoordinates();

                int i = 0;
                foreach (var point in drivenRoute)
                {
                    if (point.hasReached)
                    {
                        String p1 = $"waypoint {i}: [{point.poi.locationX}, {point.poi.locationY}]";
                        String p2 = $"Distance and fuel to get here: {point.distanceToNextPoint} km, {point.fuelUsedSoFar} L\n";
                        String devider = "";
                        while (p1.Length + p2.Length + devider.Length < FixedParams.maxScreenWidth)
                        {
                            devider += " ";
                        }
                        result += p1 + devider + p2;
                        i++;
                    }
                }
                result += $"Driven in this route: {car.getKMDriven()} km\n";
                int conditionalDistance = car.route.getLength().Item2 < int.MaxValue ? car.route.getLength().Item2 : 0;
                result += $"Total route distance: {conditionalDistance} km\n\n";
            }
            return result;
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