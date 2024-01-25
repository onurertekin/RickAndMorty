using Host.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Host.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class AuthorizableAttribute : TypeFilterAttribute
    {

        public AuthorizableAttribute(string actionCode) : base(typeof(AuthorizableFilter))
        {
            Arguments = new object[] { actionCode };
        }
    }

    public class AuthorizableFilter : IAuthorizationFilter
    {
        private readonly string claimCode;

        public AuthorizableFilter(string claimCode = null)
        {
            this.claimCode = claimCode;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Token"].FirstOrDefault(); //swagger'dan header'da gönderdiğimiz token'ı okuyor.
            if (token == null || token == "null")
                throw new BusinessException(401, "TokenRequired"); //token gerekli

            //return;

            #region Validate Token

            //burada token doğru mu diye kontrol edicez.
            bool isValid = ValidateToken(token);
            if (!isValid)
                throw new BusinessException(401, "InvalidToken");

            #endregion

            #region Validate Claims

            var claims = GetTokenClaims(token);
            var userId = Convert.ToInt32(claims.Where(x => x.Type == "UserId").SingleOrDefault().Value);

            //Yetki kontrolü yapıyoruz
            //IAM'i çağırarak soracak
            //var hasClaim = userRolesOperation.CheckUserClaim(userId, claimCode);
            var hasClaim = await CheckUserClaim(userId, claimCode);
            if (!hasClaim)
                throw new BusinessException(401, "Unauthorized");

            #endregion
        }

        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SecretKeyBurayaGelecek");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "OnurErtekin",
                    ValidAudience = "AdminPortalUsers",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private List<Claim> GetTokenClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            return jwtSecurityToken.Claims.ToList();
        }

        private async Task<bool> CheckUserClaim(int userId, string claimCode)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = "{\"userId\":" + userId + ",\"claimCode\":\"" + claimCode + "\"}";

                // JSON içeriği bir StringContent nesnesine ekleyin
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // POST isteği gönderin
                HttpResponseMessage httpResponseMessage = await client.PostAsync("http://localhost:5002/iam/authorization", content);

                // Yanıt hatalı ise
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    dynamic error = JsonConvert.DeserializeObject(responseContent);
                    throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
                }
            }

            return true;
        }
    }
}
