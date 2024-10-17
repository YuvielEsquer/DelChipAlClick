using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiTallerDelChipAlClick.Models;

namespace ApiTallerDelChipAlClick.Helpers
{
    public class Utilities
    {
        private readonly IConfiguration _configuration;
        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncryptSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create()) 
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string triggerJwT(UsersModel model) 
        {
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserID.ToString()),
                new Claim(ClaimTypes.Name, model.UserName.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwT:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var issuer = _configuration["JwT:Issuer"];
            var audience = _configuration["JwT:Audience"];

            var JwTConfig = new JwtSecurityToken(
               issuer: issuer,
               audience: audience,
               claims: userClaims,
               expires: DateTime.UtcNow.AddMinutes(20),
               signingCredentials: credentials
               );
            return new JwtSecurityTokenHandler().WriteToken( JwTConfig );
        }
    }
}
