using Microsoft.EntityFrameworkCore;
using WDRRP.Dtos;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class UserRepository : IUserService
{
    private readonly WdrrpContext _dbContext;
    private readonly IEmailService _emailService;

    public UserRepository(WdrrpContext dbContext, IEmailService emailService)
    {
        _dbContext = dbContext;
        _emailService = emailService;
    }

    public async Task<UserDto> AddUser(UserDto user)
    {
        var resultCheck = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Email == user.Email);

        if (resultCheck != null)
        {
            throw new InvalidOperationException("Email is alredy used.");
        }


        var newUser = new User{
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ContactNumber = user.ContactNumber,
            Password = user.Password,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            UserTypeId = user.UserTypeId,
            UserStatusId = user.UserStatusId,
            CreatedBy = 0,
            CreatedAt = DateTime.Now,
            UpdatedBy = null,
            UpdatedAt = null,
            IsActive = true
        };

        var result = await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        var response = new UserDto{
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            ContactNumber = newUser.ContactNumber,
            Password = newUser.Password,
            DateOfBirth = newUser.DateOfBirth,
            Gender = newUser.Gender,
            UserTypeId = newUser.UserTypeId,
            UserStatusId = newUser.UserStatusId,
            IsActive = newUser.IsActive
        };

        return response;
    }

    public async Task<string> ChangePassword(PasswordChangeDto changeDto)
    {
        var result = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Email == changeDto.Email && e.IsActive == true);

            if (result == null)
            {
                throw new InvalidOperationException("Invalid credintials.");
            }

            if(result.Password != changeDto.OldPassword)
            {
                throw new InvalidOperationException("Current password is mismatch.");
            }

            result.Password = changeDto.NewPassword;
            result.UpdatedBy = 1; //This will replace the jwt token claim userId
            result.UpdatedAt = DateTime.Now;

            _dbContext.Users.Update(result);

            await _dbContext.SaveChangesAsync();

            return "Password change successful";
    }

    public async Task<bool> DeleteUser(UserDto user)
    {
        var result = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == user.Id);

        if (result == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        result.UpdatedBy = 0; //This will replace the jwt token claim userId
        result.UpdatedAt = DateTime.Now;
        result.IsActive = false;

        _dbContext.Users.Update(result);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var ExistingUser = await (from user in _dbContext.Users
                                         where user.Email == forgotPasswordDto.Email && user.IsActive == true
                                         select user).FirstOrDefaultAsync();

            if (ExistingUser == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

            var subject = "Reset passowrd";
            var body = $"You password reset link is " + forgotPasswordDto.ResetUrl + "?ref=" + ExistingUser.Email;

            _emailService.SendEmailAsync(forgotPasswordDto.Email, subject, body);

            return true;
    }

    public async Task<UserDto> GetUser(int userId)
    {
        var result = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == userId && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var response = new UserDto{
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            ContactNumber = result.ContactNumber,
            Password = result.Password,
            DateOfBirth = result.DateOfBirth,
            Gender = result.Gender,
            UserTypeId = result.UserTypeId,
            UserStatusId = result.UserStatusId,
            IsActive = result.IsActive,
            ProfilePic = result.ProfilePic,
        };

        return response;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var data = await (from user in _dbContext.Users
                where user.IsActive == true
                select new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ContactNumber = user.ContactNumber,
                    Password = user.Password,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    UserTypeId = user.UserTypeId,
                    UserStatusId = user.UserStatusId,
                    IsActive = user.IsActive,
                    ProfilePic = user.ProfilePic,
                }).ToListAsync();

        return data;
    }

    public async Task<UserDto> Login(string email, string password)
    {
        var result = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Email == email && e.Password == password && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("Invalid credintials.");
        }

        var response = new UserDto{
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            ContactNumber = result.ContactNumber,
            Password = result.Password,
            DateOfBirth = result.DateOfBirth,
            Gender = result.Gender,
            UserTypeId = result.UserTypeId,
            UserStatusId = result.UserStatusId,
            IsActive = result.IsActive,
            ProfilePic = result.ProfilePic,
        };

        return response;
    }

    public async Task<bool> ResetPassword(PasswordRestDto passwordRestDto)
    {
        var filteredData = _dbContext.Users.FirstOrDefault(x => x.Email == passwordRestDto.Email && x.IsActive == true);

            if (filteredData == null)
            {
                throw new InvalidOperationException("Employee not found");
            }

            filteredData.Password = passwordRestDto.Password;
            filteredData.UpdatedBy = 1; //This will replace the jwt token claim userId
            filteredData.UpdatedAt = DateTime.Now;

            _dbContext.Users.Update(filteredData);

            await _dbContext.SaveChangesAsync();

            return true;
    }

    public async Task<IEnumerable<UserDto>> SearchUsers(string userName)
    {
        var data = await (from user in _dbContext.Users
                where user.IsActive == true && (user.FirstName == userName || user.LastName == userName)
                select new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ContactNumber = user.ContactNumber,
                    Password = user.Password,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    UserTypeId = user.UserTypeId,
                    UserStatusId = user.UserStatusId,
                    IsActive = user.IsActive,
                    ProfilePic = user.ProfilePic,
                }).ToListAsync();

        return data;    
    }

    public async Task<UserDto> UpdateUser(UserDto user)
    {
        var result = await _dbContext.Users
            .FirstOrDefaultAsync(e => e.Id == user.Id && e.IsActive == true);

        if (result == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        result.FirstName = user.FirstName;
        result.LastName = user.LastName;
        result.Email = user.Email;
        result.ContactNumber = user.ContactNumber;
        result.Password = user.Password;
        result.DateOfBirth = user.DateOfBirth;
        result.Gender = user.Gender;
        result.UserTypeId = user.UserTypeId;
        result.UserStatusId = user.UserStatusId;

        _dbContext.Users.Update(result);

        await _dbContext.SaveChangesAsync();

        var response = new UserDto{
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            ContactNumber = result.ContactNumber,
            Password = result.Password,
            DateOfBirth = result.DateOfBirth,
            Gender = result.Gender,
            UserTypeId = result.UserTypeId,
            UserStatusId = result.UserStatusId,
            IsActive = result.IsActive
        };

        return response;
    }

    public async Task<int> UserCount()
    {
       var data = await (from user in _dbContext.Users
            where user.IsActive == true //&& skill.UserId == userId
            select user).CountAsync();

        return data;
    }
}
