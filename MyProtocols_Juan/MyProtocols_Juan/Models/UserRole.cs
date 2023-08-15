using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyProtocols_Juan.Models
{
    public class UserRole
    {
        public RestRequest Request { get; set; }
        public int UserRoleId { get; set; }

        public string Description { get; set; } = null!;

        //Funciones
        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            try
            {
                //Usaremos el prefijo de la ruta del API que se indica en
                //Services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo del end point que se quiere usar.

                string RouteSufix = string.Format("UserRoles");

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
                    var list = JsonConvert.DeserializeObject<List<UserRole>>(response.Content);

                    return list;
                }
                else
                {
                    return null;
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
