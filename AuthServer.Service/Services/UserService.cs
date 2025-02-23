﻿using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using SharedLibary.DTOs;

namespace AuthServer.Service.Services
{
    /// <summary>
    ///  Kullanıcı kayıt işlemlerini yapacağımız servis
    /// </summary>
    public class UserService(UserManager<UserApp> _userManager, RoleManager<IdentityRole> roleManager) : IUserService
    {
        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            //identity kütüphanesi password hashlemeyi otomatik yapar

            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<NoContentDto>> CreateUserRoles(string userName)
        {
            if (!await roleManager.RoleExistsAsync("admin") || !await roleManager.RoleExistsAsync("manager"))
            {
                await roleManager.CreateAsync(new() { Name = "admin" });
                await roleManager.CreateAsync(new() { Name = "manager" });
            }

            var user = await _userManager.FindByNameAsync(userName);

            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "manager");

            return Response<NoContentDto>.Success(204);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 4004, true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}