using System.ComponentModel;

namespace ECommerce_WorkingSolo.Areas.Admin.Models
{
  public class ImageFileModel
  {
    [DisplayName("Upload Image")]
    public string FileDetails { get; set; }
    public IFormFile File { get; set; }
  }
}
