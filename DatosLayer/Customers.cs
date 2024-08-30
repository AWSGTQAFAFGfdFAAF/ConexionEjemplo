using System;                          // Importa las clases básicas del sistema.
using System.Collections.Generic;      // Importa las clases genéricas, como List.
using System.Linq;                     // Permite el uso de LINQ para consultas en colecciones.
using System.Text;                     // Importa las clases para manipulación de texto.
using System.Threading.Tasks;          // Importa las clases para programación asincrónica.

namespace DatosLayer
{
    // Clase que representa un cliente en la base de datos.
    public class Customers
    {
        // Propiedad que almacena el ID del cliente.
        public string CustomerID { get; set; }

        // Propiedad que almacena el nombre de la compañía del cliente.
        public string CompanyName { get; set; }

        // Propiedad que almacena el nombre de contacto del cliente.
        public string ContactName { get; set; }

        // Propiedad que almacena el título del contacto del cliente.
        public string ContactTitle { get; set; }

        // Propiedad que almacena la dirección del cliente.
        public string Address { get; set; }

        // Propiedad que almacena la ciudad donde se encuentra el cliente.
        public string City { get; set; }

        // Propiedad que almacena la región donde se encuentra el cliente.
        public string Region { get; set; }

        // Propiedad que almacena el código postal del cliente.
        public string PostalCode { get; set; }

        // Propiedad que almacena el país del cliente.
        public string Country { get; set; }

        // Propiedad que almacena el número de teléfono del cliente.
        public string Phone { get; set; }

        // Propiedad que almacena el número de fax del cliente.
        public string Fax { get; set; }
    }
}
