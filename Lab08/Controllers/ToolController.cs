using Microsoft.AspNetCore.Mvc;

public class ToolController : Controller
{
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

    [Route("Solve/{a}/{b}/{c}")]
    public IActionResult Solve(double a, double b, double c)
    {
        double[]? result = SolveSquareEquation(a, b, c);
        ViewBag.Equation = $"{a}x^2 + {b}x + {c} = 0";
        string message = "";
        string solution = "";
        string cssClass = "";
        
        if (result == null)
        {
            message = "Równanie tożsamościowe.";
            solution = "nieskończenie wiele rozwiązań.";
            cssClass = "inf-sol";
        }
        else if (result.Length == 0)
        {
            message = "Równanie sprzeczne lub delta ujemna.";
            solution = "brak rozwiązań";
            cssClass = "zero-sol";
        }
        else if (result.Length == 1)
        {
            message = "Jedno rozwiązanie.";
            solution = $"x=";
            cssClass = "one-sol";
            ViewBag.x = result[0];
        }
        else
        {
            message = "Dwa rozwiązania.";
            cssClass = "two-sol";
            solution = "x1=";
            ViewBag.x1 = result[0];
            ViewBag.x2 = result[1];

        }

        ViewBag.message = message;
        ViewBag.solution = solution;
        ViewBag.cssClass = cssClass;
        return View();
    }
}