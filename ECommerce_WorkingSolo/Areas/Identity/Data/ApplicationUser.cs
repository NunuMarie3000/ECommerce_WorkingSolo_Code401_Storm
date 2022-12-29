using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ECommerce_WorkingSolo.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_WorkingSolo.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
  [StringLength(250)]
  [Display(Name ="First Name")]
  public string FirstName { get; set; }
  [StringLength(250)]
  [Display(Name = "Last Name")]
  public string LastName { get; set; }
  [StringLength(250)]
  public string Email { get; set; }
  [StringLength(250)]
  [Display(Name = "Address 1")]
  public string Address1 { get; set; }
  [StringLength(250)]
  [Display(Name = "Address 2")]
  public string Address2 { get; set; }
  [StringLength(250)]
  [Display(Name = "Zip Code")]
  public string ZipCode { get; set; }
  public string ShoppingCartId { get; set; }
}

