using Lab10.Data;
using Lab10.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleApiController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ArticleApiController(ShopDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/ArticleApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        // GET: api/ArticleApi/GetNext
        [HttpGet("GetNext")]
        public async Task<ActionResult<IEnumerable<Article>>> GetNextArticles(int lastId, int size, int? cat_id)
        {
            var articles = _context.Articles.Include(a => a.Category).OrderBy(a => a.Id);

            if (cat_id != null)
            {
                articles = articles.Where(a => a.CategoryId == cat_id).OrderBy(a => a.Id);
            }

            var articlesJson = articles
                .Where(a => a.Id > lastId)
                .Take(size)
                .Select(a => new
                {
                    id = a.Id,
                    name = a.Name,
                    price = a.Price,
                    imageUrl = a.ImageUrl,
                    categoryId = a.CategoryId,
                    categoryName = a.Category != null ? a.Category.Name : "None"
                });

            if(!articlesJson.Any()) return NoContent();

            return Ok(articlesJson);
        }

        // GET: api/ArticleApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/ArticleApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminRole")]
        public async Task<IActionResult> PutArticle(int id, ArticleDto articleDto)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            article.Name = articleDto.Name;
            article.Price = articleDto.Price;
            article.CategoryId = articleDto.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ArticleApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminRole")]
        public async Task<ActionResult<Article>> PostArticle(ArticleDto articleDto)
        {
            var article = new Article
            {
                Name = articleDto.Name,
                Price = articleDto.Price,
                CategoryId = articleDto.CategoryId
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, articleDto);
        }

        // DELETE: api/ArticleApi/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminRole")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

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

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
