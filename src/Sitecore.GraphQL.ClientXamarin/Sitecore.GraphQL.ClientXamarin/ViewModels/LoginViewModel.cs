using Sitecore.GraphQL.Authenticator.Authenticator;
using Sitecore.GraphQL.ClientXamarin.Views;
using Sitecore.GraphQL.Services.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Sitecore.GraphQL.ClientXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Command LoginCommand { get; }

        public Action DisplayInvalidLoginPrompt;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            
        }

        private async void OnLoginClicked(object obj)
        {
            

            string siteDomain = "cb76982436b7.ngrok.io";
            string SSCURL = "https://" + siteDomain + "/sitecore/api/ssc/auth/login";
            string SCC_GRAPHURL = "https://" + siteDomain + "/sitecore/api/graph/items/master";
            string domain = "sitecore";           

            var authCookie = Login.GetAuthroizedCookie(SSCURL, domain, userName, password);

            if (!string.IsNullOrEmpty(authCookie))
            {
                Login.SetAUthToken(authCookie);               
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }

            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
