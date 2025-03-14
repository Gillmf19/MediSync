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
using LiveCharts;
using LiveCharts.Wpf;
using MediSync.Controllers;
using MediSync.Models;

namespace MediSync.Views
{
    public partial class SupervisorDashboardView : UserControl
    {
        public SeriesCollection MovimientosData { get; set; }
        public List<string> FechasMovimientos { get; set; }

        public SupervisorDashboardView()
        {
            InitializeComponent();
            CargarAlertasStock();
            CargarTransaccionesRecientes();
            CargarGraficoMovimientos();

            // Establecer contexto de datos para la gráfica
            DataContext = this;
        }

        /// <summary>
        /// Método para cargar alertas de stock bajo.
        /// </summary>
        private void CargarAlertasStock()
        {
            var productos = new List<Producto>
            {
                new Producto { Nombre = "Guantes Estériles", Stock = 5 },
                new Producto { Nombre = "Jeringas 5ml", Stock = 2 },
                new Producto { Nombre = "Alcohol en Gel", Stock = 8 }
            };

            lvAlertasStock.ItemsSource = productos.Where(p => p.Stock < 10).ToList();
        }

        /// <summary>
        /// Método para cargar las transacciones recientes.
        /// </summary>
        private void CargarTransaccionesRecientes()
        {
            var transacciones = new List<Transaccion>
            {
                new Transaccion { Tipo = "Entrada", Fecha = DateTime.Now.AddDays(-1), Producto = "Guantes", Cantidad = 50 },
                new Transaccion { Tipo = "Salida", Fecha = DateTime.Now.AddDays(-2), Producto = "Jeringas", Cantidad = 30 },
                new Transaccion { Tipo = "Entrada", Fecha = DateTime.Now.AddDays(-3), Producto = "Alcohol en Gel", Cantidad = 25 }
            };

            lvTransacciones.ItemsSource = transacciones;
        }

        /// <summary>
        /// Método para generar datos de la gráfica de movimientos.
        /// </summary>
        private void CargarGraficoMovimientos()
        {
            var movimientos = new Dictionary<string, int>
            {
                { "01 Mar", 50 },
                { "02 Mar", 30 },
                { "03 Mar", 40 },
                { "04 Mar", 25 }
            };

            MovimientosData = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Movimientos",
                    Values = new ChartValues<int>(movimientos.Values)
                }
            };

            FechasMovimientos = movimientos.Keys.ToList();
        }

        /// <summary>
        /// Evento para navegar a la vista de Gestión de Productos del Supervisor.
        /// </summary>
        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo("GestionProductosSupervisorView");
        }

        private void BtnMovimientos_Click(object sender, RoutedEventArgs e)
        {
            // Navegar a la vista de Gestión de Movimientos para el Supervisor
            NavigationController.NavigateTo("GestionMovimientosSupervisor");
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            // Navega a la vista de Gestión de Reportes del Supervisor
            NavigationController.NavigateTo("GestionReportesSupervisorView");
        }


    }

    /// <summary>
    /// Modelo para representar una transacción en el inventario.
    /// </summary>
    public class Transaccion
    {
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
