using Portfolio.WebApi.Helpers;
using Portfolio.WebApi.Interfaces.Services;

namespace Portfolio.WebApi.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly string _imageFolderName = "Images";

        public FileService(IWebHostEnvironment environment)
        {
            _basePath = environment.WebRootPath;
        }

        public Task<bool> DeleteImageAsync(string relativeFilePath)
        {
            string absaluteFilePath = Path.Combine(_basePath, relativeFilePath);

            if(!File.Exists(absaluteFilePath)) return Task.FromResult(false);

            try
            {
                File.Delete(absaluteFilePath);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string fileName = ImageHelper.MakeImageName(image.FileName);
            string partPath = Path.Combine(_imageFolderName, fileName);
            string path = Path.Combine(_basePath, partPath);

            var stream = File.Create(path);
            await image.CopyToAsync(stream);
            stream.Close();

            return partPath;

        }
    }
}
