using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookSite.Models;

namespace CookSite.Areas.Management.Controllers
{
    [Area("Management")]
    public class MediaController : Controller
    {
        private readonly CookSiteContext _context;

        public MediaController()
        {
            _context = new CookSiteContext();
        }

        // GET: Management/Media
        public async Task<IActionResult> Index()
        {
            var cookSiteContext = _context.Media.Include(m => m.MediaType).Include(m => m.Recipe);
            return View(await cookSiteContext.ToListAsync());
        }

        // GET: Management/Media/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medium = await _context.Media
                .Include(m => m.MediaType)
                .Include(m => m.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medium == null)
            {
                return NotFound();
            }

            return View(medium);
        }

        // GET: Management/Media/Create
        public IActionResult Create()
        {
            ViewData["MediaTypeId"] = new SelectList(_context.MediaTypes, "Id", "Id");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
            return View();
        }

        // POST: Management/Media/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Path,AltText,MediaTypeId,RecipeId")] Medium medium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MediaTypeId"] = new SelectList(_context.MediaTypes, "Id", "Id", medium.MediaTypeId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", medium.RecipeId);
            return View(medium);
        }

        // GET: Management/Media/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medium = await _context.Media.FindAsync(id);
            if (medium == null)
            {
                return NotFound();
            }
            ViewData["MediaTypeId"] = new SelectList(_context.MediaTypes, "Id", "Id", medium.MediaTypeId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", medium.RecipeId);
            return View(medium);
        }

        // POST: Management/Media/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Path,AltText,MediaTypeId,RecipeId")] Medium medium)
        {
            if (id != medium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediumExists(medium.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MediaTypeId"] = new SelectList(_context.MediaTypes, "Id", "Id", medium.MediaTypeId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", medium.RecipeId);
            return View(medium);
        }

        // GET: Management/Media/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medium = await _context.Media
                .Include(m => m.MediaType)
                .Include(m => m.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medium == null)
            {
                return NotFound();
            }

            return View(medium);
        }

        // POST: Management/Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medium = await _context.Media.FindAsync(id);
            if (medium != null)
            {
                _context.Media.Remove(medium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediumExists(int id)
        {
            return _context.Media.Any(e => e.Id == id);
        }
    }
}
