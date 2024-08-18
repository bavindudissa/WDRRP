using System;

namespace WDRRP.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string fileType);
}
