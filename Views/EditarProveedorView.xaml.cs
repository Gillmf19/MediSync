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
using System;
using System.Windows;
using MediSync.Models;

namespace MediSync.Views
{
    public partial class EditarProveedorView : Window
    {
        public Proveedor ProveedorEditado { get; private set; }

        public EditarProveedorView(Proveedor proveedor)
        {
            InitializeComponent();
            CargarDatos(proveedor);
        }

        /// <summary>
        /// Cargar los datos del proveedor en los campos del formulario.
        /// </summary>
        private void CargarDatos(Proveedor proveedor)
        {
            txtNombre.Text = proveedor.Nombre;
            txtContacto.Text = proveedor.Contacto;
            txtCorreo.Text = proveedor.Correo;
            txtTelefono.Text = proveedor.Telefono;
            txtDireccion.Text = proveedor.Direccion;
        }

        /// <summary>
        /// Evento para guardar los cambios en el proveedor.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de campos vacíos
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

            // Guardar los cambios en un nuevo objeto Proveedor
            ProveedorEditado = new Proveedor
            {
                Id = 0, // Se mantiene el ID original
                Nombre = txtNombre.Text,
                Contacto = txtContacto.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text
            };

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
