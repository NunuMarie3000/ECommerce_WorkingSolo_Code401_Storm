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
using System.Data;

namespace ECommerce_WorkingSolo.Controllers
{
  public class ProductsController: Controller
  {
    private readonly ECommerceDbContext _context;

    public ProductsController( ECommerceDbContext context )
    {
      _context = context;
    }

    // GET: Products
    //[Authorize(Roles = "Shopper")]
    public async Task<IActionResult> Index()
    {
      //var eCommerceDbContext = _context.Products.Include(p => p.Category);
      //return View(await eCommerceDbContext.ToListAsync());
      return View(await _context.Products.ToListAsync());
    }

    // GET: Products/Details/5
    //[Authorize(Roles = "Shopper, Admin, Editor")]
    public async Task<IActionResult> Details( string? id )
    {
      if (id == null || _context.Products == null)
      {
        return NotFound();
      }

      var product = await _context.Products
          .FirstOrDefaultAsync(m => m.Id == id);
      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }

    private bool ProductExists( string id )
    {
      return _context.Products.Any(e => e.Id == id);
    }
  }
}
