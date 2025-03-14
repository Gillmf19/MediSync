﻿using System;
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
    public partial class GestionProductosView : UserControl
    {
        // Lista de productos simulada para la demostración
        private List<Producto> productos;

        public GestionProductosView()
        {
            InitializeComponent();
            CargarProductos();
        }

        /// <summary>
        /// Método para cargar la lista de productos simulada.
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

            // Enlazar la lista al DataGrid
            dgProductos.ItemsSource = productos;
        }

        /// <summary>
        /// Filtra los productos según el texto ingresado en la barra de búsqueda.
        /// </summary>
        private void TxtBuscarProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarProducto.Text.ToLower();
            dgProductos.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? productos
                : productos.Where(p => p.Nombre.ToLower().Contains(filtro) || p.Categoria.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Método para manejar el evento de clic en el botón "Agregar Producto".
        /// </summary>
        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Abrir la ventana para agregar un nuevo producto
            AgregarProductoView agregarProductoWindow = new AgregarProductoView();
            bool? resultado = agregarProductoWindow.ShowDialog();

            // Si el usuario guardó un nuevo producto, se agrega a la lista
            if (resultado == true)
            {
                Producto nuevoProducto = agregarProductoWindow.NuevoProducto;

                // Agregar el nuevo producto a la lista
                productos.Add(nuevoProducto);

                // Refrescar el DataGrid
                dgProductos.ItemsSource = null;
                dgProductos.ItemsSource = productos;

                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Método para manejar el evento de clic en el botón "Editar Producto".
        /// </summary>
        private void BtnEditarProducto_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se ha seleccionado un producto en la lista
            if (dgProductos.SelectedItem is Producto productoSeleccionado)
            {
                // Abrir la ventana de edición y pasar el producto seleccionado
                EditarProductoView editarProductoWindow = new EditarProductoView(productoSeleccionado);
                bool? resultado = editarProductoWindow.ShowDialog();

                // Si el usuario guardó los cambios, actualizar la lista
                if (resultado == true)
                {
                    Producto productoEditado = editarProductoWindow.ProductoEditado;

                    // Buscar el índice del producto original en la lista
                    int index = productos.FindIndex(p => p.Id == productoSeleccionado.Id);
                    if (index >= 0)
                    {
                        productos[index] = productoEditado;
                    }

                    // Refrescar el DataGrid
                    dgProductos.ItemsSource = null;
                    dgProductos.ItemsSource = productos;

                    MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Método para manejar el evento de clic en el botón "Eliminar Producto".
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

    /// <summary>
    /// Modelo de datos para un producto.
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
        public string Proveedor { get; internal set; }
    }
}

