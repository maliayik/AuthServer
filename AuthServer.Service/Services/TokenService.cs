using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibary.Configurations;
using SharedLibary.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        /// <summary>
        /// Refresh token oluşturur
        /// </summary>
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        /// <summary>
        /// Üyelik sistemi gerektiren claimleri oluşturan methot
        /// </summary>
        private IEnumerable<Claim> GetClaims(UserApp userApp, List<String> audiences)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email, userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }

        /// <summary>
        /// Üyelik sistemi gerektirmeyen claimleri oluşturan methot
        /// </summary>
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;
        }

        /// <summary>
        /// AuthServer'dan gelen kullanıcı bilgilerine göre token oluşturur
        /// </summary>
        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refleshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefleshTokenExpiration);

            var securityKey = SignService.GetSymetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefleshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefleshTokenExpiration = refleshTokenExpiration
            };

            return tokenDto;
        }

        /// <summary>
        /// Üyelik sistemi gerektirmeyen client bilgilerine göre token oluşturur
        /// </summary>
        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var securityKey = SignService.GetSymetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration
            };

            return tokenDto;
        }
    }
}