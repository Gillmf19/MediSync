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
    public partial class AgregarUsuarioView : Window
    {
        public Usuario UsuarioCreado { get; private set; }

        public AgregarUsuarioView(List<string> roles)
        {
            InitializeComponent();
            cbRol.ItemsSource = roles; // Cargar roles disponibles
        }

        /// <summary>
        /// Guardar el nuevo usuario con validaciones.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Password) ||
                string.IsNullOrWhiteSpace(txtConfirmarContrasena.Password) ||
                cbRol.SelectedItem == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtContrasena.Password != txtConfirmarContrasena.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UsuarioCreado = new Usuario
            {
                Id = new Random().Next(100, 999), // ID Simulado
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Contrasena = txtContrasena.Password, // En producción, deberías encriptarla
                Rol = cbRol.SelectedItem.ToString()
            };

            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Cancelar la operación.
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
