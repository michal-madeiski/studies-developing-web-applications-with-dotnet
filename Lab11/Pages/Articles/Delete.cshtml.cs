using Lab11.Data;
using Lab11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab11.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly Lab11.Data.ShopDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeleteModel(Lab11.Data.ShopDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article is not null)
            {
                Article = article;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                if (article.ImageUrl != null)
                {
                    string path = Path.Combine(_environment.WebRootPath, "upload", article.ImageUrl);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
