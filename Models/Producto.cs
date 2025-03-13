using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSync.Models
{
    /// <summary>
    /// Representa un producto en el sistema de gestión de inventarios médicos.
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Categoría a la que pertenece el producto.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Cantidad disponible en el inventario.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Nombre del proveedor asociado al producto.
        /// </summary>
        public string Proveedor { get; set; } 
    }
}
