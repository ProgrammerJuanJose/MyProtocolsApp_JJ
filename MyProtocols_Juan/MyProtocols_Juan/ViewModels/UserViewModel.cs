using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProtocols_Juan.Models;

namespace MyProtocols_Juan.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public User MyUser { get; set; }
        public UserRole MyUserRole { get; set; }
        public UserViewModel()
        {
            MyUser = new User();
            MyUserRole = new UserRole();
        }

        //Funciones
        public async Task<bool> UserAccessValidation(string pEmail, string pPassword)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ValidateUserLogin();

                return R;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Carga la lista de roles en el picker de roles en la creación de un user nuevo
        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            try
            {
                List<UserRole> roles = new List<UserRole>();
                roles = await MyUserRole.GetAllUserRolesAsync();
                if (roles == null)
                {
                    return null;
                }
                return roles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Función de creación de usuario nuevo
        public async Task<bool> AddUserAsync(string pEmail,
                                             string pPassword,
                                             string pName,
                                             string pBackUpEmail,
                                             string pPhoneNumber,
                                             string pAddress,
                                             int pUserRoleID)
        {
            if (IsBusy) return false;
            try
            {
                MyUser = new User();
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;
                MyUser.Name = pName;
                MyUser.BackUpEmail = pBackUpEmail;
                MyUser.PhoneNumber = pPhoneNumber;
                MyUser.Address = pAddress;
                MyUser.UserRoleId = pUserRoleID;
                
                bool R = await MyUser.AddUSerAsync();

                return R;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
