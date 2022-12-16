using ECommerce_WorkingSolo.Areas.Identity.Data;
using ECommerce_WorkingSolo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_WorkingSolo.Areas.Identity.Data;

public class ECommerceDbContext: IdentityDbContext<ApplicationUser>
{
  public ECommerceDbContext( DbContextOptions<ECommerceDbContext> options )
      : base(options)
  {
  }

  protected override void OnModelCreating( ModelBuilder builder )
  {
    base.OnModelCreating(builder);

    // categories
    var figurines = new Category { Id = "a92c6ebf-67a2-430b-bb70-934d7b533367", Name = "Figurines", Description = "Browse our various antique figurines and action figures!", ImagePath = "./wwwroot/images/retro_action_figures.jpg" };

    var gamingConsoles = new Category { Id = "28838b5c-1f9b-40b3-90a5-266779ee2c3f", Name = "Gaming Consoles", Description = "Category full of different retro gaming consoles: from the gameboy color to the original xbox!", ImagePath = "./wwwroot/images/retro_gaming_consoles.jpg" };

    // products
    var storm = new Product { Id = "53dbaaf5-3b8d-42e1-af12-e10e779e27a9", Name = "Storm X-Men Figurine", Description = "Mint condition Storm figurine from the X-Men comic series", CategoryId = figurines.Id, Condition = Condition.New, Rating = Rating.High, ImagePath = "./wwwroot/images/storm_retro_figurine.jpg", Price = 300.00m };

    var gameboyColor = new Product { Id = "8aa4d1a2-bd54-4f5b-8811-b754e337790f", Name = "Gameboy Color", Description = "Gently used Gameboy Color!", CategoryId = gamingConsoles.Id, Condition = Condition.UsedGood, Price = 250.00m, Rating = Rating.MediumHigh, ImagePath = "./wwwroot/gameboycolor_gaming_console.jpg" };

    var gameboyAdvanceSp = new Product { Id = "438ee5b7-0dde-4500-8868-a122dcd2a07a", Name = "Gameboy Advance Sp", CategoryId = gamingConsoles.Id, Condition = Condition.UsedGood, Rating = Rating.Medium, ImagePath = "./wwwroot/images/gameboyadvancesp_gaming_console.jpg", Price = 150.00m, Description = "Used Gameboy Advance Sp" };


    //some dummy data since azure trial is over
    //builder.Entity<Category>().HasData(figurines);
    //builder.Entity<Category>().HasData(gamingConsoles);
    //builder.Entity<Product>().HasData(storm);
    //builder.Entity<Product>().HasData(gameboyAdvanceSp);
    //builder.Entity<Product>().HasData(gameboyColor);

  }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ShoppingCart> ShoppingCart { get; set; }
}
