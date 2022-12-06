using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_WorkingSolo.Models
{
  public class Category
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    //[ForeignKey("CategoryId")]
    public List<CategoryProduct> CategoryProducts { get; set; }
    [Required]
    public string Description { get; set; }
    public string ImagePath { get; set; }
    [ForeignKey("CategoryId")]
    public List<Product> ProductsList { get; set; } = new List<Product>();
  }
}
