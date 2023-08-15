using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyProtocols_Juan.Models
{
    public class User
    {

        public RestRequest Request { get; set; }

        //En este ejemplo usaré los mismos atributos que en el modelo del API
        //posteriormente en otra clase usaré el DTO del usuario para simplificar
        //el json que se envía y recibe desde el API

        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string BackUpEmail { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        public bool? Active { get; set; }

        public bool? IsBlocked { get; set; }

        public int UserRoleId { get; set; }

        public virtual UserRole? UserRole { get; set; }

        //Funciones específicas de llamada a end points del API

        //Función que permite validar que los datos digitados en la página de
        //applogin sean correctos o no

        public async Task<bool> ValidateUserLogin()
        {
            try
            {
                //Usaremos el prefijo de la ruta del API que se indica en
                //Services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo del end point que se quiere usar.

                string RouteSufix = string.Format("Users/ValidateLogin?username={0}&password={1}", this.Email, this.Password);

                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //Agregamos el mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                //Ejecutar la llamada del API
                RestResponse response = await client.ExecuteAsync(Request);

                //Saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }

        public async Task<bool> AddUSerAsync()
        {
            try
            {

                string RouteSufix = string.Format("Users");

                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                //Agregamos el mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                string SerializedModelObject = JsonConvert.SerializeObject(this);
                Request.AddBody(SerializedModelObject, GlobalObjects.MimeType);

                //Ejecutar la llamada del API
                RestResponse response = await client.ExecuteAsync(Request);

                //Saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }
    }
}
