using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LmsApi.Utils;

public class JwtUtil
{
    public static string KeyStr() => "12345678901234567890123456789012";

    public static string GenerateToken(string id)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Iss, id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyStr()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expired = DateTime.Now.AddMonths(2);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expired,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
