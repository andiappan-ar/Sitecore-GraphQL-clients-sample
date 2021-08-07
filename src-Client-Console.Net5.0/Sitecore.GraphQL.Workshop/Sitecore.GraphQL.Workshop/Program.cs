using Sitecore.GraphQL.WorkShopAuthenticator.Authenticator;
using Sitecore.GraphQL.WorkshopClient.Client;
using System;

namespace Sitecore.GraphQL.Workshop
{
    class Program
    {
        static void Main(string[] args)
        {
            var authCookie = Login.GetAuthroizedCookie();
           Console.WriteLine(ReadClient.Read(authCookie));
            Console.ReadLine();
        }      

    }
}
