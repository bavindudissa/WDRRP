using System;
using Microsoft.EntityFrameworkCore;
using WDRRP.Dtos;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class ProfilePicRepository : IProfilePicService
{
    private readonly WdrrpContext _dbContext;
    private readonly IFileService _fileService;

    public ProfilePicRepository(WdrrpContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    public async Task<ProfilePicDto> AddProfilePicture(ProfilePicDto profilePicDto)
    {
        string? dbPath = "";
        //var hasImage = agent.image;
        if (profilePicDto.file != null || profilePicDto.file.Length != 0)
        {
            dbPath = await _fileService.SaveFileAsync(profilePicDto.file, profilePicDto.file.ContentType);
        }

        var result = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == profilePicDto.id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        result.ProfilePic = dbPath;

        _dbContext.Users.Update(result);

        await _dbContext.SaveChangesAsync();

        var response =  new ProfilePicDto{
            id = profilePicDto.id,
            link = dbPath,
        };

        return response;
    }

    public async Task<ProfilePicDto> GetProfilePicture(int userId)
    {
        var result = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == userId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("User not found.");
        }

         var response =  new ProfilePicDto{
            id = result.Id,
            link = result.ProfilePic,
        };

        return response;
    }
}