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
    public partial class EditarProductoView : Window
    {
        public Producto ProductoEditado { get; private set; }

        /// <summary>
        /// Constructor que inicializa la ventana con los datos del producto seleccionado.
        /// </summary>
        /// <param name="producto">Producto a editar.</param>
        public EditarProductoView(Producto producto)
        {
            InitializeComponent();
            CargarDatos(producto);
        }

        /// <summary>
        /// Cargar los datos del producto en los campos del formulario.
        /// </summary>
        /// <param name="producto">Producto a editar.</param>
        private void CargarDatos(Producto producto)
        {
            txtNombre.Text = producto.Nombre;
            txtCantidad.Text = producto.Stock.ToString(); // Se usa Stock en lugar de Cantidad

            // Verifica si el producto tiene un proveedor asignado
            if (!string.IsNullOrEmpty(producto.Proveedor))
            {
                txtProveedor.Text = producto.Proveedor;
            }

            // Buscar y seleccionar la categoría correcta en el ComboBox
            foreach (ComboBoxItem item in cbCategoria.Items)
            {
                if (item.Content.ToString() == producto.Categoria)
                {
                    cbCategoria.SelectedItem = item;
                    break;
                }
            }
        }

        /// <summary>
        /// Evento para guardar los cambios en el producto.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                cbCategoria.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtProveedor.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que la cantidad sea un número válido y mayor o igual a 0
            if (!int.TryParse(txtCantidad.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número válido y mayor o igual a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear el objeto con los datos editados
            ProductoEditado = new Producto
            {
                Id = 0, // Se mantiene el ID original
                Nombre = txtNombre.Text,
                Stock = stock, // Se usa Stock en lugar de Cantidad
                Categoria = ((ComboBoxItem)cbCategoria.SelectedItem).Content.ToString(),
                Proveedor = txtProveedor.Text // ✅ Se guarda correctamente el proveedor
            };

            // Indicar que la edición fue exitosa y cerrar la ventana
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
