﻿using WDRRP.Dtos;

namespace WDRRP.Services;

public interface IUserService
{
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(int userId);
        Task<UserDto> AddUser(UserDto user);
        Task<UserDto> UpdateUser(UserDto user);
        Task<bool> DeleteUser(UserDto user);
        Task<IEnumerable<UserDto>> SearchUsers(string userName);
        Task<UserDto> Login(string email, string password);
        Task<int> UserCount();

        Task<string> ChangePassword(PasswordChangeDto changeDto);
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPassword(PasswordRestDto passwordRestDto);

}
