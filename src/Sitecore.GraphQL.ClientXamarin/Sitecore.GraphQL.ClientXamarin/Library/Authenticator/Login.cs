using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sitecore.GraphQL.Authenticator.Authenticator
{
    public static class Login
    {
        public static string AuthToken = "";

        //ngrok http https://arsc.dev.local:443 -host-header="arsc.dev.local:443"
        public static string siteDomain = "d07636c9e720.ngrok.io";
        public static string SSCURL = "https://" + siteDomain + "/sitecore/api/ssc/auth/login";
        public static string SCC_GRAPHURL = "https://" + siteDomain + "/sitecore/api/graph/items/master";
        public static string domain = "sitecore";

        public static void SetAUthToken(string token)
        {
            AuthToken = token;
        }

        /// <summary>
        /// GetAuthroizedCookie
        /// </summary>
        /// <param name="SSCURL"></param>
        /// <param name="domain"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetAuthroizedCookie(string SSCURL , string domain , string userName , string password)
        {
            string result = string.Empty;

            try
            {
                CookieContainer cookies = new CookieContainer();

                HttpClientHandler handler = new HttpClientHandler();

                handler.CookieContainer = cookies;

                var client = new HttpClient(handler);               

                var webRequest = new HttpRequestMessage(HttpMethod.Post, SSCURL)
                {
                    Content = new StringContent("{ \n    \"domain\": \""+ domain + "\", \n    \"username\": \""+userName+"\", \n    \"password\": \""+ password + "\" \n}", Encoding.UTF8, "application/json")
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = client.SendAsync(webRequest).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;
                    }
                }

                Uri uri = new Uri(SSCURL);
                IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

                result=  (responseCookies.Count() > 0) ? responseCookies.Where(x => x.Name.Equals(".AspNet.Cookies")).FirstOrDefault().Value : "";
            }
            catch(Exception ex)
            {
                throw ;
            }

            return result;
        }
    }
}
