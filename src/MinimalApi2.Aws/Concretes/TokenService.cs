using Microsoft.IdentityModel.Tokens;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Entities.Identity;
using MinimalApi2.Aws.Helpers;
using MinimalApi2.Aws.Models;
using MinimalApi2.Aws.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace MinimalApi2.Aws.Concretes
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel GenerateToken(User user)
        {
            JwtOptions jwtOptions = _configuration.GetOptions<JwtOptions>("JwtOptions");

            var siginingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Email),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("Email",user.Email),
                new Claim(ClaimTypes.Role,Constants.Role.User),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var _expries = DateTime.Now.AddMinutes(int.Parse(jwtOptions.ExpiryMinutes));

            var securityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: _expries,
                claims: claims,
                signingCredentials: siginingCredentials
            );

            TokenModel token = new();
            token.Expiration = _expries;
            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public TokenModel GenerateToken(User user, string role)
        {
            JwtOptions jwtOptions = _configuration.GetOptions<JwtOptions>("JwtOptions");

            var siginingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Email),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("Email",user.Email),
                new Claim(ClaimTypes.Role,Constants.Role.User),
                new Claim("Role",role),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var _expries = DateTime.Now.AddMinutes(int.Parse(jwtOptions.ExpiryMinutes));

            var securityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: _expries,
                claims: claims,
                signingCredentials: siginingCredentials
            );

            TokenModel token = new();
            token.Expiration = _expries;
            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
