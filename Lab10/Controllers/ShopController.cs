using Lab10.Data;
using Lab10.Models;
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
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.SelectedCategoryId = cat_id;
            var articles = _context.Articles.Include(a => a.Category);

            if (cat_id != null)
            {
                var category = await _context.Categories.Include(c => c.Articles).FirstOrDefaultAsync(m => m.Id == cat_id);
                if (category != null)
                {
                    if (category.Articles != null)
                    {
                        return View(category.Articles.ToList());
                    }
                }
            }

            return View(await articles.ToListAsync());
        }
    }
}
