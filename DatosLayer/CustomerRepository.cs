using System;                           // Importa las clases básicas del sistema.
using System.Collections.Generic;       // Importa las clases genéricas como List.
using System.Data.SqlClient;            // Importa las clases necesarias para trabajar con SQL Server.
using System.Linq;                      // Permite el uso de LINQ para consultas en colecciones.
using System.Net.Http.Headers;          // Importa las clases para trabajar con cabeceras HTTP.
using System.Text;                      // Importa las clases para manipulación de texto.
using System.Threading.Tasks;           // Importa las clases para programación asincrónica.

namespace DatosLayer
{
    public class CustomerRepository
    {
        // Método para obtener todos los clientes de la base de datos.
        public List<Customers> ObtenerTodos()
        {
            // Abre una conexión a la base de datos.
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Define la consulta SQL para seleccionar todos los campos de la tabla Customers.
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Ejecuta la consulta SQL y obtiene los resultados.
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader();
                    List<Customers> Customers = new List<Customers>();

                    // Lee los resultados y los agrega a la lista de clientes.
                    while (reader.Read())
                    {
                        var customers = LeerDelDataReader(reader);
                        Customers.Add(customers);
                    }
                    return Customers;   // Devuelve la lista de clientes.
                }
            }
        }

        // Método para obtener un cliente por su ID.
        public Customers ObtenerPorID(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Define la consulta SQL para seleccionar un cliente por su ID.
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n";
                selectForID = selectForID + $"  Where CustomerID = @customerId";

                // Ejecuta la consulta SQL y obtiene el resultado.
                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    comando.Parameters.AddWithValue("customerId", id);  // Asigna el valor del parámetro.

                    var reader = comando.ExecuteReader();
                    Customers customers = null;

                    // Si se encuentra un registro, se asigna al objeto customer.
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader);
                    }
                    return customers;  // Devuelve el cliente encontrado.
                }
            }
        }

        // Método para leer un cliente desde un SqlDataReader.
        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers customers = new Customers();
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            return customers;   // Devuelve el objeto cliente con los datos leídos.
        }

        // Método para insertar un nuevo cliente en la base de datos.
        public int InsertarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Define la consulta SQL para insertar un nuevo cliente.
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                // Ejecuta la consulta SQL para insertar el cliente.
                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    int insertados = parametrosCliente(customer, comando);
                    return insertados;  // Devuelve el número de filas insertadas.
                }
            }
        }

        // Método para actualizar un cliente existente en la base de datos.
        public int ActualizarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Define la consulta SQL para actualizar un cliente existente por su ID.
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID = @CustomerID";

                // Ejecuta la consulta SQL para actualizar el cliente.
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                {
                    int actualizados = parametrosCliente(customer, comando);
                    return actualizados;  // Devuelve el número de filas actualizadas.
                }
            }
        }

        // Método auxiliar para agregar los parámetros de un cliente a un SqlCommand.
        public int parametrosCliente(Customers customer, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName); // Probablemente debería ser "customer.ContactTitle"
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);
            var insertados = comando.ExecuteNonQuery();  // Ejecuta la consulta SQL.
            return insertados;  // Devuelve el número de filas afectadas.
        }

        // Método para eliminar un cliente de la base de datos por su ID.
        public int EliminarCliente(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Define la consulta SQL para eliminar un cliente por su ID.
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";

                // Ejecuta la consulta SQL para eliminar el cliente.
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                {
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    int elimindos = comando.ExecuteNonQuery();  // Ejecuta la consulta SQL.
                    return elimindos;  // Devuelve el número de filas eliminadas.
                }
            }
        }
    }
}

