using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocols_Juan.Services
{
    public static class APIConnection
    {
        //Acá definimos la direccion URL, ya sea una ip o un nombre de un dominio, a la que la app debe apuntar
        //Por mayor comodidad, la ruta URL completa para consumir los recursos del API se hará en formato "Prefijo"+"sufijo"
        //donde el prefijo será la parte del URL que nunca cambiará y el sufijo será la parte variable
        //(nombre del controlador y sus parámetros)

        public static string ProductionPrefixURL = "http://192.168.0.15:45455/api/";
        public static string TestingPrefixURL = "http://192.168.0.15:45455/api/";

        public static string ApiKeyName = "Progra6ApiKeyJJ";
        public static string ApiKeyValue = "JuanJoseProgra6acb123";

    }
}
