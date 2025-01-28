using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Service.Services
{
    // Bu klassın amacı, token oluştururken kullanılacak olan imzayı için simetrik security key'i oluşturmak.
    internal static class SignService
    {
        public static SecurityKey GetSymetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
