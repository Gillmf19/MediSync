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

namespace MediSync.Views
{
    public partial class VerDetallesMovimientoSupervisorView : Window
    {
        public VerDetallesMovimientoSupervisorView(Movimientos movimiento)
        {
            InitializeComponent();
            CargarDatos(movimiento);
        }

        /// <summary>
        /// Cargar los datos del movimiento en los campos del formulario.
        /// </summary>
        private void CargarDatos(Movimientos movimiento)
        {
            txtProducto.Text = movimiento.Producto;
            txtCantidad.Text = movimiento.Cantidad.ToString();
            txtTipo.Text = movimiento.Tipo;
            txtFecha.Text = movimiento.Fecha.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Evento para cerrar la ventana.
        /// </summary>
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
