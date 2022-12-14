using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_WorkingSolo.Controllers
{
  [Authorize]
  public class TransactionsController: Controller
  {
    private readonly ECommerceDbContext _context;
    public TransactionsController(ECommerceDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      return View();
    }
    public IActionResult Buy( string? id)
    {
      //NOTE: COME BACK TO THIS
      return View();
    }

    public async Task<IActionResult> AddToCart( string id, string userId)
    {
      // NOTE: RETURN TO THIS
      var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
      if(user == null)
      {
        return NotFound();
      }

      var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();
      if(product == null)
      {
        return BadRequest();
      }

      //var userCart = _context.ShoppingCart.Where(sc => sc.UserID == user.Id).FirstOrDefault();

      //if(userCart != null)
      //{
      //  userCart.Cart.Add(product);

      //  await _context.SaveChangesAsync();
      //}

      //return $"{product.Name} added to cart";
      return RedirectToAction("Index");
    }
  }
}
