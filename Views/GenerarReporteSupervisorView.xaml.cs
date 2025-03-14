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
using MediSync.Views;

namespace MediSync.Views
{
    public partial class GenerarReporteSupervisorView : Window
    {
        public Reporte ReporteGenerado { get; private set; }

        public GenerarReporteSupervisorView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para generar un reporte con los parámetros seleccionados.
        /// </summary>
        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de selección de campos
            if (cbTipoReporte.SelectedItem == null || cbFormatoExportacion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione el tipo de reporte y el formato.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string tipoReporte = ((ComboBoxItem)cbTipoReporte.SelectedItem).Content.ToString();
            string formato = ((ComboBoxItem)cbFormatoExportacion.SelectedItem).Content.ToString();

            // Crear el objeto Reporte
            ReporteGenerado = new Reporte
            {
                Id = new Random().Next(100, 999), // ID aleatorio de ejemplo
                Nombre = tipoReporte,
                Fecha = DateTime.Now,
                Formato = formato,
                Ruta = $@"C:\Reportes\{tipoReporte}.{(formato == "PDF" ? "pdf" : "xlsx")}"
            };

            // Indicar que la generación fue exitosa y cerrar la ventana
            MessageBox.Show($"Reporte {tipoReporte} generado en formato {formato}.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
