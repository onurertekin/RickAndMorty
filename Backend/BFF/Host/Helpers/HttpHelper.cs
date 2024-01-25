using Contract.Request.IAM.User;
using Contract.Request.Users;
using Contract.Response.IAM.User;
using Host.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Host.Helpers
{
    public class HttpHelper
    {
        public async Task Delete(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var httpResponseMessage = await client.DeleteAsync(url);

                // Yanıt hatalı ise
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    dynamic error = JsonConvert.DeserializeObject(responseContent);
                    throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
                }
            }
        }

        public async Task Update(string url, object request)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(request); //Request(DTO) nesnesini Json formatına dönüştürdük.

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"); //JSON içeriği bir StringContent nesnesine ekliyoruz. HTTP isteği gönderilirken bu içerik gönderilebilir

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content); // PUT isteği gönderiyoruz. 

                //Yanıt Hatalı ise
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    //HTTP isteğinin yanıtının içeriğini okumak ve bu içeriği bir metin dizesi olarak elde etmek için kullanılır.
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    //HTTP yanıtının JSON içeriğini alır, bu içeriği C# içinde kullanılabilir bir formata dönüştürür ve dinamik bir değişkende saklar.
                    dynamic error = JsonConvert.DeserializeObject(responseContent);
                    throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
                }
            }
        }

        public async Task<T> Get<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                else
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    dynamic error = JsonConvert.DeserializeObject(responseContent);
                    throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
                }
            }
        }
        public async Task Create(string url, object request)
        {
            using (HttpClient client = new HttpClient())
            {
                // DTO nesnesini JSON formatına dönüştürün
                string jsonContent = JsonConvert.SerializeObject(request);

                // JSON içeriği bir StringContent nesnesine ekleyin
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // POST isteği gönderin
                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                // Yanıtı işleyin ve gereken işlemleri yapın
                // Yanıt hatalı ise
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    dynamic error = JsonConvert.DeserializeObject(responseContent);
                    throw new BusinessException((int)httpResponseMessage.StatusCode, error.ErrorMessage.ToString());
                }
            }
        }
    }
}
