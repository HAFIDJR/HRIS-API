using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HRIS.models;
using Microsoft.IdentityModel.Tokens;

namespace HRIS.JWT
{

    public class GenerateJwt
    {
        private readonly IConfiguration _config;

        public GenerateJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = {
                new Claim (ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:TokenKey"]!));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: _config["Jwt:Issuer"],
             audience: _config["Jwt:Audience"],
             claims: claims,
             expires: DateTime.UtcNow.AddHours(3),
             signingCredentials: creds
         );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}