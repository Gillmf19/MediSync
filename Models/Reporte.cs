using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MediSync.Models
{
    /// <summary>
    /// Representa un reporte en el sistema de gestión de inventarios médicos.
    /// </summary>
    public class Reporte
    {
        /// <summary>
        /// Identificador único del reporte.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del reporte.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Fecha de generación del reporte.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Formato del reporte (ejemplo: PDF, Excel).
        /// </summary>
        public string Formato { get; set; }

        /// <summary>
        /// Ruta donde se guardó el archivo del reporte.
        /// </summary>
        public string Ruta { get; set; }
    }
}

