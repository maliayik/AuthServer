﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SharedLibary.Services
{
    public static class SignService
    {
        public static SecurityKey GetSymetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}