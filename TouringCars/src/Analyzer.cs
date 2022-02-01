namespace TouringCars
{
    public class Analyzer
    {
        public static Tuple<Automerken, int, int, int>[] AvgSpeedPerBrand(Car[] carsToAnalyze)
        {
            int brandsTotal = Enum.GetNames(typeof(Automerken)).Length;

            Tuple<Automerken, int, int, int>[] result = new Tuple<Automerken, int, int, int>[brandsTotal];
            int[] amounts = new int[brandsTotal];
            int[] totalKilometers = new int[brandsTotal];

            Automerken[] merken = new Automerken[brandsTotal];
            foreach (Car car in carsToAnalyze)
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

        public static String avgSpeedResults(Car[] carsToAnalyze)
        {
            String result = "";
            var _avgspeedresults = AvgSpeedPerBrand(carsToAnalyze);
            foreach (var item in _avgspeedresults)
            {
                result += $"{item.Item1}: {item.Item2} cars. They drove a combined {item.Item3} kilometers, averaging {item.Item4} km per car\n";
            }
            return result;
        }
    }
}