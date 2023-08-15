using MyProtocols_Juan.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyProtocols_Juan.Views
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