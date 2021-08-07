using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.GraphQL.WorkShopAuthenticator.Authenticator
{
    public static class Login
    {
        private static string SSCURL = "https://arsc.dev.local/sitecore/api/ssc/auth/login";        

        public static string GetAuthroizedCookie()
        {
            string result = string.Empty;

            try
            {
                CookieContainer cookies = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler();
                handler.CookieContainer = cookies;

                var client = new HttpClient(handler);

                var collection = new NameValueCollection();
                collection["domain"] = "sitecore";
                collection["username"] = "admin";
                collection["password"] = "b";

                string values = string.Join(",", collection.AllKeys.Select(key => collection[key]));

                var webRequest = new HttpRequestMessage(HttpMethod.Post, SSCURL)
                {
                    Content = new StringContent("{ \n    \"domain\": \"sitecore\", \n    \"username\": \"admin\", \n    \"password\": \"b\" \n}", Encoding.UTF8, "application/json")
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                var response = client.Send(webRequest);

                var reader = new StreamReader(response.Content.ReadAsStream());

                string responseStr = reader.ReadToEnd();

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
