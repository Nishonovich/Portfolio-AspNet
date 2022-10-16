using Microsoft.AspNetCore.Http;
using Portfolio.WebApi.Helpers;
using Portfolio.WebApi.Interfaces.Services;

namespace Portfolio.WebApi.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly string _imageFolderName = "Images";
        private readonly string _logoFolderName = "Logos";

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
        string IFileService.ImageFolderName => _imageFolderName;

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

        public async Task<string> SaveLogoAsync(IFormFile image)
        {
            var fileName = ImageHelper.MakeLogoAsync(image.FileName);
            var partPath = Path.Combine(_logoFolderName, fileName);
            var path = Path.Combine(_basePath, partPath);

            var stream = File.Create(path);
            await image.CopyToAsync(stream);
            stream.Close();
            return partPath;
        }

        public Task<bool> DeleteLogoAsync(string relativeFilePath)
        {
            string absaluteFilePath = Path.Combine(_basePath, relativeFilePath);

            if (!File.Exists(absaluteFilePath)) return Task.FromResult(false);

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
    }
}
