// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ECommerce_WorkingSolo.Models;

namespace ECommerce_WorkingSolo.Areas.Identity.Pages.Account
{
  public class RegisterModel: PageModel
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IUserEmailStore<ApplicationUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly ECommerceDbContext _context;
    //private readonly RoleManager<ApplicationUser> _roleManager;
    //private readonly IUserRoleStore<ApplicationUser> _userRoleStore;
    //private readonly UserRoleStore _userRoleStore;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        SignInManager<ApplicationUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        ECommerceDbContext context
        //RoleManager<ApplicationUser> roleManager
      //IUserRoleStore<ApplicationUser> userRoleStore
      //UserRoleStore userRoleStore
      )
    {
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
      _context = context;
      //_roleManager = roleManager;
      //_userRoleStore= userRoleStore;
      //_userRoleStore= userRoleStore;
    }


    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public class InputModel 
    {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

      [StringLength(250)]
      [Display(Name = "First Name")]
      public string FirstName { get; set; }
      [StringLength(250)]
      [Display(Name = "Last Name")]
      public string LastName { get; set; }
      [StringLength(250)]
      [Display(Name = "Address 1")]
      public string Address1 { get; set; }
      [Display(Name ="Address 2")]
      public string Address2 { get; set; }

      [StringLength(250)]
      [Display(Name = "Zip Code")]
      public string ZipCode { get; set; }
      public string PhoneNumber { get; set; } 
    }

    public async Task OnGetAsync( string returnUrl = null )
    {
      ReturnUrl = returnUrl;
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync( string returnUrl = null )
    {
      returnUrl ??= Url.Content("~/");
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      //var role = _roleManager.FindByNameAsync("Shopper").Result;
      //var role = _roleManager.FindByIdAsync()
      if (ModelState.IsValid)
      {
        var user = CreateUser();

        user.FirstName = Input.FirstName; user.LastName = Input.LastName; user.Email= Input.Email; user.Address1 = Input.Address1; user.Address2 = Input.Address2; user.PhoneNumber = Input.PhoneNumber; user.ZipCode = Input.ZipCode;
        // create new instance of empty shopping cart and add it to the new user
        var userCart = new ShoppingCart();
        //string newguid = Guid.NewGuid().ToString();
        //userCart.Id = newguid;
        userCart.Id = Guid.NewGuid().ToString();
        userCart.UserID = user.Id;
        user.ShoppingCartId = userCart.Id;

        // imma create an instance of UserBillingInfoModel with all the same info so i can update this if the billing info changes, this way we can always keep the change on file


        var userBillingInfo = new UserBillingInfoModel();

        userBillingInfo.Id = Guid.NewGuid().ToString();
        userBillingInfo.FirstName = Input.FirstName;
        userBillingInfo.LastName = Input.LastName;
        userBillingInfo.Email = Input.Email;
        userBillingInfo.Address1 = Input.Address1;
        userBillingInfo.Address2 = Input.Address2;
        userBillingInfo.PhoneNumber = Input.PhoneNumber;
        userBillingInfo.ZipCode = Input.ZipCode;
        userBillingInfo.UsersOGID = user.Id;

        _context.ShoppingCart.Add(userCart);
        _context.UserBillingInfo.Add(userBillingInfo);

        await _context.SaveChangesAsync();

        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        // adding 'shopper' role to user when they create an account
        
        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
          await _userManager.AddToRoleAsync(user, "Shopper");
          
          _logger.LogInformation("User created a new account with password.");

          var userId = await _userManager.GetUserIdAsync(user);
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          var callbackUrl = Url.Page(
              "/Account/ConfirmEmail",
              pageHandler: null,
              values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
              protocol: Request.Scheme);

          await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
              $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

          if (_userManager.Options.SignIn.RequireConfirmedAccount)
          {
            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
          }
          else
          {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
          }
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    private ApplicationUser CreateUser()
    {
      try
      {
        //return Activator.CreateInstance<ApplicationUser>();
        var user = Activator.CreateInstance<ApplicationUser>();
        return user;
      }
      catch
      {
        throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
            $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
      }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
      if (!_userManager.SupportsUserEmail)
      {
        throw new NotSupportedException("The default UI requires a user store with email support.");
      }
      return (IUserEmailStore<ApplicationUser>)_userStore;
    }
  }
}
