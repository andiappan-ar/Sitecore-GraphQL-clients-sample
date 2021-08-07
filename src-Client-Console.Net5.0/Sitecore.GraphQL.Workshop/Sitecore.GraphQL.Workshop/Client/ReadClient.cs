using GraphQL.Client.Http;
using System;
using Newtonsoft;
using Newtonsoft.Json;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Sitecore.GraphQL.WorkshopClient.Client
{
    public static class ReadClient
    {
        public static async Task<dynamic> ReadSampleItems(string authCookie)
        {
            dynamic result = null;

            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(new Cookie(".AspNet.Cookies", authCookie,"/", "arsc.dev.local"));

                var handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true
                };

                Uri scURI = new Uri(ClientConstants.SCC_GRAPHURL);
                using (var client = new GraphQLHttpClient(
                new GraphQLHttpClientOptions { EndPoint = scURI, HttpMessageHandler = handler  }, new NewtonsoftJsonSerializer()
                ))
                {

                    var query = @"{
                                      item {
                                        name
                                        children {
                                          name,
                                            children{name}
                                        }
                                      }
                                    }";

                    var request = new GraphQLRequest(query);



                    result = await client.SendQueryAsync<dynamic>(request);


                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static string Read(string authCookie)
        {
            var result = Task.Run(async () => await ReadSampleItems(authCookie)).Result;            
            return JsonConvert.SerializeObject(result.Data);
        }
    }
}

