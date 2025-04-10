using System;
using System.Collections.Generic;
using System.Configuration;
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
using Microsoft.Data.SqlClient;

namespace MediSync.Views
{
    public partial class ConsultaProductosView : UserControl
    {
        private List<Producto> productos;

        public ConsultaProductosView()
        {
            InitializeComponent();
            CargarProductos(); // Carga inicial sin filtro
        }

        /// <summary>
        /// Cargar lista de productos desde la base de datos con filtro opcional.
        /// </summary>
        private void CargarProductos(string filtro = null)
        {
            productos = new List<Producto>();
            string connectionString = ConfigurationManager.ConnectionStrings["MediSyncConection"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Nombre, Stock FROM Productos WHERE Nombre LIKE @filtro OR @filtro IS NULL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parámetro para el filtro, usando % para búsqueda parcial
                        command.Parameters.AddWithValue("@filtro", string.IsNullOrEmpty(filtro) ? (object)DBNull.Value : $"%{filtro}%");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new Producto
                                {
                                    Nombre = reader["Nombre"].ToString(),
                                    Stock = Convert.ToInt32(reader["Stock"])
                                });
                            }
                        }
                    }
                }
                dgProductos.ItemsSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Filtrar productos en la tabla según la búsqueda.
        /// </summary>
        private void TxtBuscarProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarProducto.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filtro))
            {
                CargarProductos(); // Recarga todos los productos si el filtro está vacío
            }
            else
            {
                CargarProductos(filtro); // Carga productos filtrados desde la BD
            }
        }
    }
}