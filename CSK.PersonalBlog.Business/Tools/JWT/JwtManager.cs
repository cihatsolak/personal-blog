using CSK.PersonalBlog.Business.Tools.Settings;
using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CSK.PersonalBlog.Business.Tools.JWT
{
    public class JwtManager : IJwtService
    {
        private readonly IOptions<JwtSettings> jwtOptions;
        public JwtManager(IOptions<JwtSettings> _jwtOptions)
        {
            jwtOptions = _jwtOptions;
        }

        public JwtToken GenerateJwt(AppUser appUser)
        {
            var securityKey = Encoding.UTF8.GetBytes(jwtOptions.Value.SecurityKey);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: SetClaims(appUser),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(jwtOptions.Value.Expires),
                signingCredentials: signingCredentials
            );

            var responseToken = new JwtToken();

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            responseToken.Token = handler.WriteToken(jwtSecurityToken);

            return responseToken;
        }

        private List<Claim> SetClaims(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, $"{appUser.Id}")
            };

            return claims;
        }
    }
}
