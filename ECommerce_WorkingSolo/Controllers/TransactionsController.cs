using ECommerce_WorkingSolo.Areas.Admin.Models.Services;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using ECommerce_WorkingSolo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_WorkingSolo.Controllers
{
  [Authorize]
  public class TransactionsController: Controller
  {
    private readonly ECommerceDbContext _context;
    private readonly IEmailSender _emailSender;
    public TransactionsController( ECommerceDbContext context, IEmailSender emailSender )
    {
      _context = context;
      _emailSender = emailSender;
    }

    public IActionResult Index()
    {
      return View("ShoppingCart", new ShoppingCart());
    }

    public async Task<IActionResult> AddToCart( string productId, string userId )
    {
      // NOTE: RETURN TO THIS
      var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
      if (user == null)
      {
        return NotFound();
      }

      var product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
      if (product == null)
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

    public async Task<IActionResult> RemoveFromCart( string cartId, string productId )
    {
      // i need to remove the product from the user's cart
      var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
      var userCart = await _context.ShoppingCart.Where(sc => sc.Id == cartId).FirstOrDefaultAsync();

      if (userCart != null && product != null)
      {
        userCart.Cart.Remove(product);
        await _context.SaveChangesAsync();

      }
      return RedirectToAction("Index", "ShoppingCarts", new { userId = userCart.UserID });
    }

    public IActionResult CheckoutPage( string cartId )
    {
      var user = _context.Users.Where(u => u.ShoppingCartId == cartId).FirstOrDefault();
      var userBilling = _context.UserBillingInfo.Where(ub => ub.UsersOGID ==  user.Id).FirstOrDefault();
      var userCart = _context.ShoppingCart.Where(sc => sc.Id == cartId).Include(sc => sc.Cart).FirstOrDefault();

      //ViewBag.User = user;
      ViewBag.User = userBilling;
      ViewBag.UserCart = userCart;
      if(ViewBag.UserBilling !=null)
      {
        return View("CheckoutPage", ViewBag.UserBilling);
      }
      return View("CheckoutPage", userBilling);
    }

    public async Task<IActionResult> Checkout( string userscartId )
    {
      // this is where i need to have a button that confirms total and everything in the cart, then process cc and send email to user/warehouse
      var usersCart = _context.ShoppingCart.Where(sc => sc.Id == userscartId).FirstOrDefault();
      var user = _context.Users.Where(u => u.ShoppingCartId == userscartId).FirstOrDefault();

      if (usersCart == null || user == null)
      {
        return null;
      }

      decimal total = 0;
      foreach (var item in usersCart.Cart)
      {
        total += item.Price;
      }

      await _emailSender.SendEmailAsync("vmarie1997@gmail.com", "Test from Transactions controller", total.ToString());

      // some point here, i need to empty the users' shopping carts...
      // maybe foreach(var item in usercart.cart)
      //{
      // 
      //}
      // nvm, userscart.cart.Clear(); will remove all items in the cart


      return null;
    }

    public IActionResult UpdateBilling( [Bind("Id,UsersOGID,FirstName,LastName,Address1,Address2,ZipCode,Email,PhoneNumber")] UserBillingInfoModel userBilling )
    {
      //NOT: COME BACK TO THIS

      var user = _context.Users.Where(u => u.Id == userBilling.UsersOGID).FirstOrDefault();
      if (user == null)
      {
        return null;
        // FIND SOMETHING BETTER TO PUT HERE
      }
      // I WANNA SET something in the viewbag so i can conditionally render the form vs just a block of the info, maybe send back the userBilling object to display in a table-esque
      ViewBag.UserBilling = userBilling;
      //return RedirectToAction("CheckoutPage", "Transactions", user.ShoppingCartId
      return RedirectToAction("CheckoutPage", "Transactions", new { cartId = user.ShoppingCartId });

    }

    public IActionResult SubmitCCInfo( string userId )
    {
      var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
      return View(user);
    }


  }
}
