using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sitecore.GraphQL.Services.Service
{
    public static class ReadService
    {
        public static async Task<GraphQLResponse<T>> GetGraphQLData<T>(string authCookie,string SCC_GRAPHURL,string siteDomain,string graphQLQuery)
        {
            GraphQLResponse<T> result;

            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(new Cookie(".AspNet.Cookies", authCookie,"/", siteDomain));

                var handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true
                };

                Uri scURI = new Uri(SCC_GRAPHURL);
                using (var client = new GraphQLHttpClient(
                new GraphQLHttpClientOptions { EndPoint = scURI, HttpMessageHandler = handler  }, new NewtonsoftJsonSerializer()
                ))
                {                  

                    var request = new GraphQLRequest(graphQLQuery);
                    result = await client.SendQueryAsync<T>(request);


                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public static GraphQLResponse<T> FetchGraphQLData<T>(string authCookie, string SCC_GRAPHURL, string siteDomain, string graphQLQuery)
        {
            var result = Task.Run(async () => await GetGraphQLData<T>(authCookie, SCC_GRAPHURL, siteDomain, graphQLQuery)).Result;
            return result;
        }

    }
}

