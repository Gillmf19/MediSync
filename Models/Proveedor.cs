using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MediSync.Models
{
    /// <summary>
    /// Representa un proveedor dentro del sistema de gestión de inventarios médicos.
    /// </summary>
    public class Proveedor
    {
        /// <summary>
        /// Identificador único del proveedor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Número de teléfono del proveedor.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del proveedor.
        /// </summary>
        public string Correo { get; set; }

        //Contacto del Proveedor
        public string Contacto { get; set; }

        //Direccion del Proveedor
        public string Direccion { get; set; }
    }
}

