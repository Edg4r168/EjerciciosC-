using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthenticationEDSL12092024.Auth;

public class JWTAuthenticationService : IJWTAuthenticationService
{
    private readonly string _key;

    public JWTAuthenticationService(string key)
    {
        _key = key;
    }

    public string Authenticate(string userName)
    {
        byte[] keyBytes = Encoding.ASCII.GetBytes(_key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, userName)
            }),

            Expires = DateTime.UtcNow.AddHours(8),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}
