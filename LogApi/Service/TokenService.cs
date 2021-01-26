using LogApi.IService;
using LogApi.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogApi.Service
{
    public class TokenService : ITokenService
    {
        private ApiConfigurationModel apiConfigurationModel { get; set; }
        private readonly ILogger<TokenService> logger;
        public TokenService(IOptions<ApiConfigurationModel> options, ILogger<TokenService> _logger)
        {
            apiConfigurationModel = options.Value;
            logger = _logger;
        }
        public string GenerateToken()
        {
            string accessToken = string.Empty;
            try
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(apiConfigurationModel.SystemKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                accessToken = tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to generate token : {ex.Message}");
            }

            return accessToken;
        }
    }
}
