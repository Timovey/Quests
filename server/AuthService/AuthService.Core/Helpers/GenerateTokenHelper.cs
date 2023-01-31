using AuthService.Core.HelperModels;
using AuthService.Database.Models;
using AuthService.DataContracts.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthService.Core.Helpers
{
    public class GenerateTokenHelper
    {
        private readonly JwtSetting _jwtSettings;

        private readonly JwtRefreshSetting _jwtRefreshSetting;

        public GenerateTokenHelper(IOptions<JwtSetting> jwtSetting, IOptions<JwtRefreshSetting> jwtRefreshSetting)
        {
            _jwtRefreshSetting = jwtRefreshSetting.Value;
            _jwtSettings = jwtSetting.Value;
        }

        public string GenerateJwtToken(UserViewModel user, string hashPassword)
        {
            var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiresMinutes),
            claims: GetIdentity(user, hashPassword),
            signingCredentials: new SigningCredentials(_jwtSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
           
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public string GenerateJwtRefreshToken(UserViewModel user, string hashPassword)
        {
            var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtRefreshSetting.TokenExpiresMinutes),
            claims: GetIdentity(user, hashPassword),
            signingCredentials: new SigningCredentials(_jwtRefreshSetting.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private IEnumerable<Claim> GetIdentity(UserViewModel user, string hashPassword)
        {

            //идентификация на основе логина, захешированного пароля
            //также на всякий добавляем пароль
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Hash, hashPassword),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            return claims;
        }

        public string GenerateJwtToken(ApplicationUser user, string hashPassword, IList<string> userRoles)
        {
            var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiresMinutes),
            claims: GetIdentity(user, hashPassword, userRoles),
            signingCredentials: new SigningCredentials(_jwtSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public (string,int) GenerateJwtRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return (Convert.ToBase64String(randomNumber), _jwtRefreshSetting.TokenExpiresMinutes);
        }

        private IEnumerable<Claim> GetIdentity(ApplicationUser user, string hashPassword, IList<string> userRoles)
        {

            //идентификация на основе логина, захешированного пароля
            //также на всякий добавляем пароль
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Hash, hashPassword),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }

    }
}
