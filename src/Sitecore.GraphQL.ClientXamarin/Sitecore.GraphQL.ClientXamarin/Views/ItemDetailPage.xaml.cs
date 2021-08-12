using Sitecore.GraphQL.ClientXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Sitecore.GraphQL.ClientXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}