using Microsoft.IdentityModel.Tokens;
using ProAirApiServices.DataLayer.Models.Dto.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ProAirApiServices.Core
{
    public class TokenServices
    {
        #region Constants

        private const int EXPIRATION_HOURS = 1;
        #endregion

        #region Fields

        private readonly IConfiguration config;
        
        #endregion

        public TokenServices(IConfiguration config)
        {
            this.config = config;
        }

        public string CreateToken(MemberDto model) 
        {
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes
            (config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, model.Email),                    
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,"Basic")
                }),
                Expires = DateTime.UtcNow.AddHours(EXPIRATION_HOURS),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);            

            return jwtToken;
        }
    }
}
