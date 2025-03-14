using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediSync.Models;

namespace MediSync.Views
{
    public partial class GestionMovimientosEmpleadoView : UserControl
    {
        private List<Movimiento> movimientos;

        public GestionMovimientosEmpleadoView()
        {
            InitializeComponent();
            CargarMovimientos();
        }

        /// <summary>
        /// Carga una lista de movimientos simulados.
        /// </summary>
        private void CargarMovimientos()
        {
            movimientos = new List<Movimiento>
            {
                new Movimiento { Id = 1, Producto = "Guantes Estériles", Cantidad = 50, Tipo = "Entrada", Fecha = DateTime.Now.AddDays(-2) },
                new Movimiento { Id = 2, Producto = "Jeringas 5ml", Cantidad = 30, Tipo = "Salida", Fecha = DateTime.Now.AddDays(-1) },
                new Movimiento { Id = 3, Producto = "Batas Quirúrgicas", Cantidad = 20, Tipo = "Entrada", Fecha = DateTime.Now }
            };
            dgMovimientos.ItemsSource = movimientos;
        }

        /// <summary>
        /// Filtra la lista de movimientos en función del texto ingresado en la barra de búsqueda.
        /// </summary>
        private void TxtBuscarMovimiento_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarMovimiento.Text.ToLower();
            dgMovimientos.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? movimientos
                : movimientos.Where(m => m.Producto.ToLower().Contains(filtro)).ToList();
        }
    }

    
   
}

