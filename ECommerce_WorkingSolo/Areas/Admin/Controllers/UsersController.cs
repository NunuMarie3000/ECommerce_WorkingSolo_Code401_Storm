using ECommerce_WorkingSolo.Areas.Identity.Data;
using ECommerce_WorkingSolo.Areas.Admin.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ECommerce_WorkingSolo.Models;

namespace ECommerce_WorkingSolo.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class UsersController: Controller
  {
    private readonly ECommerceDbContext _context;
    private readonly IEmailSender _emailSender;
    public UsersController(ECommerceDbContext context, IEmailSender emailSender)
    {
      _context = context;
      _emailSender = emailSender;
    }

    // GET: UsersController
    public ActionResult Index()
    {
      var users = _context.Users;
      ViewBag.AllUsers = users;
      return View();
    }

    // Post
    public async Task<IActionResult> CreateEmail( [Bind("toEmail,subject,message")] Email email)
    {
      //_emailSender.SendEmailAsync("email@email.com", "Test Email", "")
      await _emailSender.SendEmailAsync(email.toEmail, email.subject, email.message);

      return View();
    }

    //// GET: UsersController/Details/5
    //public ActionResult Details( int id )
    //{
    //  return View();
    //}

    //// GET: UsersController/Create
    //public ActionResult Create()
    //{
    //  return View();
    //}

    //// POST: UsersController/Create
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create( IFormCollection collection )
    //{
    //  try
    //  {
    //    return RedirectToAction(nameof(Index));
    //  }
    //  catch
    //  {
    //    return View();
    //  }
    //}

    //// GET: UsersController/Edit/5
    //public ActionResult Edit( int id )
    //{
    //  return View();
    //}

    //// POST: UsersController/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit( int id, IFormCollection collection )
    //{
    //  try
    //  {
    //    return RedirectToAction(nameof(Index));
    //  }
    //  catch
    //  {
    //    return View();
    //  }
    //}

    //// GET: UsersController/Delete/5
    //public ActionResult Delete( int id )
    //{
    //  return View();
    //}

    //// POST: UsersController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Delete( int id, IFormCollection collection )
    //{
    //  try
    //  {
    //    return RedirectToAction(nameof(Index));
    //  }
    //  catch
    //  {
    //    return View();
    //  }
    //}

  }
}
