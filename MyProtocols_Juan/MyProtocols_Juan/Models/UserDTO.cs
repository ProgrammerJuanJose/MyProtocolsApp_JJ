using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace MyProtocols_Juan.Models
{
    public class UserDTO
    {
        [Newtonsoft.Json.JsonIgnore]
        public RestRequest Request { get; set; }

        public int IDUsuario { get; set; }

        public string Correo { get; set; } = null!;

        public string Contrasennia { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string CorreoRespaldo { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string? Direccion { get; set; }

        public bool? Activo { get; set; }

        public bool? EstaBloqueado { get; set; }

        public int IDRol { get; set; }

        public string DescripcionRol { get; set; } = null!;

        //FUNCIONES
        public async Task<UserDTO> GetUserInfo(string PEmail)
        {
            try
            {
                //Usaremos el prefijo de la ruta del API que se indica en
                //Services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo del end point que se quiere usar.

                string RouteSufix = string.Format("Users/GetUserInfoByEmail?Pemail={0}",PEmail);

                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //Agregamos el mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //Ejecutar la llamada del API
                RestResponse response = await client.ExecuteAsync(Request);

                //Saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);
                    var item = list[0];
                    return item;
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

        public async Task<bool> UpdateUserAsync()
        {
            try
            {

                string RouteSufix = string.Format("Users/{0}", this.IDUsuario);

                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

                //Agregamos el mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                string SerializedModel = JsonConvert.SerializeObject(this);
                Request.AddBody(SerializedModel, GlobalObjects.MimeType);

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

        public async Task<bool> UpdateUserPassAsync()
            {
                try
                {

                    string RouteSufix = string.Format("Users/{0}", this.Correo);

                    string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                    RestClient client = new RestClient(URL);

                    Request = new RestRequest(URL, Method.Put);

                    //Agregamos el mecanismo de seguridad, en este caso API key
                    Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                    Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                    string SerializedModel = JsonConvert.SerializeObject(this);
                    Request.AddBody(SerializedModel, GlobalObjects.MimeType);

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
    }
}
