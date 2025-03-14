using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSync.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // Entrada o Salida
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}

