using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWebSite.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Post请求API
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="jsonValue"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(this HttpClient client, string url, Dictionary<string, string> headers = null)
        {
            //初始化头部
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            //初始化内容
            var responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                return $"访问API地址:{url}出错,错误代码:{responseMessage.StatusCode},错误原因:{responseMessage.ReasonPhrase}";
            }
        }


        /// <summary>
        /// Post请求API
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="jsonValue"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(this HttpClient client, string url, string jsonValue, Dictionary<string, string> headers = null)
        {
            //初始化头部
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            //初始化内容
            var content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(url, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                return $"访问API地址:{url}出错,参数:{jsonValue},错误代码:{responseMessage.StatusCode},错误原因:{responseMessage.ReasonPhrase}";
            }
        }
    }
}
