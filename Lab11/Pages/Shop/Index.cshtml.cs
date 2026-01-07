using Lab11.Data;
using Lab11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;

        public IndexModel(ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; } = default!;

        public async Task OnGet(int? cat_id)
        {
            ViewData["ShowButtons"] = false;
            ViewData["Categories"] = await _context.Categories.ToListAsync();
            ViewData["SelectedCategoryId"] = cat_id;
            var articles = _context.Articles.Include(a => a.Category);

            if (cat_id != null)
            {
                var category = await _context.Categories.Include(c => c.Articles).FirstOrDefaultAsync(m => m.Id == cat_id);
                if (category != null)
                {
                    if (category.Articles != null)
                    {
                        Articles = category.Articles.ToList();
                    }
                }
            }

            else
            {
                Articles = await articles.ToListAsync();
            }
        }
    }
}
