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
using System.Diagnostics;
using MediSync.Models;
using System.IO;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;


namespace MediSync.Views
{
    public partial class GestionReportesEmpleadoView : UserControl
    {
        private List<Reporte> reportes;

        public GestionReportesEmpleadoView()
        {
            InitializeComponent();
            CargarReportes();
        }

        /// <summary>
        /// Carga una lista simulada de reportes generados.
        /// </summary>
        private void CargarReportes()
        {
            reportes = new List<Reporte>
            {
                new Reporte { Id = 1, Nombre = "Reporte de Stock", Fecha = DateTime.Now.AddDays(-3), Formato = "PDF", Ruta = @"C:\Reportes\ReporteStock.pdf" },
                new Reporte { Id = 2, Nombre = "Reporte de Movimientos", Fecha = DateTime.Now.AddDays(-2), Formato = "Excel", Ruta = @"C:\Reportes\ReporteMovimientos.xlsx" }
            };

            dgReportes.ItemsSource = reportes;
        }

        /// <summary>
        /// Descarga el reporte seleccionado.
        /// </summary>
        private void BtnDescargarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (dgReportes.SelectedItem is Reporte reporteSeleccionado)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FileName = reporteSeleccionado.Nombre,
                    Filter = reporteSeleccionado.Formato == "PDF" ? "Archivos PDF (*.pdf)|*.pdf" : "Archivos Excel (*.xlsx)|*.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // Copiar el archivo desde la ruta de origen a la ubicación elegida por el usuario
                        File.Copy(reporteSeleccionado.Ruta, saveFileDialog.FileName, true);
                        MessageBox.Show("Reporte descargado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al descargar el reporte.\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un reporte para descargar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Abre el reporte seleccionado con el visor predeterminado del sistema.
        /// </summary>
        private void BtnVerReporte_Click(object sender, RoutedEventArgs e)
        {
            if (dgReportes.SelectedItem is Reporte reporteSeleccionado)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = reporteSeleccionado.Ruta,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo abrir el reporte.\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un reporte para ver.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Filtra los reportes según el texto ingresado en la barra de búsqueda.
        /// </summary>
        private void TxtBuscarReporte_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarReporte.Text.ToLower();
            dgReportes.ItemsSource = string.IsNullOrWhiteSpace(filtro)
                ? reportes
                : reportes.Where(r => r.Nombre.ToLower().Contains(filtro)).ToList();
        }
        /// <summary>
        /// Evento para abrir la ventana de generación de reportes.
        /// </summary>
        private void BtnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            GenerarReporteEmpleadoView ventanaReporte = new GenerarReporteEmpleadoView();
            if (ventanaReporte.ShowDialog() == true)
            {
                Reporte nuevoReporte = ventanaReporte.ReporteGenerado;
                if (nuevoReporte != null)
                {
                    reportes.Add(nuevoReporte);
                    dgReportes.ItemsSource = null;
                    dgReportes.ItemsSource = reportes;
                    MessageBox.Show("Reporte generado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


    }


}
