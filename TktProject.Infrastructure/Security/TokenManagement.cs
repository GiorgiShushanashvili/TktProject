using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.IdentityModel.Tokens;
using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Security;

public class TokenManagement:ITokenManagement
{
    public async Task<string> CreateToken(int userId,string role)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject =new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Email,"Roma@mygps.ge")
            }),
            Expires = null,
            CompressionAlgorithm = SecurityAlgorithms.Sha256,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(),SecurityAlgorithms.Sha256)
        };
        
        SecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityToken secToken = handler.CreateToken(tokenDescriptor);//new SecurityToken(tokenDescriptor);
        var token=handler.WriteToken(secToken);
        HttpScheme
    }
}