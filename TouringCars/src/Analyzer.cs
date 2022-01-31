namespace TouringCars
{
    public class Analyzer
    {
        public static Tuple<Automerken, int>[] AvgSpeedPerBrand(Car[] carsToAnalyze)
        {
            int brandsTotal = Enum.GetNames(typeof(Automerken)).Length;
            Tuple<Automerken, int>[] result = new Tuple<Automerken, int>[brandsTotal];

            int[] amounts = new int[brandsTotal];
            Automerken[] merken = new Automerken[brandsTotal];
            foreach (Car car in carsToAnalyze)
            {
                for (int i = 0; i < brandsTotal; i++)
                {
                    merken[i] = (Automerken)i;
                    if (car.brand == merken[i])
                    {
                        amounts[i]++;
                    }
                }
            }

            for (int i = 0; i < brandsTotal; i++)
            {
                result[i] = Tuple.Create(merken[i], amounts[i]);
            }

            return result;
        }

        public static String avgSpeedResults(Car[] carsToAnalyze)
        {
            String result = "";
            var _avgspeedresults = AvgSpeedPerBrand(carsToAnalyze);
            foreach (var item in _avgspeedresults)
            {
                result += $"{item.Item1}: {item.Item2} cars";
            }
            return result;
        }
    }
}