class SquareEquation
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================================");
        Console.WriteLine("PROGRAM DO OBLICZANIA PIERWIASTKÓW RÓWNANIA KWADRATOWEGO");
        Console.WriteLine("========================================================");
        Console.WriteLine("równanie kwadratowe: ax^2 + bx + c = 0");

        double a = InputFactor("a");
        double b = InputFactor("b");
        double c = InputFactor("c");

        DisplaySolution(SolveSquareEquation(a, b, c));
        Console.WriteLine("========================================================");
    }

    static double InputFactor(string factor_name)
    {
        double factor;
        Console.Write($"{factor_name}=");
        while (!double.TryParse(Console.ReadLine(), out factor))
        {
            Console.WriteLine($"wpisana wartość to nie liczba - spróbuj ponownie");
            Console.Write($"{factor_name}=");
        }
        Console.WriteLine($"zapisano: {factor_name}={factor}");
        return factor;
    }

    static double[]? SolveSquareEquation(double a, double b, double c)
    {
        if (a == 0 && b == 0 && c == 0)
        {
            return null;
        }
        else if (a == 0 && b == 0 && c != 0)
        {
            return new double[0];
        }
        else if (a == 0 && b != 0 && c != 0)
        {
            double x = (-c) / b;
            return new double[] { x };
        }
        else
        {
            double delta = b * b - 4 * a * c;
            if (delta < 0)
            {
                return new double[0];
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                return new double[] { x };
            }
            else
            {
                double sqrtDelta = Math.Sqrt(delta);
                double x1 = (-b + sqrtDelta) / 2;
                double x2 = (-b - sqrtDelta) / 2;
                return new double[] { x1, x2 };
            }
        }
    }

    static void DisplaySolution(double[]? xArray)
    {

        Console.Write("ROZWIĄZANIE: ");
        if (xArray == null)
        {
            Console.Write("nieskończenie wiele rozwiązań\n");
        }
        else if (xArray.Length == 0)
        {
            Console.Write("brak rozwiązań\n");
        }
        else if (xArray.Length == 1)
        {
            double x = xArray[0];
            Console.Write("x={0}\n", x);
        }
        else
        {
            double x1 = xArray[0];
            double x2 = xArray[1];
            Console.Write($"x1={x1}; x2={x2}\n");
        }
    }
}