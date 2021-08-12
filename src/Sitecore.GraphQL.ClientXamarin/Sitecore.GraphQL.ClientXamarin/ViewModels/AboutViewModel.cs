using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using Sitecore.GraphQL.Authenticator.Authenticator;
using Sitecore.GraphQL.Services.Service;
using Sitecore.GraphQL.ClientXamarin.GraphQLModel;

namespace Sitecore.GraphQL.ClientXamarin.ViewModels
{
public class AboutViewModel : BaseViewModel
{
        public AboutViewModel()
        {
            Title = "About";

            if (!string.IsNullOrEmpty(Login.AuthToken))
            {
                string siteDomain = "cb76982436b7.ngrok.io";
                string SSCURL = "https://" + siteDomain + "/sitecore/api/ssc/auth/login";
                string SCC_GRAPHURL = "https://" + siteDomain + "/sitecore/api/graph/items/master";
                string domain = "sitecore";

                var graphQLQuery = @"{
                                  item(path: '/sitecore/content/Home') {
                                    name,    
                                     fields{
                                        id
                                        name
                                        value
                                    }
                                  }
                                }
                                ";

                var graphQLData = ReadService.FetchGraphQLData<AboutGraphQLModel>(Login.AuthToken, SCC_GRAPHURL, siteDomain, graphQLQuery);
            }
            

            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}