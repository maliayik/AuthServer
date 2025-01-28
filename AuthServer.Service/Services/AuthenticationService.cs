using AuthServer.Core.DTOs;
using AuthServer.Core.Services;
using SharedLibary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.Configuration;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefleshToken> _userRefleshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClients, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefleshToken> userRefleshTokenService)
        {
            _clients = optionsClients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefleshTokenService = userRefleshTokenService;
        }

        /// <summary>
        /// Kullanıcı giriş işlemi sonrası token üretir
        /// </summary>
        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if(loginDto==null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);
            }

            var token = _tokenService.CreateToken(user);

            //dbde reflesh token var mı kontrol ediyoruz
            var userRefleshToken = await _userRefleshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefleshToken == null)
            {
                await _userRefleshTokenService.AddAsync(new UserRefleshToken
                    { UserId = user.Id, Code = token.RefleshToken, Expiration = token.RefleshTokenExpiration });
            }
            else
            {
                userRefleshToken.Code = token.RefleshToken;
                userRefleshToken.Expiration = token.RefleshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);

        }

        public Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TokenDto>> CreateTokenByRefleshToken(string refleshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContentDto>> RevokeRefleshToken(string refleshToken)
        {
            throw new NotImplementedException();
        }
    }
}
