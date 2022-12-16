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

namespace ECommerce_WorkingSolo.Controllers
{
  [Authorize(Roles = "Admin, Editor, Shopper")]
  public class ShoppingCartsController: Controller
  {
    private readonly ECommerceDbContext _context;

    public ShoppingCartsController( ECommerceDbContext context )
    {
      _context = context;
    }

    // GET: ShoppingCarts
    public async Task<IActionResult> Index( string userId )
    {
      var cart = await _context.ShoppingCart.Where(sc => sc.UserID == userId).Include(c => c.Cart).FirstOrDefaultAsync();
      var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
      if (cart == null || user == null)
      {
        return NotFound();
      }

      ViewBag.UsersName = user.UserName;
      ViewBag.UsersCart = cart;
      ViewBag.UsersId = user.Id;
      return View();
    }

    //public async Task<IActionResult> Details(string productId)
    //{
    //  var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
    //  if(product == null)
    //  {
    //    return RedirectToAction("Index");
    //  }


  }

  //// GET: ShoppingCarts/Details/5
  //public async Task<IActionResult> Details( string id )
  //{
  //  if (id == null || _context.ShoppingCart == null)
  //  {
  //    return NotFound();
  //  }

  //  var shoppingCart = await _context.ShoppingCart
  //      .FirstOrDefaultAsync(m => m.Id == id);
  //  if (shoppingCart == null)
  //  {
  //    return NotFound();
  //  }

  //  return View(shoppingCart);
  //}

  //// GET: ShoppingCarts/Create
  //public IActionResult Create()
  //{
  //  return View();
  //}

  //// POST: ShoppingCarts/Create
  //// To protect from overposting attacks, enable the specific properties you want to bind to.
  //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  //[HttpPost]
  //[ValidateAntiForgeryToken]
  //public async Task<IActionResult> Create( [Bind("Id,UserID")] ShoppingCart shoppingCart )
  //{
  //  if (ModelState.IsValid)
  //  {
  //    _context.Add(shoppingCart);
  //    await _context.SaveChangesAsync();
  //    return RedirectToAction(nameof(Index));
  //  }
  //  return View(shoppingCart);
  //}

  //// GET: ShoppingCarts/Edit/5
  //public async Task<IActionResult> Edit( string id )
  //{
  //  if (id == null || _context.ShoppingCart == null)
  //  {
  //    return NotFound();
  //  }

  //  var shoppingCart = await _context.ShoppingCart.FindAsync(id);
  //  if (shoppingCart == null)
  //  {
  //    return NotFound();
  //  }
  //  return View(shoppingCart);
  //}

  //// POST: ShoppingCarts/Edit/5
  //// To protect from overposting attacks, enable the specific properties you want to bind to.
  //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  //[HttpPost]
  //[ValidateAntiForgeryToken]
  //public async Task<IActionResult> Edit( string id, [Bind("Id,UserID")] ShoppingCart shoppingCart )
  //{
  //  if (id != shoppingCart.Id)
  //  {
  //    return NotFound();
  //  }

  //  if (ModelState.IsValid)
  //  {
  //    try
  //    {
  //      _context.Update(shoppingCart);
  //      await _context.SaveChangesAsync();
  //    }
  //    catch (DbUpdateConcurrencyException)
  //    {
  //      if (!ShoppingCartExists(shoppingCart.Id))
  //      {
  //        return NotFound();
  //      }
  //      else
  //      {
  //        throw;
  //      }
  //    }
  //    return RedirectToAction(nameof(Index));
  //  }
  //  return View(shoppingCart);
  //}

  //// GET: ShoppingCarts/Delete/5
  //public async Task<IActionResult> Delete( string id )
  //{
  //  if (id == null || _context.ShoppingCart == null)
  //  {
  //    return NotFound();
  //  }

  //  var shoppingCart = await _context.ShoppingCart
  //      .FirstOrDefaultAsync(m => m.Id == id);
  //  if (shoppingCart == null)
  //  {
  //    return NotFound();
  //  }

  //  return View(shoppingCart);
  //}

  //// POST: ShoppingCarts/Delete/5
  //[HttpPost, ActionName("Delete")]
  //[ValidateAntiForgeryToken]
  //public async Task<IActionResult> DeleteConfirmed( string id )
  //{
  //  if (_context.ShoppingCart == null)
  //  {
  //    return Problem("Entity set 'ECommerceDbContext.ShoppingCart'  is null.");
  //  }
  //  var shoppingCart = await _context.ShoppingCart.FindAsync(id);
  //  if (shoppingCart != null)
  //  {
  //    _context.ShoppingCart.Remove(shoppingCart);
  //  }

  //  await _context.SaveChangesAsync();
  //  return RedirectToAction(nameof(Index));
  //}

  //private bool ShoppingCartExists( string id )
  //{
  //  return _context.ShoppingCart.Any(e => e.Id == id);
  //}
}
