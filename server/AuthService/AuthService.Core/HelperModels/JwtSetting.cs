﻿using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.HelperModels
{
    public class JwtSetting
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int TokenExpiresMinutes { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }

    }
}
