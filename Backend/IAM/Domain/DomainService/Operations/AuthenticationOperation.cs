using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Contract.Response.Claims.SearchClaimsResponse;

namespace DomainService.Operations
{
    public class AuthenticationOperation
    {
        private readonly MainDbContext mainDbContext;

        public AuthenticationOperation(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public string Authentication(string username, string password)
        {
            #region Validate User

            var user = mainDbContext.Users.Where(x => x.UserName == username && x.Password == password).SingleOrDefault();

            if (user == null)
                throw new BusinessException(401, "Kullanıcı Bulunamadı");

            #endregion

            #region Generate Token

            var claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim("UserId", user.Id.ToString()));

            string token = GenerateToken(claims);

            #endregion

            return token;

        }

        private string GenerateToken(List<System.Security.Claims.Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyBurayaGelecek"));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.Now.AddHours(1);

            var jwtSecurityToken = new JwtSecurityToken("OnurErtekin", "AdminPortalUsers", claims, expires: expiry, signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
