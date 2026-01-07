using Lab10.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lab10.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;

        public CartController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ShowCart"] = true;
            ViewData["ShowButtons"] = false;

            var cart = new List<Lab10.Models.Article>();
            var quantities = new Dictionary<int, int>();

            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("article"))
                {
                    if (int.TryParse(cookie.Value, out int quantity) && int.TryParse(cookie.Key.Substring(7), out int artId))
                    {
                        var article = _context.Articles.FirstOrDefault(a => a.Id == artId);
                        if (article != null)
                        {
                            cart.Add(article);
                            quantities.Add(artId, quantity);
                        }
                        else
                        {
                            Response.Cookies.Delete(cookie.Key);
                        }
                    }
                }
            }

            ViewBag.Quantities = quantities;
            cart = cart.OrderBy(x => x.Name).ToList();
            return View(cart);
        }

        public IActionResult Plus(int id)
        {
            string cookieKey = "article" + id;

            if (Request.Cookies.ContainsKey(cookieKey))
            {
                int quantity = int.Parse(Request.Cookies[cookieKey] ?? "0");
                quantity++;

                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    IsEssential = true
                };

                Response.Cookies.Append(cookieKey, quantity.ToString(), options);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int id)
        {
            string cookieKey = "article" + id;

            if (Request.Cookies.ContainsKey(cookieKey))
            {
                int quantity = int.Parse(Request.Cookies[cookieKey] ?? "0");
                quantity--;

                if (quantity > 0)
                {
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),
                        IsEssential = true
                    };

                    Response.Cookies.Append(cookieKey, quantity.ToString(), options);
                } else
                {
                    Response.Cookies.Delete(cookieKey);
                }

                
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            string cookieKey = "article" + id;

            if (Request.Cookies.ContainsKey(cookieKey))
            {
                Response.Cookies.Delete(cookieKey);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
