using Lab10.Data;
using Lab10.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Controllers
{
    public class ShopController : Controller
    {

        private readonly ShopDbContext _context;

        public ShopController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? cat_id)
        {
            ViewData["ShowButtons"] = false;
            ViewData["ShowCart"] = false;
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.SelectedCategoryId = cat_id;

            int startSize = 3;

            if (cat_id != null)
            {
                var category = await _context.Categories.Include(c => c.Articles).FirstOrDefaultAsync(m => m.Id == cat_id);
                if (category != null)
                {
                    if (category.Articles != null)
                    {
                        return View(category.Articles.OrderBy(a => a.Id).Take(startSize).ToList());
                    }
                }
            }

            var articles = _context.Articles.Include(a => a.Category).OrderBy(a => a.Id);
            return View(await articles.Take(startSize).ToListAsync());
        }

        [Authorize(Policy = "NotAdminRole")]
        public IActionResult AddToCart(int id, int? cat_id) 
        {
            string cookie_key = "article" + id;
            int cookie_value = 1;

            if (Request.Cookies.ContainsKey(cookie_key))
            {
                cookie_value += int.Parse(Request.Cookies[cookie_key] ?? "0");
            }

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            Response.Cookies.Append(cookie_key, cookie_value.ToString(), options);

            //return RedirectToAction(nameof(Index), new { cat_id = cat_id });
            return Ok();
        }
    }
}
