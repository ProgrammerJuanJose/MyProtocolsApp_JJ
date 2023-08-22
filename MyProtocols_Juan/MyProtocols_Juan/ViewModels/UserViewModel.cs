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
        public UserDTO MyUserDTO { get; set; }
        public UserViewModel()
        {
            MyUser = new User();
            MyUserRole = new UserRole();
            MyUserDTO = new UserDTO();
        }

        //Funciones

        public async Task<UserDTO> GetUserDataAsync(string pEmail)
        {
            if (IsBusy) return null;
            IsBusy = true;

            try
            {
                UserDTO userDTO = new UserDTO();
                userDTO = await MyUserDTO.GetUserInfo(pEmail);

                if (userDTO == null) return null;
                return userDTO;
            }
            catch
            {
                return null;
                throw;
            }
            finally { IsBusy = false; }
        }


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

        public async Task<bool> UpdateUser(UserDTO pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUserDTO = pUser;
                bool R = await MyUserDTO.UpdateUserAsync();
                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
        }

        public async Task<bool> UpdateUserPass(UserDTO pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUserDTO = pUser;
                bool R = await MyUserDTO.UpdateUserPassAsync();
                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
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

        public async Task<List<User>> GetUserAsync()
        {
            try
            {
                List<User> users = new List<User>();
                users = await MyUser.GetAllUserAsync();
                if (users == null)
                {
                    return null;
                }
                return users;
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

        //Función de modificación de contraseña (TAREA)

        public async Task<bool> ModifyPasswordAsync(string pEmail, string pPassword)
        {
            if (IsBusy) return false;
            try
            {
                MyUser = new User();
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ModifyPasswordAsync();

                return R;
            }
            catch (Exception)
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
