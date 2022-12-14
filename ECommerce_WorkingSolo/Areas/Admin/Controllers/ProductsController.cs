using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using ECommerce_WorkingSolo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ECommerce_WorkingSolo.Areas.Admin.Models.Interfaces;
using ECommerce_WorkingSolo.Areas.Admin.Models;

namespace ECommerce_WorkingSolo.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ProductsController: Controller
  {
    private readonly ECommerceDbContext _context;
    private readonly IImageService _imageService;

    public ProductsController( ECommerceDbContext context, IImageService imageService )
    {
      _context = context;
      _imageService = imageService;
    }

    // GET: Admin/Products
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Index( string? categoryId )
    {
      if (categoryId != null)
      {
        ViewBag.CategoryId = categoryId;

        List<Product> list = await (from itm in _context.Products.Include(p => p.Category)
                                    where itm.CategoryId == categoryId
                                    select new Product
                                    {
                                      CategoryId = (string)categoryId,
                                      Name = itm.Name,
                                      Id = itm.Id,
                                      Condition = itm.Condition,
                                      Category = itm.Category,
                                      Description = itm.Description,
                                      ImagePath = itm.ImagePath,
                                      Price = itm.Price,
                                      Rating = itm.Rating
                                    }).ToListAsync();

        ViewBag.CategoryId = categoryId;
        return View(list);
      }

      return View(await _context.Products.Include(p => p.Category).ToListAsync());
    }

    // GET: Admin/Products/Details/5
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Details( string? id )
    {
      if (id == null || _context.Products == null)
      {
        return NotFound();
      }

      var product = await _context.Products
          .FirstOrDefaultAsync(m => m.Id == id);

      var catid = product.CategoryId;

      var category = await _context.Categories.Where(cat => cat.Id== catid).FirstOrDefaultAsync();
      product.Category = category;

      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }

    // GET: Admin/Products/Create
    [Authorize(Roles = "Admin, Editor")]
    [Route("Admin/Products/Create/{categoryId}")]
    public IActionResult Create( string? categoryId)
    {
      //var a = Request.Path.Value;
      //var categoryid = a.Split(new char[] { '/' }, 5);
      //if(categoryid.Length == 5)
      //  ViewBag.CategoryID = categoryid[ 4 ];
      if (categoryId != null)
        ViewBag.CategoryID = categoryId;
      return View();
    }

    // POST: Admin/Products/Create
    [HttpPost]
    [Route("Admin/Products/Create")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Create( [Bind("Id,Name,Price,Description,Condition,Rating,ImagePath,CategoryId")] Product product )
    {
      // when new product is made, i need to create a new CategoryProduct object so i have reference between category and product
      var cat = await _context.Categories.Where(cat => cat.Id == product.CategoryId).FirstOrDefaultAsync();

      if (cat == null)
        return View("Index");

      if (ModelState.IsValid)
      {
        product.Category = cat;
        // also need to add this product to the category's product list
        cat.ProductsList.Add(product);

        _context.Add(product);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("Index", new { categoryId = cat.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Admin/Products/SavePicture")]
    public IActionResult SavePicture( ImageFileModel imageModel )
    {
      string catId = imageModel.FileDetails;
      ViewBag.ImageModel = imageModel;

      var azureFile = _imageService.UploadImageToAzure(imageModel.File);

      // keeps saying i need to pass it a product object, so imma do something sketchy
      ViewBag.AzureUrl = new Product { Name= azureFile.Result.Url, CategoryId=catId };

      return View("Create", new Product {CategoryId=catId});
    }

    // GET: Admin/Products/Edit/5
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Edit( string? id )
    {
      if (id == null || _context.Products == null)
      {
        return NotFound();
      }

      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return View(product);
    }

    // POST: Admin/Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Edit( string id, [Bind("Id,Name,Price,Description,Condition,Rating,ImagePath,CategoryId")] Product product )
    {
      if (id != product.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(product);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ProductExists(product.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(product);
    }

    // get
    [HttpGet]
    [Route("Admin/Products/EditPicture/{categoryId}/{imagePath}")]
    public IActionResult EditPicture()
    {
      return View("EditImagePartial");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Admin/Products/EditPicture/{productId}/{imagePath}")]
    public async Task<IActionResult> EditPicture( [Bind("FileDetails, File")] ImageFileModel imageModel, string productId, string imagePath )
    {
      // get blobname out of the imagepath
      string blobUrl = imagePath;
      int pos = blobUrl.LastIndexOf("%") + 1;
      string blobName = blobUrl.Substring(pos, blobUrl.Length - pos);
      string name = blobName.Substring(2);

      // first, delete current url from blobstorage
      _imageService.DeleteImageFromAzure(name);

      // next, upload new image to azure
      var azureFile = _imageService.UploadImageToAzure(imageModel.File);

      // now update the category's imagepath property in the database
      //var cat = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
      //cat.ImagePath = azureFile.Result.Url;

      // update the product's imagepath property in the database
      var pro = _context.Products.Where(p => p.Id== productId).FirstOrDefault();
      pro.ImagePath = azureFile.Result.Url;

      // not entirely sure what this is doing, so i'll keep it for now 
      ViewBag.ImageUri = azureFile.Result.Url;

      // save the database
      await _context.SaveChangesAsync();

      return RedirectToAction("Edit", new { id = productId });

    }

    // GET: Admin/Products/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete( string? id )
    {
      if (id == null || _context.Products == null)
      {
        return NotFound();
      }

      var product = await _context.Products
          .FirstOrDefaultAsync(m => m.Id == id);
      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }

    // POST: Admin/Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed( string id )
    {
      if (_context.Products == null)
      {
        return Problem("Entity set 'ECommerceDbContext.Products'  is null.");
      }
      var product = await _context.Products.FindAsync(id);
      if (product != null)
      {
        _context.Products.Remove(product);

        // get blobname out of the imagepath
        string blobUrl = product.ImagePath;
        int pos = blobUrl.LastIndexOf("/") + 1;
        string blobName = blobUrl.Substring(pos, blobUrl.Length - pos);

        // delete blob from blobstorage
        _imageService.DeleteImageFromAzure(blobName);

        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(Index));
    }

    private bool ProductExists( string id )
    {
      return _context.Products.Any(e => e.Id == id);
    }
  }
}
