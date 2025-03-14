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
    public partial class GestionMovimientosSupervisorView : UserControl
    {
        private List<Movimientos> movimientos;

        public GestionMovimientosSupervisorView()
        {
            InitializeComponent();
            CargarMovimientos();
        }

        /// <summary>
        /// Carga una lista de movimientos simulados.
        /// </summary>
        private void CargarMovimientos()
        {
            movimientos = new List<Movimientos>
            {
                new Movimientos { Id = 1, Producto = "Guantes Estériles", Cantidad = 50, Tipo = "Entrada", Fecha = DateTime.Now.AddDays(-2) },
                new Movimientos { Id = 2, Producto = "Jeringas 5ml", Cantidad = 30, Tipo = "Salida", Fecha = DateTime.Now.AddDays(-1) },
                new Movimientos { Id = 3, Producto = "Batas Quirúrgicas", Cantidad = 20, Tipo = "Entrada", Fecha = DateTime.Now }
            };
            dgMovimientos.ItemsSource = movimientos;
        }

        /// <summary>
        /// Filtra la lista de movimientos en función del texto ingresado en la barra de búsqueda.
        /// </summary>
        private void TxtBuscarMovimiento_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarMovimiento.Text.ToLower();
            dgMovimientos.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? movimientos
                : movimientos.Where(m => m.Producto.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Evento que se dispara cuando se selecciona un elemento en el DataGrid de movimientos.
        /// </summary>
        private void DgMovimientos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMovimientos.SelectedItem != null)
            {
                // Aquí puedes habilitar botones o mostrar información adicional si es necesario
                MessageBox.Show("Movimiento seleccionado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Abre el formulario para registrar un nuevo movimiento.
        /// </summary>
        private void BtnRegistrarMovimiento_Click(object sender, RoutedEventArgs e)
        {
            RegistrarMovimientoSupervisor registrarView = new RegistrarMovimientoSupervisor();
            if (registrarView.ShowDialog() == true) // ✅ Espera confirmación de guardado
            {
                Movimientos nuevoMovimiento = registrarView.MovimientoRegistrado;
                if (nuevoMovimiento != null)
                {
                    movimientos.Add(nuevoMovimiento);
                    dgMovimientos.ItemsSource = null;  // Refrescar DataGrid
                    dgMovimientos.ItemsSource = movimientos;
                }
            }
        }
    }

    /// <summary>
    /// Modelo de datos para un movimiento en el inventario.
    /// </summary>
    public class Movimientos
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
