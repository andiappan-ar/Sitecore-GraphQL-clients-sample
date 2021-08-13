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

            var authCookie = Login.GetAuthroizedCookie(Login.SSCURL, Login.domain, userName, password);

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
