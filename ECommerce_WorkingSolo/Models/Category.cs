using ECommerce_WorkingSolo.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_WorkingSolo.Models
{
  public class Category
  {
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string ImagePath { get; set; }
    //public List<Product> ProductsList { get; set; } = new List<Product>();
  }
}
