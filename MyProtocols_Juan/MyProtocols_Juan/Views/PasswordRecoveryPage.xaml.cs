using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProtocols_Juan.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocols_Juan.Models;

namespace MyProtocols_Juan.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordRecoveryPage : ContentPage
    {
        UserViewModel viewModel;
        public PasswordRecoveryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();

            LoadUserAsync();
        }

        private async void LoadUserAsync()
        {
            PkrUser.ItemsSource = await viewModel.GetUserAsync();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtCurrentPassword.IsPassword = false;
                TxtConfirmCurrentPassword.IsPassword = false;
            }
            else
            {
                TxtCurrentPassword.IsPassword = true;
                TxtConfirmCurrentPassword.IsPassword = true;
            }
        }

        private void SwShowPassword_Toggled_1(object sender, ToggledEventArgs e)
        {
           
        }

        private void SwShowPassword_Toggled_2(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword2.IsToggled)
            {
                TxtNewPassword.IsPassword = false;
            }
            else
            {
                TxtNewPassword.IsPassword = true;
            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppLoginPage());
        }

        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {
            if (TxtCurrentPassword.Text != null && !string.IsNullOrEmpty(TxtCurrentPassword.Text.Trim()) &&
                TxtConfirmCurrentPassword.Text != null && !string.IsNullOrEmpty(TxtConfirmCurrentPassword.Text.Trim()) &&
                TxtNewPassword.Text != null && !string.IsNullOrEmpty(TxtNewPassword.Text.Trim()) &&
                TxtCurrentPassword.Text == TxtConfirmCurrentPassword.Text)
            {
                //si hay info en los cuadros de texto de email y pass se procede 
                try
                {
                    //hacemos una animación de espera 
                    UserDialogs.Instance.ShowLoading("Checking User Access...");
                    await Task.Delay(2000);

                    //Capturar el rol que se haya seleccionado en el picker
                    User selectedUser = PkrUser.SelectedItem as User;

                    string password = TxtNewPassword.Text.Trim();


                    bool R = await viewModel.ModifyPasswordAsync(selectedUser.Email, password);

                    if (R)
                    {
                        //si la validación es correcta se permite el ingreso al sistema 
                        //igual que el progra 5 vamos a tener un usuario global 

                        GlobalObjects.MyLocalUser = await viewModel.GetUserDataAsync(TxtCurrentPassword.Text.Trim());

                        await Navigation.PushAsync(new AppLoginPage());
                        return;
                    }
                    else
                    {
                        //algo salió mal 

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
                    //apagamos la animación de carga 
                    UserDialogs.Instance.HideLoading();
                }


            }
            else
            {
                //si no digito datos indicarle al usuario del requerimiento 

                await DisplayAlert("Data required", "Error", "OK");
                return;
            }
        }
    }
}