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
    public partial class GestionMovimientosView : UserControl
    {
        // Lista de movimientos simulada
        private List<Movimiento> movimientos;

        public GestionMovimientosView()
        {
            InitializeComponent();
            CargarMovimientos();
        }

        /// <summary>
        /// Carga una lista de movimientos simulados en la tabla.
        /// </summary>
        private void CargarMovimientos()
        {
            movimientos = new List<Movimiento>
            {
                new Movimiento { Id = 1, Producto = "Guantes Estériles", Tipo = "Entrada", Cantidad = 100, Fecha = DateTime.Now.AddDays(-2) },
                new Movimiento { Id = 2, Producto = "Jeringas 5ml", Tipo = "Salida", Cantidad = 20, Fecha = DateTime.Now.AddDays(-1) },
                new Movimiento { Id = 3, Producto = "Batas Quirúrgicas", Tipo = "Entrada", Cantidad = 50, Fecha = DateTime.Now },
                new Movimiento { Id = 4, Producto = "Mascarillas N95", Tipo = "Salida", Cantidad = 30, Fecha = DateTime.Now.AddDays(-3) }
            };

            // Enlazar la lista al DataGrid
            dgMovimientos.ItemsSource = movimientos;
        }

        /// <summary>
        /// Filtra los movimientos según el texto ingresado en la barra de búsqueda.
        /// </summary>
        private void TxtBuscarMovimiento_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarMovimiento.Text.ToLower();
            dgMovimientos.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? movimientos
                : movimientos.Where(m => m.Producto.ToLower().Contains(filtro) || m.Tipo.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Abre la ventana de registrar un nuevo movimiento.
        /// </summary>
        private void BtnRegistrarMovimiento_Click(object sender, RoutedEventArgs e)
        {
            RegistrarMovimientoView registrarMovimiento = new RegistrarMovimientoView();
            if (registrarMovimiento.ShowDialog() == true)
            {
                movimientos.Add(registrarMovimiento.NuevoMovimiento);
                dgMovimientos.ItemsSource = null;
                dgMovimientos.ItemsSource = movimientos;
                MessageBox.Show("Movimiento registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Muestra los detalles del movimiento seleccionado.
        /// </summary>
        private void BtnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            if (dgMovimientos.SelectedItem is Movimiento movimientoSeleccionado)
            {
                MessageBox.Show($"Detalles del movimiento:\n\nProducto: {movimientoSeleccionado.Producto}\nCantidad: {movimientoSeleccionado.Cantidad}\nFecha: {movimientoSeleccionado.Fecha}",
                                "Detalles del Movimiento", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Seleccione un movimiento para ver los detalles.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    /// <summary>
    /// Modelo de datos para un movimiento de inventario.
    /// </summary>
    public class Movimiento
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Tipo { get; set; } // Entrada o Salida
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
