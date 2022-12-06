using Microsoft.Build.Framework;

namespace ECommerce_WorkingSolo.Models
{
  public class Product
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    //public string CategoryName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } 
    public Condition Condition { get; set; }
    public Rating Rating { get; set; }
    public string ImagePath { get; set; }
    
  }
  public enum Condition
  {
    New, UsedLikeNew, UsedGood, UsedFair, Poor
  }
  public enum Rating
  {
    Low = 1, LowMedium = 2, Medium = 3, MediumHigh = 4, High = 5
  }
}
