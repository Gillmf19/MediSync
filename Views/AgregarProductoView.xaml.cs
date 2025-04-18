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
using System.Windows.Shapes;
using System;
using System.Windows;
using System.Windows.Controls;
using MediSync.Models; // Asegurar que la referencia al modelo está incluida

namespace MediSync.Views
{
    public partial class AgregarProductoView : Window
    {
        public Producto NuevoProducto { get; private set; }

        public AgregarProductoView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para guardar un nuevo producto.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                cbCategoria.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtProveedor.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que el stock sea un número válido
            if (!int.TryParse(txtCantidad.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número válido y mayor o igual a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear el nuevo producto
            NuevoProducto = new Producto
            {
                Id = new Random().Next(100, 999), // ID Simulado
                Nombre = txtNombre.Text,
                Stock = stock,
                Categoria = ((ComboBoxItem)cbCategoria.SelectedItem).Content.ToString(),
                Proveedor = txtProveedor.Text
            };

            this.DialogResult = true; // Indicar que la operación fue exitosa
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
