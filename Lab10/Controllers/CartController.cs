using Lab10.Data;
using Lab10.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lab10.Controllers
{
    [Authorize(Policy = "NotAdminRole")]
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;

        public CartController(ShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartData = GetCartFromCookies();

            ViewData["ShowCart"] = true;
            ViewData["ShowButtons"] = false;
            ViewData["ShowCartButtons"] = true;

            ViewBag.TotalPrice = cartData.TotalPrice;
            ViewBag.Quantities = cartData.Quantities;
            return View(cartData.Articles);
        }

        [Authorize]
        public IActionResult Checkout()
        {
            var cartData = GetCartFromCookies();

            if (!cartData.Articles.Any()) return RedirectToAction(nameof(Index));

            ViewData["ShowCart"] = true;
            ViewData["ShowButtons"] = false;
            ViewData["ShowCartButtons"] = false;

            ViewBag.TotalPrice = cartData.TotalPrice;
            ViewBag.Quantities = cartData.Quantities;
            return View(cartData);
        }

        [HttpPost]
        public IActionResult ConfirmOrder(string firstName, string lastName, string address, string zipCode, string city, string paymentMethod)
        {
            var cartData = GetCartFromCookies();

            if (!cartData.Articles.Any()) return RedirectToAction(nameof(Index));

            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("article"))
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }

            var orderDetails = new ConfirmOrderViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                ZipCode = zipCode,
                City = city,
                PaymentMethod = paymentMethod
            };

            TempData["OrderDetails"] = JsonSerializer.Serialize(orderDetails);
            return RedirectToAction(nameof(ConfirmOrderGet));
        }

        [HttpGet]
        public IActionResult ConfirmOrderGet()
        {
            var data = TempData["OrderDetails"] as string;

            if (string.IsNullOrEmpty(data))
            {
                return RedirectToAction("Index");
            }

            var orderDetails = JsonSerializer.Deserialize<ConfirmOrderViewModel>(data);
            return View("ConfirmOrder", orderDetails);
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

        private CartViewModel GetCartFromCookies()
        {
            var model = new CartViewModel();

            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("article"))
                {
                    if (int.TryParse(cookie.Value, out int quantity) && int.TryParse(cookie.Key.Substring(7), out int artId))
                    {
                        var article = _context.Articles.Include(a => a.Category).FirstOrDefault(a => a.Id == artId);
                        if (article != null)
                        {
                            model.Articles.Add(article);
                            model.Quantities.Add(artId, quantity);
                            model.TotalPrice += article.Price * quantity;
                        }
                        else
                        {
                            Response.Cookies.Delete(cookie.Key);
                        }
                    }
                }
            }

            model.Articles = model.Articles.OrderBy(x => x.Name).ToList();
            return model;
        }
    }
}
