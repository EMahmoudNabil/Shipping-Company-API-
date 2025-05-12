using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Core.Domin.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.AuthServices;
public class JWTProvider:IJWTProvider
{
    // JWT Provider
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
