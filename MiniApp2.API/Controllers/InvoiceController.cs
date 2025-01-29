﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        public IActionResult GetInvoices()
        {
            var userName = HttpContext.User.Identity.Name;

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            //TODO veri tabanında userID veya username ile ilgili alanlar içeren tablolarla işlem yapılabilir.

            return Ok($"İnvoince işlemleri => Username:{userName} - UserId={userId}");
        }
    }
}
