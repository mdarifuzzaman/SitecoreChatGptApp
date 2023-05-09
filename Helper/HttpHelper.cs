using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChatGptApp.Helper
{
    public class HttpHelper
    {
        public HttpResponseMessage ExecuteRequest(RequestType requestType, string url, string body, string contentType, IDictionary<string, string> headers)
        {
            using (var httpClient = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

                    }
                }

                var content = new StringContent(body, Encoding.UTF8, contentType);

                ServicePointManager.Expect100Continue = true;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Task<HttpResponseMessage> httpResponse = null;

                if (requestType == RequestType.Patch)
                {
                    httpResponse = httpClient.PutAsync(url, content);
                }
                else if (requestType == RequestType.Post)
                {
                    httpResponse = httpClient.PostAsync(url, content);
                }

                return httpResponse != null ? httpResponse.Result : null;

            }
        }
    }

    public enum RequestType
    {
        Post,
        Patch
    }
}