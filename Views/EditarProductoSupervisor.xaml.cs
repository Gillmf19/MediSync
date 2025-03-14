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
    public partial class EditarProductoSupervisor : Window
    {
        public Producto ProductoEditado { get; private set; }
        private Producto _productoOriginal;
        private List<string> categoriasDisponibles = new List<string>
        {
            "Equipamiento", "Descartables", "Protección", "Medicamentos", "Material de Curación"
        };

        /// <summary>
        /// Constructor que inicializa la ventana con los datos del producto seleccionado.
        /// </summary>
        /// <param name="producto">Producto a editar.</param>
        public EditarProductoSupervisor(Producto producto)
        {
            InitializeComponent();
            _productoOriginal = producto;
            CargarCategorias();  // Cargar las categorías disponibles
            CargarDatos(producto); // Cargar los datos del producto
        }

        /// <summary>
        /// Carga la lista de categorías en el ComboBox.
        /// </summary>
        private void CargarCategorias()
        {
            cbCategoria.Items.Clear();
            foreach (var categoria in categoriasDisponibles)
            {
                cbCategoria.Items.Add(categoria);
            }
        }

        /// <summary>
        /// Carga los datos del producto en los campos del formulario.
        /// </summary>
        private void CargarDatos(Producto producto)
        {
            txtNombre.Text = producto.Nombre;
            txtStock.Text = producto.Stock.ToString();

            // Seleccionar la categoría correcta en el ComboBox
            cbCategoria.SelectedItem = categoriasDisponibles.Contains(producto.Categoria) ? producto.Categoria : null;
        }

        /// <summary>
        /// Guarda los cambios en el producto editado.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                cbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número válido y mayor o igual a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ProductoEditado = new Producto
            {
                Id = _productoOriginal.Id,
                Nombre = txtNombre.Text,
                Stock = stock,
                Categoria = cbCategoria.SelectedItem.ToString()
            };

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
