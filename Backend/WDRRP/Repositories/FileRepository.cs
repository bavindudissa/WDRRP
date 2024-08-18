using System;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class FileRepository : IFileService
{
    private readonly string _filePath;

    public FileRepository()
    {
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");

        EnsureDirectoriesExist();
    }

    private void EnsureDirectoriesExist()
    {
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
    }

    public async Task<string> SaveFileAsync(IFormFile file, string fileType)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("The file is null or empty.");
        }

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        Guid guid = Guid.NewGuid();
        var fileName = $"{guid}";
        var fileExtension = GetFileExtension(fileType);
        var fullPath = Path.Combine(_filePath, fileName + fileExtension);

        await File.WriteAllBytesAsync(fullPath, fileBytes);

        return Path.Combine(fileName + fileExtension);
    }

    private string GetFileExtension(string fileType)
    {
        return fileType switch
        {
            "image/png" => ".png",
            "image/jpeg" => ".jpg",
            "image/gif" => ".gif",
            "image/bmp" => ".bmp",
            "image/tiff" => ".tiff",
            "image/webp" => ".webp",
            "application/pdf" => ".pdf",
            "application/msword" => ".doc",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
            "application/vnd.ms-excel" => ".xls",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ".xlsx",
            "application/vnd.ms-powerpoint" => ".ppt",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation" => ".pptx",
            "text/plain" => ".txt",
            "text/html" => ".html",
            "application/json" => ".json",
            "application/xml" => ".xml",
            "application/zip" => ".zip",
            "application/x-rar-compressed" => ".rar",
            "video/mp4" => ".mp4",
            "video/x-matroska" => ".mkv",
            "video/x-msvideo" => ".avi",
            "video/x-ms-wmv" => ".wmv",
            "video/mpeg" => ".mpeg",
            "video/ogg" => ".ogv",
            "video/webm" => ".webm",
            _ => ".dat"
        };
    }


}
