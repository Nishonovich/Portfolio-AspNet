namespace Portfolio.WebApi.Interfaces.Services
{
    public interface IFileService
    {
        public string ImageFolderName { get; }
        Task<string> SaveImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string relativeFilePath);
        Task<string> SaveLogoAsync(IFormFile image);
        Task<bool> DeleteLogoAsync(string relativeFilePath);
    }
}
