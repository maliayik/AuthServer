using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.DTOs;
using SharedLibary.DTOs;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Kullanıcı adı ve şifre ile token oluşturma işlemi
        /// </summary>
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        /// <summary>
        /// Reflesh token ile yeni token oluşturma işlemi
        /// </summary>
        Task<Response<TokenDto>> CreateTokenByRefleshToken(string refleshToken);

        /// <summary>
        /// Reflesh tokeni veritabanından silme işlemi
        /// </summary>
        Task<Response<NoContentDto>> RevokeRefleshToken(string refleshToken);

        /// <summary>
        /// Tanımlı bir client için token oluşturma işlemi
        /// </summary>
        Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
