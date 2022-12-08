namespace ECommerce_WorkingSolo.Areas.Admin.Models.Interfaces
{
  public interface IImageService
  {
    Task<AzureFile> UploadImageToAzure(IFormFile file);
    void DeleteImageFromAzure(string blobName);
    public bool DoesImageExist( IFormFile file );
  }
}
