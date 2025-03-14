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
    public partial class AgregarProductoSupervisorView : Window
    {
        public Producto ProductoNuevo { get; private set; }

        public AgregarProductoSupervisorView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda el nuevo producto después de validar los campos.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cbCategoria.SelectedItem == null || !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Por favor, complete todos los campos correctamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProductoNuevo = new Producto
            {
                Id = new Random().Next(100, 999), // Simulación de un ID
                Nombre = txtNombre.Text,
                Categoria = (cbCategoria.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Stock = stock
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
