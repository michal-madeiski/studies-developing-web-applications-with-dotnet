using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab11.Data;
using Lab11.Models;

namespace Lab11.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly Lab11.Data.ShopDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(Lab11.Data.ShopDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Article.FormImage != null)
                {
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "upload");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Article.FormImage.FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                    {
                        await Article.FormImage.CopyToAsync(fileStream);
                    }
                    Article.ImageUrl = fileName;
                }

                _context.Add(Article);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }
    }
}
