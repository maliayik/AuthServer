using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibary.DTOs;

namespace SharedLibary.Extensions
{
    public static class CustomValidationResponse
    {
        /// <summary>
        /// Modellerimizin validation işlemlerinde hata oluştuğunda dönecek response'un formatını değiştirmek için kullanılır.
        /// </summary>
        /// <param name="services"></param>
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    //model state içerisindeki sadece hata mesajlarını alır.
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);

                    var response = Response<NoContentDto>.Fail(errorDto, 400);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}