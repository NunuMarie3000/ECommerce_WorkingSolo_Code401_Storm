using Microsoft.Build.Framework;

namespace ECommerce_WorkingSolo.Models
{
  public class CategoryProduct
  {
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int CategoryId { get; set; }

  }
}
