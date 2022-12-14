using Microsoft.Build.Framework;

namespace ECommerce_WorkingSolo.Models
{
  public class CategoryProduct
  {
    public string Id { get; set; }
    [Required]
    public string ProductId { get; set; }
    [Required]
    public string CategoryId { get; set; }

  }
}
