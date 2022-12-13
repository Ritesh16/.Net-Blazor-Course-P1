using Microsoft.AspNetCore.Components.Forms;
using TangyWeb_Server.Services.Interfaces;

namespace TangyWeb_Server.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string filePath)
        {
            if (File.Exists(_webHostEnvironment.WebRootPath + filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath + filePath);
                return true;
            }

            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = $"{Guid.NewGuid().ToString()}{fileInfo.Extension}";
            var directoryPath = $"{_webHostEnvironment.WebRootPath}\\images\\products";
            if (Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fileStream);

            var fullPath = $"\\images\\products\\{fileName}";
            return fullPath;
        }
    }
}
