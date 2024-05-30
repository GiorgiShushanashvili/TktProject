using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Security;

public class TokenManagement:ITokenManagement
{
    private readonly IConfiguration _configuration;

    public TokenManagement(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> CreateToken(int userId, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,userId.ToString())
        };
        if (role == "Admin")
        {
            claims.Add(new Claim(ClaimTypes.Role,role));
        }

        /*var token = new JwtSecurityToken(
            claims: claims,
            expires: null,
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);*/

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Expires = null,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}