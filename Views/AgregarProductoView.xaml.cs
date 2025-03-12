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

namespace MediSync.Views
{
    public partial class AgregarProductoView : Window
    {
        public Producto NuevoProducto { get; private set; }

        public AgregarProductoView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para guardar un nuevo producto.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                cbCategoria.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtProveedor.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad < 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NuevoProducto = new Producto
            {
                Id = new System.Random().Next(100, 999), // ID Simulado
                Nombre = txtNombre.Text,
                Cantidad = cantidad,
                Categoria = ((ComboBoxItem)cbCategoria.SelectedItem).Content.ToString(),
                Proveedor = txtProveedor.Text
            };

            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Evento para cerrar la ventana sin guardar.
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

