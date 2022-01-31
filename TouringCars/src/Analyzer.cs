namespace TouringCars
{
    public class Analyzer
    {
        public static String AvgSpeedPerBrand(Car[] carsToAnalyze)
        {
            String result = "";
            int brandsTotal = Enum.GetNames(typeof(POIType)).Length - 1;

            int[] amounts = new int[brandsTotal];
            Automerken[] merken = new Automerken[brandsTotal];
            foreach (Car car in carsToAnalyze)
            {
                for (int i = 0; i < brandsTotal; i++)
                {
                    if (car.brand == merken[i])
                    {
                        amounts[i]++;
                    }
                }
            }

            for (int i = 0; i < brandsTotal; i++)
            {
                result += $"{merken[i]}: {amounts[i]} cars.\n";
            }

            return result;
        }
    }
}