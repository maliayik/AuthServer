using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    //rol bazlı authorization
  
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [Authorize(Policy = "AgePolicy")]
        [Authorize(Roles = "admin",Policy = "AnkaraPolicy")]
        [HttpGet]
        public IActionResult GetStock()
        {
            var userName = HttpContext.User.Identity.Name;

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            //TODO  tabanında userID veya username ile ilgili alanlar içeren tablolarla işlem yapılabilir.

            return Ok($"Stock işlemleri => Username:{userName} - UserId={userIdClaim.Value}");
        }
    }
}