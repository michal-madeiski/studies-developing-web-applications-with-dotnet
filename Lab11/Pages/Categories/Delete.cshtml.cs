using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab11.Data;
using Lab11.Models;

namespace Lab11.Pages.Categories
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
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is not null)
            {
                Category = category;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var category = await _context.Categories.Include(c => c.Articles).FirstOrDefaultAsync(m => m.Id == id);

            if (category != null)
            {
                if (category.Articles != null)
                {
                    foreach (var article in category.Articles)
                    {
                        if (article.ImageUrl != null)
                        {
                            string path = Path.Combine(_environment.WebRootPath, "upload", article.ImageUrl);
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                        }
                    }
                }
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
