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
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

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

        /// <summary>
        /// Tanımlı bir client için token oluşturma işlemi
        /// </summary>
        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x =>
                x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }

        /// <summary>
        /// Reflesh token ile yeni token oluşturma işlemi
        /// </summary>
        public async Task<Response<TokenDto>> CreateTokenByRefleshToken(string refleshToken)
        {
            var existRefleshToken = await _userRefleshTokenService.Where(x => x.Code == refleshToken).SingleOrDefaultAsync();

            if (existRefleshToken == null)
            {
                return Response<TokenDto>.Fail("Reflesh token not found", 404, true);
            }

            var user= await _userManager.FindByIdAsync(existRefleshToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id not found", 404, true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefleshToken.Code = tokenDto.RefleshToken;
            existRefleshToken.Expiration = tokenDto.RefleshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto,200);
        }

        /// <summary>
        /// Var olan reflesh tokeni silme işlemi
        /// </summary>
        public async Task<Response<NoContentDto>> RevokeRefleshToken(string refleshToken)
        {
            var existRefleshToken =
                await _userRefleshTokenService.Where(x => x.Code == refleshToken).SingleOrDefaultAsync();

            if (existRefleshToken == null)
            {
                return Response<NoContentDto>.Fail("Reflesh token not found", 404, true);
            }

            _userRefleshTokenService.Remove(existRefleshToken);

            await _unitOfWork.CommitAsync();

            return Response<NoContentDto>.Success(200);
        }
    }
}
