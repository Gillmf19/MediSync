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
    public partial class GenerarReporteView : Window
    {
        public Reporte ReporteGenerado { get; private set; }
        public string TipoReporte { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public string FormatoExportacion { get; private set; }

        public GenerarReporteView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para generar un reporte con los parámetros seleccionados.
        /// </summary>
        private void BtnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            // Validación de los campos obligatorios
            if (cbTipoReporte.SelectedItem == null || cbFormatoExportacion.SelectedItem == null ||
                dpFechaInicio.SelectedDate == null || dpFechaFin.SelectedDate == null)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtener los valores seleccionados
            TipoReporte = ((ComboBoxItem)cbTipoReporte.SelectedItem).Content.ToString();
            FechaInicio = dpFechaInicio.SelectedDate.Value;
            FechaFin = dpFechaFin.SelectedDate.Value;
            FormatoExportacion = ((ComboBoxItem)cbFormatoExportacion.SelectedItem).Content.ToString();

            // Validación de rango de fechas
            if (FechaInicio > FechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 📌 Se asigna correctamente el ReporteGenerado
            ReporteGenerado = new Reporte
            {
                Id = new Random().Next(100, 999), // Genera un ID único simulado
                Nombre = $"{TipoReporte} ({FechaInicio:dd/MM/yyyy} - {FechaFin:dd/MM/yyyy})",
                Fecha = DateTime.Now,
                Formato = FormatoExportacion
            };

            // Confirmación de generación del reporte
            MessageBox.Show($"Generando {TipoReporte} desde {FechaInicio:dd/MM/yyyy} hasta {FechaFin:dd/MM/yyyy} en formato {FormatoExportacion}.",
                "Reporte Generado", MessageBoxButton.OK, MessageBoxImage.Information);

            // Cerrar ventana y confirmar éxito
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
