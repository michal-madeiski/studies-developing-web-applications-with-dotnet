using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private static int _n;
    private static int _randValue;
    private static int _counter = 0;
    private static Random _random = new Random();

    [Route("Set")]
    [Route("Set,{n}")]
    public IActionResult Set(int n=10)
    {
        _n = n;
        _randValue = _random.Next(0, _n);
        _counter = 0;
        ViewBag.cssClass = "set";
        ViewBag.message = $"Ustawiono zakres losowania: od 0 do {_n-1} włącznie.";
        ViewBag.messageCount = $"Liczba prób została wyzerowana.";
        //ViewBag.rand = _randValue;
        return View("GameResult");
    }

    [Route("Draw")]
    public IActionResult Draw()
    {
        _randValue = _random.Next(0, _n);
        _counter = 0;
        ViewBag.cssClass = "draw";
        ViewBag.message = $"Wylosowano nową tajemniczą liczbę.";
        ViewBag.messageCount = $"Liczba prób została wyzerowana, zakres to: od 0 do {_n - 1} włącznie.";
        //ViewBag.rand = _randValue;
        return View("GameResult");
    }

    [Route("Guess,{x}")]
    public IActionResult Guess(int x)
    {
        _counter++;
        ViewBag.message = $"Twój typ liczby to: {x}.";
        string messageResult = "";
        string messageCount = "";
        string cssClass = "";
        if (x != _randValue)
        {
            messageCount = $"Niestety, nie trafiłeś. Twoja liczba prób to: {_counter}.";
            cssClass = "incorrect";
            if (x > _randValue)
            {
                messageResult = $"Twój typ liczby to... za dużo.";
            } else
            {
                messageResult = $"Twój typ liczby to... za mało.";
            }
        }
        else
        {
            cssClass = "correct";
            messageCount = $"Gratulacje, trafiłeś! Liczba prób potrzebna do wygranej to: {_counter}.";
            _counter = 0;
        }
        ViewBag.cssClass = cssClass;
        ViewBag.messageCount = messageCount;
        ViewBag.messageResult = messageResult;
        //ViewBag.rand = _randValue;
        return View("GameResult");
    }
}