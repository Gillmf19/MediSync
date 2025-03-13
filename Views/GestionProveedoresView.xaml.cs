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
    public partial class GestionProveedoresView : UserControl
    {
        private List<Proveedor> proveedores;

        public GestionProveedoresView()
        {
            InitializeComponent();
            CargarProveedores();
        }

        /// <summary>
        /// Método para cargar la lista de proveedores simulada.
        /// </summary>
        private void CargarProveedores()
        {
            proveedores = new List<Proveedor>
            {
                new Proveedor { Id = 1, Nombre = "Proveedor A", Telefono = "555-1234", Correo = "proveedorA@email.com" },
                new Proveedor { Id = 2, Nombre = "Proveedor B", Telefono = "555-5678", Correo = "proveedorB@email.com" },
                new Proveedor { Id = 3, Nombre = "Proveedor C", Telefono = "555-9101", Correo = "proveedorC@email.com" }
            };

            dgProveedores.ItemsSource = proveedores;
        }

        /// <summary>
        /// Filtra los proveedores según el texto ingresado en la barra de búsqueda.
        /// </summary>
        private void TxtBuscarProveedor_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarProveedor.Text.ToLower();
            dgProveedores.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? proveedores
                : proveedores.Where(p => p.Nombre.ToLower().Contains(filtro) || p.Correo.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Método para manejar el evento de clic en el botón "Agregar Proveedor".
        /// </summary>
        private void BtnAgregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            // Crear y mostrar la ventana emergente para agregar proveedor
            AgregarProveedorView agregarProveedorView = new AgregarProveedorView();

            // Si el usuario presiona "Guardar", se agrega el proveedor a la lista
            if (agregarProveedorView.ShowDialog() == true)
            {
                proveedores.Add(agregarProveedorView.NuevoProveedor);
                dgProveedores.ItemsSource = null;
                dgProveedores.ItemsSource = proveedores;
                MessageBox.Show("Proveedor agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        /// <summary>
        /// Método para manejar el evento de clic en el botón "Editar Proveedor".
        /// </summary>
        private void BtnEditarProveedor_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay un proveedor seleccionado
            if (dgProveedores.SelectedItem is Proveedor proveedorSeleccionado)
            {
                // Abrir la ventana de edición con los datos del proveedor seleccionado
                EditarProveedorView editarProveedorView = new EditarProveedorView(proveedorSeleccionado);

                // Si el usuario presiona "Guardar", actualizar la lista de proveedores
                if (editarProveedorView.ShowDialog() == true)
                {
                    // Buscar el proveedor en la lista y actualizar sus datos
                    var proveedorEditado = proveedores.FirstOrDefault(p => p.Id == proveedorSeleccionado.Id);
                    if (proveedorEditado != null)
                    {
                        proveedorEditado.Nombre = editarProveedorView.ProveedorEditado.Nombre;
                        proveedorEditado.Contacto = editarProveedorView.ProveedorEditado.Contacto;
                        proveedorEditado.Correo = editarProveedorView.ProveedorEditado.Correo;
                        proveedorEditado.Telefono = editarProveedorView.ProveedorEditado.Telefono;
                        proveedorEditado.Direccion = editarProveedorView.ProveedorEditado.Direccion;

                        // Actualizar el DataGrid
                        dgProveedores.ItemsSource = null;
                        dgProveedores.ItemsSource = proveedores;

                        MessageBox.Show("Proveedor actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Método para manejar el evento de clic en el botón "Eliminar Proveedor".
        /// </summary>
        private void BtnEliminarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (dgProveedores.SelectedItem is Proveedor proveedorSeleccionado)
            {
                var resultado = MessageBox.Show($"¿Está seguro de eliminar el proveedor {proveedorSeleccionado.Nombre}?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    proveedores.Remove(proveedorSeleccionado);
                    dgProveedores.ItemsSource = null;
                    dgProveedores.ItemsSource = proveedores;
                    MessageBox.Show("Proveedor eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
