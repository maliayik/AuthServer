using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibary.DTOs;

namespace SharedLibary.Exceptions
{
    /// <summary>
    /// Error middleware for handling custom exceptions
    /// </summary>
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //sonlandırıcı middleware
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var exception = error.Error;

                        ErrorDto errorDto = null;

                        if (exception is CustomException)
                        {
                            errorDto = new ErrorDto(exception.Message, true);

                        }
                        else
                        {
                            //uygulamanın kendi fırlattığı hataları false olarak set edip clientlara göstermiyoruz.
                            errorDto = new ErrorDto(exception.Message, false);
                        }

                        var response = Response<NoContentDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }

                });
            });

        }
    }
}
