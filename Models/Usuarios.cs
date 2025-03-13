using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSync.Models
{
    /// <summary>
    /// Representa a un usuario dentro del sistema.
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; } // En un sistema real, debería ser encriptada
        public string Rol { get; set; }
    }
}

