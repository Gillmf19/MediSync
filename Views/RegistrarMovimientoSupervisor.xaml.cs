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
    public partial class RegistrarMovimientoSupervisor : Window
    {
        public Movimientos MovimientoRegistrado { get; private set; } // ✅ Propiedad para devolver el movimiento creado

        public RegistrarMovimientoSupervisor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda el nuevo movimiento con los datos ingresados.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtProducto.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                cbTipoMovimiento.SelectedItem == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que la cantidad sea un número válido
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido mayor a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtener el tipo de movimiento
            string tipoMovimiento = (cbTipoMovimiento.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Crear el objeto del nuevo movimiento
            MovimientoRegistrado = new Movimientos
            {
                Id = new Random().Next(1000, 9999), // Generar un ID aleatorio
                Producto = txtProducto.Text,
                Cantidad = cantidad,
                Tipo = tipoMovimiento,
                Fecha = DateTime.Now
            };

            // Indicar que se guardó correctamente y cerrar
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Cierra la ventana sin guardar cambios.
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
