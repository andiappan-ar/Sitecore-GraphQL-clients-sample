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
            Console.WriteLine("Enter User Name:");
            var userName = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();

            var authCookie = Login.GetAuthroizedCookie(SSCURL, domain, userName, password);

            if (!string.IsNullOrEmpty(authCookie))
            {
                Console.WriteLine("***************** Login successfull ********************" + Environment.NewLine);
                var graphQLQuery = @"{
                                      item {
                                        name
                                        children {
                                          name                                            
                                        }
                                      }
                                    }";

                Console.WriteLine("GraphQL query:" + Environment.NewLine + graphQLQuery + Environment.NewLine);
                var graphQLData = ReadService.FetchGraphQLData<dynamic>(authCookie, SCC_GRAPHURL, siteDomain, graphQLQuery);
                Console.WriteLine("GraphQL Data:" + Environment.NewLine + JsonConvert.SerializeObject(graphQLData.Data));               
            }
            else
            {
                Console.WriteLine("***************** Login failed ********************" + Environment.NewLine);
            }
            Console.ReadLine();

        }
    }
}
