using Microsoft.AspNetCore.Mvc;
using SharedLibary.DTOs;

namespace AuthServer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        /// <summary>
        /// Response tipinde dönüş yapacak tüm controllerlar için ortak metot
        /// </summary>
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}