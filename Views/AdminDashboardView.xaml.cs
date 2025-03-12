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

namespace MediSync.Views
{
    public partial class AdminDashboardView : UserControl
    {
        public SeriesCollection StockProductos { get; set; }
        public List<string> NombresProductos { get; set; }

        public AdminDashboardView()
        {
            InitializeComponent();
            CargarDatosInventario();

            // Establecer DataContext para la gráfica
            DataContext = this;
        }

        /// <summary>
        /// Método para cargar datos simulados de productos médicos en la gráfica.
        /// </summary>
        private void CargarDatosInventario()
        {
            // Simulación de productos médicos con su stock disponible
            var productos = new Dictionary<string, int>
            {
                { "Guantes Estériles", 50 },
                { "Jeringas 5ml", 30 },
                { "Batas Quirúrgicas", 20 },
                { "Mascarillas N95", 60 },
                { "Solución Salina", 40 }
            };

            // Asignar datos a la gráfica
            StockProductos = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Stock Disponible",
                    Values = new ChartValues<int>(productos.Values)
                }
            };

            // Nombres de los productos en el eje X
            NombresProductos = new List<string>(productos.Keys);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo("GestionProductosView");
        }
    }
}

