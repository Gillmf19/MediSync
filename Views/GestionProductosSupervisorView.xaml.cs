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
    public partial class GestionProductosSupervisorView : UserControl
    {
        private List<Producto> productos;

        public GestionProductosSupervisorView()
        {
            InitializeComponent();
            CargarProductos();
        }

        /// <summary>
        /// Cargar lista de productos.
        /// </summary>
        private void CargarProductos()
        {
            productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Guantes Estériles", Categoria = "Equipamiento", Stock = 50 },
                new Producto { Id = 2, Nombre = "Jeringas 5ml", Categoria = "Descartables", Stock = 30 },
                new Producto { Id = 3, Nombre = "Batas Quirúrgicas", Categoria = "Equipamiento", Stock = 20 },
                new Producto { Id = 4, Nombre = "Mascarillas N95", Categoria = "Protección", Stock = 60 },
                new Producto { Id = 5, Nombre = "Solución Salina", Categoria = "Medicamentos", Stock = 40 }
            };

            dgProductos.ItemsSource = productos;
        }

        /// <summary>
        /// Filtra los productos según la búsqueda.
        /// </summary>
        private void TxtBuscarProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarProducto.Text.ToLower();
            dgProductos.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? productos
                : productos.Where(p => p.Nombre.ToLower().Contains(filtro) || p.Categoria.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Evento de clic en el botón "Agregar Producto".
        /// </summary>
        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductoSupervisorView ventanaAgregar = new AgregarProductoSupervisorView();
            if (ventanaAgregar.ShowDialog() == true)
            {
                Producto nuevoProducto = ventanaAgregar.ProductoNuevo;
                if (nuevoProducto != null)
                {
                    productos.Add(nuevoProducto);
                    dgProductos.ItemsSource = null;
                    dgProductos.ItemsSource = productos;
                    MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Evento de clic en el botón "Editar Producto".
        /// </summary>
        private void BtnEditarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto productoSeleccionado)
            {
                // Abrir la ventana de edición y pasar el producto seleccionado
                EditarProductoSupervisor ventanaEditar = new EditarProductoSupervisor(productoSeleccionado);

                if (ventanaEditar.ShowDialog() == true)
                {
                    Producto productoEditado = ventanaEditar.ProductoEditado;

                    if (productoEditado != null)
                    {
                        // Buscar el producto en la lista y actualizarlo
                        int index = productos.FindIndex(p => p.Id == productoSeleccionado.Id);
                        if (index >= 0)
                        {
                            productos[index] = productoEditado;
                            dgProductos.ItemsSource = null; // Resetear DataGrid
                            dgProductos.ItemsSource = productos; // Recargar datos
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        /// <summary>
        /// Evento de clic en el botón "Eliminar Producto".
        /// </summary>
        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto productoSeleccionado)
            {
                var resultado = MessageBox.Show($"¿Está seguro de eliminar el producto {productoSeleccionado.Nombre}?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    productos.Remove(productoSeleccionado);
                    dgProductos.ItemsSource = null;
                    dgProductos.ItemsSource = productos;
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

