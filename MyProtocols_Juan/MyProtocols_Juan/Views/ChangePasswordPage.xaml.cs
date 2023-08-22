using Acr.UserDialogs;
using MyProtocols_Juan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyProtocols_Juan.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Xml.Linq;

namespace MyProtocols_Juan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {

        UserViewModel viewModel;
        public ChangePasswordPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadUserName();
        }

        private void LoadUserName()
        {
            LblUserName.Text = GlobalObjects.MyLocalUser.Nombre.ToUpper();
        }


        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtCurrentPassword.IsPassword = false;
            }
            else
            {
                TxtCurrentPassword.IsPassword = true;
            }
        }

        private void SwShowPassword_Toggled_2(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword2.IsToggled)
            {
                TxtNewPassword.IsPassword = false;
                TxtConfirm.IsPassword = false;
            }
            else
            {
                TxtNewPassword.IsPassword = true;
                TxtConfirm.IsPassword = true;
            }
        }
        private bool ValidateFields()
        {
            bool R = false;
            if (TxtCurrentPassword.Text != null && !string.IsNullOrEmpty(TxtCurrentPassword.Text.Trim()) &&
                TxtNewPassword.Text != null && !string.IsNullOrEmpty(TxtNewPassword.Text.Trim()) &&
                TxtConfirm.Text != null && !string.IsNullOrEmpty(TxtConfirm.Text.Trim()))
            {
                //en este caso están todos los datos requeridos 

                R = true;
            }
            else
            {
                //si falta algún  dato obligatorio 
                if (TxtCurrentPassword.Text == null || string.IsNullOrEmpty(TxtCurrentPassword.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "The current password is required", "OK");
                    TxtCurrentPassword.Focus();
                    return false;
                }

                if (TxtNewPassword.Text == null || string.IsNullOrEmpty(TxtNewPassword.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "The New password is required", "OK");
                    TxtNewPassword.Focus();
                    return false;
                }

                if (TxtConfirm.Text == null || string.IsNullOrEmpty(TxtConfirm.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "The confirmation is required", "OK");
                    TxtConfirm.Focus();
                    return false;
                }

            }

            return R;
        }
        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {
            //primero hacemos validación de campos requeridos 

            if (ValidateFields())
            {
                //sacar un respaldo del usuario global tal y como está en este momento 
                //por si algo sale mal en el proceso de update, reversar los cambios 
                UserDTO BackupLocaluser = new UserDTO();
                BackupLocaluser = GlobalObjects.MyLocalUser;

                try
                {
                    GlobalObjects.MyLocalUser.Contrasennia = TxtNewPassword.Text.Trim();

                    var answer = await DisplayAlert("???", "Are you sure to continue updating user password?", "Yes", "No");

                    if (answer)
                    {
                        bool R = await viewModel.UpdateUserPass(GlobalObjects.MyLocalUser);

                        if (R)
                        {
                            await DisplayAlert(":)", "Password Updated!!!", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert(":(", "Something went wrong...", "OK");
                            await Navigation.PopAsync();
                        }

                    }

                }
                catch (Exception)
                {
                    //si algo sale mal reversamos los cambios 
                    GlobalObjects.MyLocalUser = BackupLocaluser;
                    throw;
                }



            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}