using MyProtocols_Juan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocols_Juan
{
    public static class GlobalObjects
    {
        //definimos las propiedades de codificación de los json
        //que usaré en los modelos
        public static string MimeType = "application/json";
        public static string ContentType = "Content-Type";

        //Crear el objeto local (Global) de usuario
        public static UserDTO MyLocalUser = new UserDTO();
    }
}
