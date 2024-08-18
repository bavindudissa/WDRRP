using System;
using WDRRP.Dtos;

namespace WDRRP.Services;

public interface IProfilePicService
{
        Task<ProfilePicDto> GetProfilePicture(int userId);
        Task<ProfilePicDto> AddProfilePicture(ProfilePicDto profilePicDto);
}
