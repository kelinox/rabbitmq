using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Microservices.Services.Infrastructure.Providers
{
    public class TokenProvider : ITokenProvider
    {
        public string GetToken(User user)
        {
            var base64 = "b3Oh9TaOEG+pEQguLbEJXBZnIlkXGjXHF2IFivJcYeh7YAQ3qwRAOLFNb6B4HBGVsEhkX3aRWhQbQmJrpTinwmfFE3xZHX8Bj2pamvm42nFbAt6nvJUnY4AdcEJE+rIVGM7YOZgLSVmaseXIxDM5C5+ELg==";

            Console.Out.WriteLine(base64);
            var signinKey = Convert.FromBase64String(base64);
            var expiryDuration = 5;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("username", user.Email)
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