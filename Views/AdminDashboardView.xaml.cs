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
using MediSync.Views; // Asegurar que pueda acceder a otras vistas

namespace MediSync.Views
{
    public partial class AdminDashboardView : UserControl
    {
        public AdminDashboardView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método para cambiar entre vistas y ocultar el Dashboard cuando se necesite.
        /// </summary>
        private void MostrarVista(UserControl vista)
        {
            if (DashboardContent != null && MainFrame != null)
            {
                DashboardContent.Visibility = Visibility.Collapsed; // Oculta el Dashboard
                MainFrame.Visibility = Visibility.Visible; // Muestra el Frame
                MainFrame.Content = vista; // Carga la nueva vista
            }
        }

        /// <summary>
        /// Carga la vista de Gestión de Productos.
        /// </summary>
        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            MostrarVista(new GestionProductosView());
        }

        /// <summary>
        /// Carga la vista de Gestión de Categorías.
        /// </summary>
        private void BtnCategorias_Click(object sender, RoutedEventArgs e)
        {
            MostrarVista(new GestionCategoriasView());
        }

        /// <summary>
        /// Regresa al Dashboard Principal.
        /// </summary>
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame != null && DashboardContent != null)
            {
                MainFrame.Content = null; // Limpia el Frame
                MainFrame.Visibility = Visibility.Collapsed; // Oculta el Frame
                DashboardContent.Visibility = Visibility.Visible; // Muestra el Dashboard
            }
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            MostrarVista(new GestionProveedoresView());
        }

        private void BtnMovimientos_Click(object sender, RoutedEventArgs e)
        {
            MostrarVista(new GestionMovimientosView());
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            MostrarVista(new GestionReportesView());
        }

        private void BtnUsuariosRoles_Click(object sender, RoutedEventArgs e)
        {
            MostrarVista(new GestionUsuariosRolesView());
        }
    }
}
