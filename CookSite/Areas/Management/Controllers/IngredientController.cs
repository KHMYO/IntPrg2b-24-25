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
    public class IngredientController : Controller
    {
        private readonly CookSiteContext _context;

        public IngredientController()
        {
            _context = new CookSiteContext();
        }

        // GET: Management/Ingredient
        public async Task<IActionResult> Index()
        {
            var cookSiteContext = _context.Ingredients.Include(i => i.AmountUnit).Include(i => i.Recipe);
            return View(await cookSiteContext.ToListAsync());
        }

        // GET: Management/Ingredient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
                .Include(i => i.AmountUnit)
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Management/Ingredient/Create
        public IActionResult Create()
        {
            ViewData["AmountUnitId"] = new SelectList(_context.Units, "Id", "Id");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
            return View();
        }

        // POST: Management/Ingredient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,AmountUnitId,RecipeId")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmountUnitId"] = new SelectList(_context.Units, "Id", "Id", ingredient.AmountUnitId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", ingredient.RecipeId);
            return View(ingredient);
        }

        // GET: Management/Ingredient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            ViewData["AmountUnitId"] = new SelectList(_context.Units, "Id", "Id", ingredient.AmountUnitId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", ingredient.RecipeId);
            return View(ingredient);
        }

        // POST: Management/Ingredient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,AmountUnitId,RecipeId")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.Id))
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
            ViewData["AmountUnitId"] = new SelectList(_context.Units, "Id", "Id", ingredient.AmountUnitId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", ingredient.RecipeId);
            return View(ingredient);
        }

        // GET: Management/Ingredient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
                .Include(i => i.AmountUnit)
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Management/Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
