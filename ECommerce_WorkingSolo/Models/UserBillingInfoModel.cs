using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_WorkingSolo.Models
{
  public class UserBillingInfoModel
  {
    public string Id { get; set; }
    [StringLength(250)]
    [Display(Name = "First Name")]
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
    public string PhoneNumber { get; set; }
    [ForeignKey("AspNetUsersId")]
    public string UsersOGID { get; set; }
  }
}
