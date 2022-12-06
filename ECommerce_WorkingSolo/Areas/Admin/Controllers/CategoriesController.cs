using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using ECommerce_WorkingSolo.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce_WorkingSolo.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = "Admin")]
  //[Authorize(Policy = "RequireAdminRole")]
  public class CategoriesController: Controller
  {
    private readonly ECommerceDbContext _context;

    public CategoriesController( ECommerceDbContext context )
    {
      _context = context;
    }

    // GET: Admin/Categories
    public async Task<IActionResult> Index()
    {
      return View(await _context.Categories.ToListAsync());
    }

    // GET: Admin/Categories/Details/5
    public async Task<IActionResult> Details( int? id )
    {
      if (id == null || _context.Categories == null)
      {
        return NotFound();
      }

      var category = await _context.Categories
          .FirstOrDefaultAsync(m => m.Id == id);
      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    // GET: Admin/Categories/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Admin/Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create( [Bind("Id,Name,Description,ImagePath,CategoryName")] Category category )
    {
      if (ModelState.IsValid)
      {
        _context.Add(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(category);
    }

    // GET: Admin/Categories/Edit/5
    public async Task<IActionResult> Edit( int? id )
    {
      if (id == null || _context.Categories == null)
      {
        return NotFound();
      }

      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }
      return View(category);
    }

    // POST: Admin/Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit( int id, [Bind("Id,Name,Description,ImagePath")] Category category )
    {
      if (id != category.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(category);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CategoryExists(category.Id))
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
      return View(category);
    }

    // GET: Admin/Categories/Delete/5
    public async Task<IActionResult> Delete( int? id )
    {
      if (id == null || _context.Categories == null)
      {
        return NotFound();
      }

      var category = await _context.Categories
          .FirstOrDefaultAsync(m => m.Id == id);
      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    // POST: Admin/Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed( int id )
    {
      if (_context.Categories == null)
      {
        return Problem("Entity set 'ECommerceDbContext.Categories'  is null.");
      }
      var category = await _context.Categories.FindAsync(id);
 
      if (category != null)
      {
        _context.Categories.Remove(category);

        //delete all associated products if you get rid of a category
        var productQuery = await (from item in _context.Products
                                  where item.CategoryId == id
                                  select item).ToListAsync();
        foreach (var item in productQuery)
        {
          _context.Products.Remove(item);
        }
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists( int id )
    {
      return _context.Categories.Any(e => e.Id == id);
    }
  }
}
