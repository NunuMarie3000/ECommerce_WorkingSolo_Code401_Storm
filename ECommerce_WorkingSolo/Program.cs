using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Identity;
using ECommerce_WorkingSolo.Areas.Admin.Models.Services;
using ECommerce_WorkingSolo.Areas.Admin.Models.Interfaces;
using ECommerce_WorkingSolo.Areas.Admin.Models.Options;
using ECommerce_WorkingSolo.Areas.Identity.Pages.Account;
using ECommerce_WorkingSolo.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ECommerceDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ECommerceDbContextConnection' not found.");

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ECommerceDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//builder.Services.AddAuthorization(options =>
//{
//  options.AddPolicy("readpolicy", builder => builder.RequireRole("Admin", "Editor", "Shopper"));
//  options.AddPolicy("writepolicy", builder => builder.RequireRole("Admin", "Editor"));
//});

// getting rid of making values not explicitly stated [required] from being required
// so ModelState.IsValid is true when we create a new category and don't include a list of categoryproducts
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddTransient<IImageService, ImageService>();

//builder.Services.AddTransient<IUserRoleStore<ApplicationUser>, UserRoleStore>();

builder.Services.Configure<AzureOptions>(builder.Configuration.GetSection("AzureStorageConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
;

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
