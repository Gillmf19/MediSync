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
    public partial class AgregarProveedorView : Window
    {
        public Proveedor NuevoProveedor { get; private set; }

        public AgregarProveedorView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para guardar un nuevo proveedor.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtContacto.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que el teléfono sea un número válido
            if (!long.TryParse(txtTelefono.Text, out _))
            {
                MessageBox.Show("El teléfono debe contener solo números.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear un nuevo objeto Proveedor con los datos ingresados
            NuevoProveedor = new Proveedor
            {
                Id = new Random().Next(1000, 9999), // ID Simulado
                Nombre = txtNombre.Text,
                Contacto = txtContacto.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text
            };

            // Indicar que la operación fue exitosa y cerrar la ventana
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Evento para cancelar la operación.
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
