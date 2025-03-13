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
using System;
using System.Collections.Generic;
using System.Windows;
using MediSync.Models;

namespace MediSync.Views
{
    public partial class EditarUsuarioView : Window
    {
        public Usuario UsuarioEditado { get; private set; }

        public EditarUsuarioView(Usuario usuario, List<string> roles)
        {
            InitializeComponent();
            cbRol.ItemsSource = roles; // Cargar roles disponibles
            CargarDatos(usuario);
        }

        /// <summary>
        /// Cargar los datos actuales del usuario en los campos del formulario.
        /// </summary>
        private void CargarDatos(Usuario usuario)
        {
            txtNombre.Text = usuario.Nombre;
            txtCorreo.Text = usuario.Correo; // Correo no editable
            cbRol.SelectedItem = usuario.Rol;
        }

        /// <summary>
        /// Guardar los cambios realizados al usuario.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cbRol.SelectedItem == null)
            {
                MessageBox.Show("El nombre y el rol son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtContrasena.Password) && txtContrasena.Password != txtConfirmarContrasena.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UsuarioEditado = new Usuario
            {
                Id = new Random().Next(100, 999), // ID Simulado
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text, // No se permite modificar
                Contrasena = string.IsNullOrWhiteSpace(txtContrasena.Password) ? null : txtContrasena.Password,
                Rol = cbRol.SelectedItem.ToString()
            };

            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Cancelar la operación de edición.
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
