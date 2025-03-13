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
using System.IO;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using System.Reflection.Metadata;

namespace MediSync.Views
{
    public partial class GestionReportesView : UserControl
    {
        private List<Reporte> reportes;

        public GestionReportesView()
        {
            InitializeComponent();
            CargarReportes();
        }

        /// <summary>
        /// Carga una lista de reportes simulados al iniciar la vista.
        /// </summary>
        private void CargarReportes()
        {
            reportes = new List<Reporte>
            {
                new Reporte { Id = 1, Nombre = "Inventario", Fecha = DateTime.Now.AddDays(-3), Formato = "PDF" },
                new Reporte { Id = 2, Nombre = "Movimientos", Fecha = DateTime.Now.AddDays(-2), Formato = "Excel" },
                new Reporte { Id = 3, Nombre = "Proveedores", Fecha = DateTime.Now.AddDays(-1), Formato = "PDF" }
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
        /// Evento que abre la ventana de generación de reportes.
        /// </summary>
        private void BtnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            GenerarReporteView ventanaReporte = new GenerarReporteView();
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
                    // Verificar si la ruta del archivo es válida
                    if (!string.IsNullOrEmpty(reporteSeleccionado.Ruta) && File.Exists(reporteSeleccionado.Ruta))
                    {
                        // Abrir el archivo con la aplicación predeterminada del sistema
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = reporteSeleccionado.Ruta,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("El archivo del reporte no se encontró en la ubicación especificada.",
                                        "Archivo no encontrado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el reporte.\nError: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un reporte para ver.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Descarga el reporte en formato PDF o Excel.
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
        /// <summary>
        /// Genera un archivo PDF con el contenido del reporte.
        /// </summary>
        private void GenerarReportePDF(string filePath, Reporte reporte)
        {
            // Se usa iTextSharp.text.Document explícitamente
            iTextSharp.text.Document documento = new iTextSharp.text.Document();

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

        /// <summary>
        /// Elimina un reporte seleccionado de la lista.
        /// </summary>
        private void BtnEliminarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (dgReportes.SelectedItem is Reporte reporteSeleccionado)
            {
                var resultado = MessageBox.Show($"¿Está seguro de eliminar el reporte {reporteSeleccionado.Nombre}?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    reportes.Remove(reporteSeleccionado);
                    dgReportes.ItemsSource = null;
                    dgReportes.ItemsSource = reportes;
                    MessageBox.Show("Reporte eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un reporte para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

