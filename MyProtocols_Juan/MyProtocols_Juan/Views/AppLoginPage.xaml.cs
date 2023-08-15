using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyProtocols_Juan.ViewModels;
using Acr.UserDialogs;

namespace MyProtocols_Juan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
    {

        UserViewModel viewModel;

        public AppLoginPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new UserViewModel();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (TxtUserName.Text != null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Checking User Access...");
                    await Task.Delay(800);

                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();

                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R)
                    {
                        await Navigation.PushAsync(new StartPage());
                         
                        return;
                    }
                    else
                    {
                        await DisplayAlert("User Access Denied", "Username or Password are incorrect", "OK");
                        return;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            else
            {
                await DisplayAlert("User Access Denied", "Please, insert data", "OK");
                return;
            }


        }

        private async void BtnSingUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSingUpPage());
        }
    }
}