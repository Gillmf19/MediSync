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
    public partial class GenerarReporteEmpleadoView : Window
    {
        public Reporte ReporteGenerado { get; private set; }

        public GenerarReporteEmpleadoView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para generar un reporte con los parámetros seleccionados.
        /// </summary>
        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipoReporte.SelectedItem == null || cbFormatoExportacion.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un tipo de reporte y un formato.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string tipoReporte = ((ComboBoxItem)cbTipoReporte.SelectedItem).Content.ToString();
            string formato = ((ComboBoxItem)cbFormatoExportacion.SelectedItem).Content.ToString();

            MessageBox.Show($"Generando {tipoReporte} en formato {formato}.",
                "Reporte Generado", MessageBoxButton.OK, MessageBoxImage.Information);

            // Se crea un objeto de reporte con la información seleccionada
            ReporteGenerado = new Reporte
            {
                Id = new Random().Next(100, 999),
                Nombre = tipoReporte,
                Fecha = DateTime.Now,
                Formato = formato,
                Ruta = $@"C:\Reportes\{tipoReporte}.{(formato == "PDF" ? "pdf" : "xlsx")}"
            };

            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Evento para cerrar la ventana sin generar un reporte.
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
