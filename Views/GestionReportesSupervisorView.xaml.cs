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
using Microsoft.Win32;
using System.Diagnostics;
using MediSync.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;

namespace MediSync.Views
{
    public partial class GestionReportesSupervisorView : UserControl
    {
        private List<Reporte> reportes;

        public GestionReportesSupervisorView()
        {
            InitializeComponent();
            CargarReportes();
        }

        /// <summary>
        /// Cargar lista simulada de reportes generados.
        /// </summary>
        private void CargarReportes()
        {
            reportes = new List<Reporte>
            {
                new Reporte { Id = 1, Nombre = "Inventario", Fecha = DateTime.Now.AddDays(-3), Formato = "PDF", Ruta = @"C:\Reportes\Inventario.pdf" },
                new Reporte { Id = 2, Nombre = "Movimientos", Fecha = DateTime.Now.AddDays(-2), Formato = "Excel", Ruta = @"C:\Reportes\Movimientos.xlsx" },
                new Reporte { Id = 3, Nombre = "Proveedores", Fecha = DateTime.Now.AddDays(-1), Formato = "PDF", Ruta = @"C:\Reportes\Proveedores.pdf" }
            };

            dgReportes.ItemsSource = reportes;
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
            GenerarReporteSupervisorView ventanaReporte = new GenerarReporteSupervisorView();
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
        /// Exportar el reporte seleccionado en PDF o Excel.
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
                        if (reporteSeleccionado.Formato == "PDF")
                        {
                            GenerarReportePDF(saveFileDialog.FileName, reporteSeleccionado);
                        }
                        else if (reporteSeleccionado.Formato == "Excel")
                        {
                            GenerarReporteExcel(saveFileDialog.FileName, reporteSeleccionado);
                        }

                        MessageBox.Show("Reporte exportado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al exportar el reporte.\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un reporte para descargar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Genera un archivo PDF con el contenido del reporte.
        /// </summary>
        private void GenerarReportePDF(string filePath, Reporte reporte)
        {
            Document documento = new Document();
            PdfWriter.GetInstance(documento, new FileStream(filePath, FileMode.Create));

            documento.Open();
            documento.Add(new iTextSharp.text.Paragraph($"Reporte: {reporte.Nombre}"));
            documento.Add(new iTextSharp.text.Paragraph($"Fecha de Generación: {reporte.Fecha:dd/MM/yyyy}"));
            documento.Add(new iTextSharp.text.Paragraph($"Formato: {reporte.Formato}"));
            documento.Add(new iTextSharp.text.Paragraph("\nContenido del reporte:\n"));
            documento.Add(new iTextSharp.text.Paragraph("Este es un reporte de muestra generado en PDF."));

            documento.Close();
        }

        /// <summary>
        /// Genera un archivo Excel con el contenido del reporte.
        /// </summary>
        private void GenerarReporteExcel(string filePath, Reporte reporte)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                worksheet.Cell(1, 1).Value = "Reporte";
                worksheet.Cell(1, 2).Value = reporte.Nombre;
                worksheet.Cell(2, 1).Value = "Fecha de Generación";
                worksheet.Cell(2, 2).Value = reporte.Fecha.ToString("dd/MM/yyyy");
                worksheet.Cell(3, 1).Value = "Formato";
                worksheet.Cell(3, 2).Value = reporte.Formato;
                worksheet.Cell(5, 1).Value = "Contenido del Reporte";
                worksheet.Cell(6, 1).Value = "Este es un reporte de muestra generado en Excel.";

                workbook.SaveAs(filePath);
            }
        }
    }

    /// <summary>
    /// Modelo de datos para un reporte.
    /// </summary>
    public class Reporte
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Formato { get; set; }
        public string Ruta { get; set; } // Ruta donde está guardado el archivo
    }
}
