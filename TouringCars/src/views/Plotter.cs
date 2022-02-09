namespace TouringCars
{
    public class Plotter
    {
        private int width = WorkingParams.maxDistance;
        public static String plotPoints(RoutePoint[] points, String name = "")
        {
            int width = WorkingParams.maxDistance;
            int celwidth = FixedParams.outputLogCelwidth;
            char yCharacter = '|';
            char xCharacter = '-';
            String xIndent = new String(' ', label(0, ' ').Length);
            String graphResult = "";
            String graphTitle = $"{xIndent} {name}\n";
            graphResult += graphTitle;
            String[][] graph = new String[width][];
            for (int y = width - 1; y >= 0; y--)
            {
                graph[y] = new String[width];
                for (int x = 0; x < width; x++)
                {
                    graph[y][x] = new String(' ', celwidth);
                }
            }

            foreach (var point in points)
            {
                if (point.hasReached)
                {
                    String celValue = "";
                    celValue = point.id.ToString();
                    while (celValue.Length < celwidth)
                    {
                        celValue += ' ';
                    }
                    graph[point.poi.locationX][point.poi.locationY] = celValue;
                }
            }

            for (int y = width - 1; y >= 0; y--)
            {
                graphResult += label(y, yCharacter);
                for (int x = 0; x < width; x++)
                {
                    graphResult += graph[x][y];
                }
                graphResult += "\n";
            }
            graphResult += xIndent + new String(xCharacter, width * celwidth) + "\n" + xIndent;
            for (int i = 0; i < width; i++)
            {
                String xlabel = i.ToString();
                while (xlabel.Length < celwidth)
                {
                    xlabel += " ";
                }
                graphResult += xlabel;
            }


            return graphResult + "\n\n";
        }
        private static String label(int x, char axisChar)
        {
            String result = "";
            if (x < 10)
            {
                result += $"{x}  ";
            }
            else
            {
                result += $"{x} ";
            }
            result += axisChar;
            return result;
        }
    }

}