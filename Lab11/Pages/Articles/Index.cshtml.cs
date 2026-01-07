using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab11.Data;
using Lab11.Models;

namespace Lab11.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly Lab11.Data.ShopDbContext _context;

        public IndexModel(Lab11.Data.ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ViewData["ShowButtons"] = true;
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
        }
    }
}
