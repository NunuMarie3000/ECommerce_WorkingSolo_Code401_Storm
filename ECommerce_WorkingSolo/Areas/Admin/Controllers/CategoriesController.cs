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
using ECommerce_WorkingSolo.Areas.Admin.Models;
using ECommerce_WorkingSolo.Areas.Admin.Models.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ECommerce_WorkingSolo.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class CategoriesController: Controller
  {
    private readonly ECommerceDbContext _context;

    private readonly IImageService _imageService;

    public CategoriesController( ECommerceDbContext context, IImageService imageservice )
    {
      _context = context;
      _imageService = imageservice;
    }

    // GET: Admin/Categories
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Index()
    {
      return View(await _context.Categories.ToListAsync());
    }

    // get
    [HttpGet]
    [Route("Admin/Categories/EditPicture/{categoryId}/{imagePath}")]
    public IActionResult EditPicture()
    {
      return View("EditImagePartial");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Admin/Categories/EditPicture/{categoryId}/{imagePath}")]
    public async Task<IActionResult> EditPicture( [Bind("FileDetails, File")]ImageFileModel imageModel, int categoryId, string imagePath )
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
      var cat = _context.Categories.Where(c=> c.Id == categoryId).FirstOrDefault();
      cat.ImagePath = azureFile.Result.Url;

      // not entirely sure what this is doing, so i'll keep it for now 
      ViewBag.ImageUri = azureFile.Result.Url;

      // save the database
      await _context.SaveChangesAsync();

      return RedirectToAction("Edit", new {id = categoryId});

    }

    // GET: Admin/Categories/Details/5
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Details( int? id )
    {
      if (id == null || _context.Categories == null)
      {
        return NotFound();
      }

      var category = await _context.Categories
          .FirstOrDefaultAsync(m => m.Id == id);
      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    // GET: Admin/Categories/Create
    [Authorize(Roles = "Admin, Editor")]
    public IActionResult Create()
    {
      return View();
    }

    // POST: Admin/Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Create( [Bind("Id,Name,Description,CategoryName,ImagePath")] Category category )
    {
      if (ModelState.IsValid)
      {
        _context.Add(category);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SavePicture( ImageFileModel imageModel )
    {
      ViewBag.ImageModel = imageModel;

      var azureFile = _imageService.UploadImageToAzure(imageModel.File);

      ViewBag.ImageUri = azureFile.Result.Url;

      return View("Create");
    }

    // GET: Admin/Categories/Edit/5
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Edit( int? id )
    {
      if (id == null || _context.Categories == null)
      {
        return NotFound();
      }

      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }
      return View(category);
    }

    // POST: Admin/Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> Edit( int id, [Bind("Id,Name,Description,ImagePath")] Category category )
    {
      if (id != category.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {

          //EditPicture(imageModel);

          _context.Update(category);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CategoryExists(category.Id))
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
      return View(category);
    }


    // GET: Admin/Categories/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete( int? id )
    {
      if (id == null || _context.Categories == null)
      {
        return NotFound();
      }

      var category = await _context.Categories
          .FirstOrDefaultAsync(m => m.Id == id);
      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    // POST: Admin/Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed( int id )
    {
      if (_context.Categories == null)
      {
        return Problem("Entity set 'ECommerceDbContext.Categories'  is null.");
      }
      var category = await _context.Categories.FindAsync(id);
 
      if (category != null)
      {
        _context.Categories.Remove(category);

        //delete all associated products if you get rid of a category
        var productQuery = await (from item in _context.Products
                                  where item.CategoryId == id
                                  select item).ToListAsync();
        foreach (var item in productQuery)
        {
          _context.Products.Remove(item);
        }

        // get blobname out of the imagepath
        string blobUrl = category.ImagePath;
        int pos = blobUrl.LastIndexOf("/") + 1;
        string blobName = blobUrl.Substring(pos, blobUrl.Length - pos);

        // delete blob from blobstorage
        _imageService.DeleteImageFromAzure(blobName);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists( int id )
    {
      return _context.Categories.Any(e => e.Id == id);
    }
  }
}
