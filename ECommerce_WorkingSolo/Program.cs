using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Identity;
using ECommerce_WorkingSolo.Areas.Admin.Models.Services;
using ECommerce_WorkingSolo.Areas.Admin.Models.Interfaces;
using ECommerce_WorkingSolo.Areas.Admin.Models.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ECommerceDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ECommerceDbContextConnection' not found.");

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
  // this is so we can use roles in our project
  .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ECommerceDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//builder.Services.AddAuthorization(options =>
//{
//  options.AddPolicy("RequireAdminRole",
//       policy => policy.RequireRole("Admin"));
//});

// getting rid of making values not explicitly stated [required] from being required
// so ModelState.IsValid is true when we create a new category and don't include a list of categoryproducts
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// connecting to blob storage with connection string
//builder.Services.AddScoped(_ =>
//{
//  return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
//});

//builder.Services.AddScoped(b =>
//{
//  return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
//});

//// connecting to our blob storage
//var blobServiceClient = new BlobServiceClient(
//  new Uri("https://stormfirststorageaccount.blob.core.windows.net"),
//  new DefaultAzureCredential());

////create name for the container
//string retroGamingBlobContainer = "retrogamingblobs" + Guid.NewGuid().ToString();

//// create the container and return a container client object
//BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(retroGamingBlobContainer);

builder.Services.AddTransient<IImageService, ImageService>();
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
