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
    public partial class GestionProductosView : UserControl
    {
        // Lista de productos médicos
        private List<Producto> productos;
        private List<Producto> productosFiltrados;

        public GestionProductosView()
        {
            InitializeComponent();
            CargarProductos();
        }

        /// <summary>
        /// Método para cargar productos médicos en la tabla.
        /// </summary>
        private void CargarProductos()
        {
            productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Guantes Estériles", Cantidad = 50, Categoria = "Equipamiento", Proveedor = "MediPro" },
                new Producto { Id = 2, Nombre = "Jeringas 5ml", Cantidad = 30, Categoria = "Descartables", Proveedor = "HealthSupplies" },
                new Producto { Id = 3, Nombre = "Batas Quirúrgicas", Cantidad = 20, Categoria = "Equipamiento", Proveedor = "SurgicalTech" },
                new Producto { Id = 4, Nombre = "Mascarillas N95", Cantidad = 60, Categoria = "Protección", Proveedor = "SafeMed" }
            };

            productosFiltrados = new List<Producto>(productos);
            dgProductos.ItemsSource = productosFiltrados;
        }

        /// <summary>
        /// Evento para agregar un nuevo producto.
        /// </summary>
        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductoView ventanaAgregar = new AgregarProductoView();
            if (ventanaAgregar.ShowDialog() == true)
            {
                productos.Add(ventanaAgregar.NuevoProducto);
                dgProductos.ItemsSource = null;
                dgProductos.ItemsSource = productos;
            }
        }

        /// <summary>
        /// Evento para editar un producto seleccionado.
        /// </summary>
        private void BtnEditarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto productoSeleccionado)
            {
                MessageBox.Show($"Editar producto: {productoSeleccionado.Nombre}", "Editar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Evento para eliminar un producto seleccionado.
        /// </summary>
        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto productoSeleccionado)
            {
                MessageBoxResult result = MessageBox.Show($"¿Seguro que deseas eliminar {productoSeleccionado.Nombre}?",
                                                          "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    productos.Remove(productoSeleccionado);
                    FiltrarProductos("");
                }
            }
        }

        /// <summary>
        /// Método para filtrar productos en la búsqueda.
        /// </summary>
        private void TxtBuscarProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarProductos(txtBuscarProducto.Text);
        }

        /// <summary>
        /// Filtra la lista de productos en base al texto ingresado.
        /// </summary>
        private void FiltrarProductos(string texto)
        {
            productosFiltrados = productos
                .Where(p => p.Nombre.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            dgProductos.ItemsSource = null;
            dgProductos.ItemsSource = productosFiltrados;
        }
    }

    /// <summary>
    /// Clase que representa un producto médico.
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }
    }
}
