using Microsoft.AspNetCore.Mvc;

public class ToolController : Controller
{
    [Route("Solve/{a}/{b}/{c}")]
    public IActionResult Solve(double a, double b, double c)
    {
        ViewBag.Equation = $"{a}x^2 + {b}x + {c} = 0";
        string message = "";
        string solution = "";
        string cssClass = "";
        
        if (a == 0 && b == 0 && c == 0)
        {
            message = "Równanie tożsamościowe.";
            solution = "nieskończenie wiele rozwiązań";
            cssClass = "inf-sol";
        }
        else if (a == 0 && b == 0 && c != 0)
        {
            message = "Równanie sprzeczne.";
            solution = "brak rozwiązań";
            cssClass = "zero-sol";
        }
        else if (a == 0 && b != 0 && c != 0)
        {
            double x = (-c) / b;
            message = "Równanie liniowe.";
            solution = $"jedno rozwiązanie: x={x}";
            cssClass = "one-sol";
        }
        else
        {
            double delta = b * b - 4 * a * c;
            message = "Równanie kwadratowe.";
            if (delta < 0)
            {
                message += " Delta ujemna.";
                solution = "brak rozwiązań";
                cssClass = "zero-sol";
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                message += " Delta równa zero.";
                solution = $"jedno rozwiązanie: x={x}";
                cssClass = "one-sol";
            }
            else
            {
                double sqrtDelta = Math.Sqrt(delta);
                double x1 = (-b + sqrtDelta) / 2;
                double x2 = (-b - sqrtDelta) / 2;
                message += " Delta dodatnia.";
                solution = $"dwa rozwiązania: x1={x1}; x2={x2}";
                cssClass = "two-sol";
            }
        }

        ViewBag.message = message;
        ViewBag.solution = solution;
        ViewBag.cssClass = cssClass;
        return View();
    }
}