using Host.Helpers.Exceptions;
using Microsoft.AspNetCore.Http;
using System;

namespace Host.Helpers
{
    public static class HttpRequestParser
    {
        public static void UpdateRequest<T>(T request, HttpContext httpContext)
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var requestData = System.Text.Encoding.UTF8.GetBytes(json);

                httpContext.Request.Body = new System.IO.MemoryStream(requestData);
                httpContext.Request.ContentLength = httpContext.Request.Body.Length;
            }
            catch (Exception ex)
            {
                throw new BusinessException(400, "HttpRequestBody.BadRequest");
            }
        }
    }
}
