using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tea
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            return View(await _context.blogs.ToListAsync());
        }

          // GET: Blog
        public async Task<IActionResult> Manage()
        {
            return View(await _context.blogs.ToListAsync());
        }


        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("BlogId,Title,Content,Author")] Blog blog)
        {
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
                    }
                }
            }
            
            if (ModelState.IsValid)
            {
                if(file != null && file.Length != 0) {
                    Console.WriteLine("file is not null");
                    var uniqueFileName = GetUniqueFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", uniqueFileName);
                    var imagePath = $"img/{uniqueFileName}";
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    blog.ImageUrl = imagePath;
                }
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }
            return View(blog);
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? file, [Bind("BlogId,Title,Content,Author")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentItem = await _context.blogs.FindAsync(id);
                if (currentItem == null)
                {
                    return NotFound();
                }
                try
                {
                    if(file != null && file.Length != 0) {
                        Console.WriteLine("file is not null");
                        var uniqueFileName = GetUniqueFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", uniqueFileName);
                        var imagePath = $"img/{uniqueFileName}";
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        blog.ImageUrl = imagePath;
                    } else {
                        blog.ImageUrl = currentItem.ImageUrl;
                    }
                    _context.ChangeTracker.Clear();
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Manage));
            }
            return View(blog);
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.blogs.FindAsync(id);
            if (blog != null)
            {
                _context.blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Manage));
        }

        private bool BlogExists(int id)
        {
            return _context.blogs.Any(e => e.BlogId == id);
        }


        private string GetUniqueFileName(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            return $"{fileNameWithoutExtension}_{Guid.NewGuid()}{fileExtension}";
        }
    }
}
