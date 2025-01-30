using AuthServer.Core.DTOs;
using SharedLibary.DTOs;

namespace AuthServer.Core.Services
{
    /// <summary>
    /// Indentity kütüphanesi kullanacağımız için sadece servis oluşturuyoruz, bu servis ile kullanıcı kayıt işlemlerini yapacağız.
    /// </summary>
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);

        Task<Response<NoContentDto>> CreateUserRoles(string userName);
    }
}