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
    public partial class ConsultaProductosEmpleadoView : UserControl
    {
        private List<Producto> productos;

        public ConsultaProductosEmpleadoView()
        {
            InitializeComponent();
            CargarProductos();
        }

        /// <summary>
        /// Cargar lista simulada de productos en la tabla.
        /// </summary>
        private void CargarProductos()
        {
            productos = new List<Producto>
            {
                new Producto { Nombre = "Guantes Estériles", Stock = 50 },
                new Producto { Nombre = "Jeringas 5ml", Stock = 30 },
                new Producto { Nombre = "Batas Quirúrgicas", Stock = 20 },
                new Producto { Nombre = "Mascarillas N95", Stock = 60 },
                new Producto { Nombre = "Solución Salina", Stock = 40 }
            };

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
                : productos.Where(p => p.Nombre.ToLower().Contains(filtro)).ToList();
        }
    }
}
