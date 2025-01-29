using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public IActionResult GetStock()
        {
            var userName = HttpContext.User.Identity.Name;

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            //TODO  tabanında userID veya username ile ilgili alanlar içeren tablolarla işlem yapılabilir.

            return Ok($"Stock işlemleri => Username:{userName} - UserId={userId}");
        }
    }
}
