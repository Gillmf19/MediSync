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
using System.Collections.Generic;
using MediSync.Models;

namespace MediSync.Views
{
    public partial class GestionUsuariosRolesView : UserControl
    {
        private List<Usuario> usuarios;
        private List<string> rolesDisponibles = new List<string> { "Administrador", "Supervisor", "Empleado" };

        public GestionUsuariosRolesView()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        /// <summary>
        /// Cargar lista de usuarios simulada.
        /// </summary>
        private void CargarUsuarios()
        {
            usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nombre = "Juan Pérez", Correo = "juan.perez@email.com", Rol = "Administrador" },
                new Usuario { Id = 2, Nombre = "Ana López", Correo = "ana.lopez@email.com", Rol = "Supervisor" },
                new Usuario { Id = 3, Nombre = "Carlos Gómez", Correo = "carlos.gomez@email.com", Rol = "Empleado" }
            };

            dgUsuarios.ItemsSource = usuarios;
        }

        /// <summary>
        /// Filtrar usuarios según la búsqueda.
        /// </summary>
        private void TxtBuscarUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarUsuario.Text.ToLower();
            dgUsuarios.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? usuarios
                : usuarios.Where(u => u.Nombre.ToLower().Contains(filtro) || u.Correo.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Evento para agregar un nuevo usuario.
        /// </summary>
        private void BtnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            AgregarUsuarioView agregarUsuarioView = new AgregarUsuarioView(rolesDisponibles);
            if (agregarUsuarioView.ShowDialog() == true)
            {
                Usuario nuevoUsuario = agregarUsuarioView.UsuarioCreado;
                if (nuevoUsuario != null)
                {
                    usuarios.Add(nuevoUsuario);
                    dgUsuarios.ItemsSource = null;
                    dgUsuarios.ItemsSource = usuarios;
                    MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Evento para editar un usuario seleccionado.
        /// </summary>
        private void BtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuarioSeleccionado)
            {
                EditarUsuarioView editarUsuarioView = new EditarUsuarioView(usuarioSeleccionado, rolesDisponibles);
                if (editarUsuarioView.ShowDialog() == true)
                {
                    dgUsuarios.ItemsSource = null;
                    dgUsuarios.ItemsSource = usuarios;
                    MessageBox.Show("Usuario editado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Evento para eliminar un usuario con confirmación.
        /// </summary>
        private void BtnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuarioSeleccionado)
            {
                var resultado = MessageBox.Show($"¿Está seguro de eliminar al usuario {usuarioSeleccionado.Nombre}?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    usuarios.Remove(usuarioSeleccionado);
                    dgUsuarios.ItemsSource = null;
                    dgUsuarios.ItemsSource = usuarios;
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

