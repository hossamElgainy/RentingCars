using Core.Interfaces.IServices.SystemIServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services.SystemServices
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly string _webRootPath;

        public FileService(ILogger<FileService> logger, string webRootPath)
        {
            _logger = logger;
            _webRootPath = webRootPath ?? throw new ArgumentNullException(nameof(webRootPath));

            // Ensure Images directory exists
            var imagesPath = Path.Combine(_webRootPath, "Images");
            Directory.CreateDirectory(imagesPath);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            // Validate input
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded");

            // Sanitize folder name
            var safeFolderName = Path.GetFileName(folderName);
            if (string.IsNullOrWhiteSpace(safeFolderName))
                throw new ArgumentException("Invalid folder name");

            // Create target directory
            var uploadsPath = Path.Combine(_webRootPath, "Images", safeFolderName);
            Directory.CreateDirectory(uploadsPath);

            // Generate unique filename
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(uploadsPath, uniqueFileName);

            // Save file
            try
            {
                await using var fileStream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(fileStream);

      
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to {Path}", fullPath);
                throw new ApplicationException("File upload failed", ex);
            }
        }

        public async Task DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentException("File path is required");

            var fullPath = Path.Combine(_webRootPath, relativePath.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(fullPath))
            {
                _logger.LogWarning("File not found: {Path}", fullPath);
                return;
            }

            try
            {
                await Task.Run(() => File.Delete(fullPath));
                _logger.LogInformation("Deleted file: {Path}", fullPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file: {Path}", fullPath);
                throw;
            }
        }

        public string GetWebRootPath() => _webRootPath;
    }
}