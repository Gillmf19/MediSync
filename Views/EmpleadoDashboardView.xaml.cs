using MediSync.Controllers;
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

namespace MediSync.Views
{
    public partial class EmpleadoDashboardView : UserControl
    {
        public EmpleadoDashboardView()
        {
            InitializeComponent();
            CargarStock();
            CargarMovimientos();
        }

        /// <summary>
        /// Cargar lista simulada de stock de productos.
        /// </summary>
        private void CargarStock()
        {
            var productos = new List<Producto>
            {
                new Producto { Nombre = "Guantes Estériles", Stock = 50 },
                new Producto { Nombre = "Jeringas 5ml", Stock = 30 },
                new Producto { Nombre = "Batas Quirúrgicas", Stock = 20 },
                new Producto { Nombre = "Mascarillas N95", Stock = 60 },
                new Producto { Nombre = "Solución Salina", Stock = 40 }
            };

            lvStock.ItemsSource = productos;
        }

        /// <summary>
        /// Cargar lista simulada de movimientos recientes.
        /// </summary>
        private void CargarMovimientos()
        {
            var movimientos = new List<Movimientos>
            {
                new Movimientos { Tipo = "Entrada", Fecha = DateTime.Now.AddDays(-1), Producto = "Guantes" },
                new Movimientos { Tipo = "Salida", Fecha = DateTime.Now.AddDays(-2), Producto = "Jeringas" },
                new Movimientos { Tipo = "Entrada", Fecha = DateTime.Now.AddDays(-3), Producto = "Alcohol en Gel" }
            };

            lvMovimientos.ItemsSource = movimientos;
        }

        /// <summary>
        /// Navegar a la vista de Consulta de Productos.
        /// </summary>
        private void BtnConsultaProductos_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo("ConsultaProductosView");
        }

        /// <summary>
        /// Navegar a la vista de Movimientos.
        /// </summary>
        private void BtnMovimientos_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo("GestionMovimientosEmpleadoView");
        }


        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo("GestionReportesEmpleadoView");
        }
    }

    /// <summary>
    /// Modelo de datos para un movimiento en el inventario.
    /// </summary>
   
}
