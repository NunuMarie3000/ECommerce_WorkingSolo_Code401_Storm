using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce_WorkingSolo.Areas.Identity.Pages.Account.Manage
{
  public class ShoppingCartModel: PageModel
  {
    private readonly ECommerceDbContext _context;
    public ShoppingCartModel(ECommerceDbContext context)
    {
      _context= context;
    }
    public void OnGet()
    {
    }
  }
}
