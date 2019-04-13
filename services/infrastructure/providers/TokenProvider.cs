using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microservices.Services.Infrastructure.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;

        public TokenProvider(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generate a JwtToken for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetToken(User user)
        {
            var signinKey = Convert.FromBase64String(_config["Jwt:SigninSecret"]);
            var expiryDuration = int.Parse(_config["Jwt:ExpiryDuration"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signinKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(jwtToken);
        }
    }
}