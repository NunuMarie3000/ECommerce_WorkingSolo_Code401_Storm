using ECommerce_WorkingSolo.Areas.Identity.Data;
using ECommerce_WorkingSolo.Models;
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
      return View("ShoppingCart", new ShoppingCart());
    }
    public IActionResult Buy( string? id)
    {
      //NOTE: COME BACK TO THIS
      return View();
    }

    public async Task<IActionResult> AddToCart( string productId, string userId)
    {
      // NOTE: RETURN TO THIS
      var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
      if(user == null)
      {
        return NotFound();
      }

      var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
      if(product == null)
      {
        return BadRequest();
      }

      var userCart = _context.ShoppingCart.Where(sc => sc.UserID == user.Id).FirstOrDefault();

      if (userCart != null)
      {
        userCart.Cart.Add(product);

        await _context.SaveChangesAsync();
      }
      return RedirectToAction("Index", "Products");
    }

    public async Task<IActionResult> RemoveFromCart(string cartId, string productId)
    {
      // i need to remove the product from the user's cart
      var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
      var userCart = await _context.ShoppingCart.Where(sc=>sc.Id== cartId).FirstOrDefaultAsync();

      if(userCart != null && product != null)
      {
        userCart.Cart.Remove(product);
        await _context.SaveChangesAsync();

      }
      return RedirectToAction("Index", "ShoppingCarts", new {userId=userCart.UserID});
    }
  }
}
