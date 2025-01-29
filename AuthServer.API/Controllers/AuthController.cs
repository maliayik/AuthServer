using AuthServer.Core.DTOs;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuthenticationService authenticationService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await authenticationService.CreateTokenAsync(loginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = authenticationService.CreateTokenByClient(clientLoginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefleshTokenDto refleshTokenDto)
        {
            var result = await authenticationService.RevokeRefleshToken(refleshTokenDto.Token);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefleshTokenDto refleshTokenDto)
        {
            var result = await authenticationService.CreateTokenByRefleshToken(refleshTokenDto.Token);

            return ActionResultInstance(result);
        }
    }
}