using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Shared;

namespace Auth
{
    public class JWTAuth
    {
        private readonly string _key;
        private readonly int _tokenlifespam = 15;
        public JWTAuth(string key)
        {
            _key = key;
        }

        public string generatetoken(TokenRequest tokenrequest)
        {   
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);


            var claim = new List<Claim>
            {
                //new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, tokenrequest.email),
                new(JwtRegisteredClaimNames.Email, tokenrequest.email),
                new("UserId", tokenrequest.UserId.ToString())
            };

            var tokenDescip = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_tokenlifespam)),
                Issuer = "ascasc",
                Audience = "csacsa",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescip);
            var writtenJWT = tokenHandler.WriteToken(token);

            return writtenJWT;
        }

        public string GenerateRefreshToken()
        {
            //should update this with a new version of Random for secuity 
            Random random = new Random();
            byte[] baseBytes = new byte[128];
            random.NextBytes(baseBytes);
            return Convert.ToBase64String(baseBytes);
        }
    }
}
