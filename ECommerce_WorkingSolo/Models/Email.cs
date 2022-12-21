using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_WorkingSolo.Models
{
  [NotMapped]
  public class Email
  {
    public string toEmail { get; set; }
    public string subject { get; set;  }
    public string message {  get; set; }
  }
}
