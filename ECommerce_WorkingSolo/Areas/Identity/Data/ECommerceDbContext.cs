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
    // Customize the ASP.NET Identity model and override the defaults if needed.
    // For example, you can rename the ASP.NET Identity table names and more.
    // Add your customizations after calling base.OnModelCreating(builder);
    int rdm = new Random().Next(9, 101);
    builder.Entity<Product>().HasData(new Product { Id=rdm, CategoryId=8, Rating=Rating.MediumHigh, Price=176.82m, ImagePath="/images/gaming_consoles/gameboyadvancesp_gaming_console.jpg", Condition=Condition.UsedLikeNew, Description= "This is a mint condition Gameboy Advance SP Gaming console. Sold with no cartridges", Name="Gameboy Advance Sp" });
  }

  public DbSet<Category> Categories { get; set; }
  public DbSet<CategoryProduct> CategoryProducts { get; set; }
  public DbSet<Product> Products { get; set; }
}
