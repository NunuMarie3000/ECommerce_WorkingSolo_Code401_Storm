using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_WorkingSolo.Models
{
  public class ShoppingCart
  {
    public string Id { get; set; }
    public string UserID { get; set; }
    public List<Product> Cart { get; set; } = new List<Product>();
  }
}
