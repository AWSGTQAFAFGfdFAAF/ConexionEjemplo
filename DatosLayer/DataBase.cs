using System;                           // Importa las clases básicas del sistema.
using System.Collections.Generic;       // Importa las clases genéricas como List.
using System.Linq;                      // Importa clases para consultas LINQ.
using System.Text;                      // Importa clases para manipulación de texto.
using System.Threading.Tasks;           // Importa clases para programación asincrónica.
using System.Configuration;             // Importa clases para acceder a configuraciones, como cadenas de conexión.
using System.Xml.Linq;                  // Importa clases para trabajar con XML.
using System.Data.SqlClient;            // Importa clases para trabajar con SQL Server.
using System.Runtime.CompilerServices;  // Importa clases para compilación y metadatos.

namespace DatosLayer
{
    // Clase que maneja la configuración de la base de datos y proporciona conexiones SQL.
    public class DataBase
    {
        // Propiedad estática que obtiene la cadena de conexión a la base de datos.
        public static string ConnectionString
        {
            get
            {
                // Obtiene la cadena de conexión de la configuración.
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Crea un objeto SqlConnectionStringBuilder a partir de la cadena de conexión.
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Asigna el nombre de la aplicación, si se ha especificado.
                conexionBuilder.ApplicationName =
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Asigna el tiempo de espera de conexión si es mayor que 0, o usa el valor predeterminado.
                conexionBuilder.ConnectTimeout = (ConnectionTimeout > 0)
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Devuelve la cadena de conexión modificada.
                return conexionBuilder.ToString();
            }
        }

        // Propiedad estática que define el tiempo de espera de la conexión.
        public static int ConnectionTimeout { get; set; }

        // Propiedad estática que define el nombre de la aplicación que utiliza la conexión.
        public static string ApplicationName { get; set; }

        // Método estático que abre y devuelve una conexión SQL.
        public static SqlConnection GetSqlConnection()
        {
            // Crea una nueva conexión SQL utilizando la cadena de conexión definida.
            SqlConnection conexion = new SqlConnection(ConnectionString);

            // Abre la conexión.
            conexion.Open();

            // Devuelve la conexión abierta.
            return conexion;
        }
    }
}
