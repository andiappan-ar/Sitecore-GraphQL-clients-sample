using Newtonsoft.Json;
using Sitecore.GraphQL.Authenticator.Authenticator;
using Sitecore.GraphQL.Services.Service;
using System;

namespace Sitecore.GraphQL.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string SSCURL = "https://arsc.dev.local/sitecore/api/ssc/auth/login";
            string SCC_GRAPHURL = "https://arsc.dev.local/sitecore/api/graph/items/master";
            string domain = "sitecore";
            string siteDomain = "arsc.dev.local";

            Console.WriteLine("***************** Login sitecore ********************"+Environment.NewLine);
            Console.WriteLine("Enter user name:"+Environment.NewLine);
            var userName = Console.ReadLine();
            Console.WriteLine("Enter password:" + Environment.NewLine);
            var password = Console.ReadLine();

            var authCookie = Login.GetAuthroizedCookie(SSCURL, domain, userName, password);

            var graphQLQuery = @"{
                                      item {
                                        name
                                        children {
                                          name                                            
                                        }
                                      }
                                    }";

            var graphQLData = ReadService.FetchGraphQLData<dynamic>(authCookie, SCC_GRAPHURL, siteDomain, graphQLQuery);

            Console.WriteLine(JsonConvert.SerializeObject(graphQLData.Data));
        }
    }
}
