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
using System.Windows.Shapes;
using MediSync.Models;

namespace MediSync.Views
{
    public partial class RegistrarMovimientoView : Window
    {
        public Movimiento NuevoMovimiento { get; private set; }

        public RegistrarMovimientoView()
        {
            InitializeComponent();
            CargarProductos();
        }

        /// <summary>
        /// Carga la lista de productos disponibles en el ComboBox.
        /// </summary>
        private void CargarProductos()
        {
            List<string> productosDisponibles = new List<string>
            {
                "Guantes Estériles",
                "Jeringas 5ml",
                "Batas Quirúrgicas",
                "Mascarillas N95",
                "Solución Salina"
            };

            cbProducto.ItemsSource = productosDisponibles;
            cbProducto.SelectedIndex = 0;
            dpFecha.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Evento para guardar el nuevo movimiento.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cbProducto.SelectedItem == null || cbTipoMovimiento.SelectedItem == null || string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido y mayor a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NuevoMovimiento = new Movimiento
            {
                Id = new Random().Next(1000, 9999), // Generación de ID simulado
                Producto = cbProducto.SelectedItem.ToString(),
                Tipo = ((ComboBoxItem)cbTipoMovimiento.SelectedItem).Content.ToString(),
                Cantidad = cantidad,
                Fecha = dpFecha.SelectedDate ?? DateTime.Now
            };

            this.DialogResult = true;
        }
        
        /// Evento que se ejecuta al hacer clic en el botón "Cancelar".
        /// Cierra la ventana sin realizar cambios.
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Indica que la acción fue cancelada
            this.Close(); // Cierra la ventana
        }
    }
}