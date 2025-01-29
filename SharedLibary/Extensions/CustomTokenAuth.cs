using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedLibary.Configurations;
using SharedLibary.Services;

namespace SharedLibary.Extensions
{
    public static class CustomTokenAuth
    {
        public static void AddCustomTokenAuth(this IServiceCollection services, CustomTokenOption customTokenOption)
        {
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
             {
                 opts.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidIssuer = customTokenOption.Issuer,
                     ValidAudience = customTokenOption.Audience[0],
                     IssuerSigningKey = SignService.GetSymetricSecurityKey(customTokenOption.SecurityKey),
                     ValidateIssuerSigningKey = true,
                     ValidateAudience = true,
                     ValidateIssuer = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
             });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .Build();
            });
        }
    }
}