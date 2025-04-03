using Microsoft.IdentityModel.Tokens;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Shipping.Api.Infrastructure.Repositories;

public class JWTProvider:IJWTProvider
{
    public (string token, int expiresIn) GenerateJwtToken(ApplicationUser user,IEnumerable<string> roles,IEnumerable<string> permissions)
    {
        Claim[] claims = [
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Name,user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.CreateVersion7().ToString()),
                new Claim(nameof(roles), JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray),
                new Claim(nameof(permissions), JsonSerializer.Serialize(permissions), JsonClaimValueTypes.JsonArray)
            ];
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lEQCxTrFYTOsyFtbtoWwPdDJ3066bWiP"));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);
        var expiresIn = 60;
        var expiration = DateTime.Now.AddMinutes(expiresIn);
        var token = new JwtSecurityToken(
            issuer: "ShippingProject",
            audience: "ShippingProject users",
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(token), expiresIn * 60);
    }
}
