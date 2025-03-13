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
using MediSync.Models; // Asegurar que la clase Categoría esté definida

namespace MediSync.Views
{
    public partial class GestionCategoriasView : UserControl
    {
        private List<Categoria> listaCategorias;

        public GestionCategoriasView()
        {
            InitializeComponent();
            CargarCategorias();
        }

        /// <summary>
        /// Carga una lista de categorías simuladas.
        /// </summary>
        private void CargarCategorias()
        {
            listaCategorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Equipamiento" },
                new Categoria { Id = 2, Nombre = "Descartables" },
                new Categoria { Id = 3, Nombre = "Protección" }
            };

            dgCategorias.ItemsSource = listaCategorias;
        }

        /// <summary>
        /// Filtra la lista de categorías en tiempo real.
        /// </summary>
        private void TxtBuscarCategoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarCategoria.Text.ToLower();
            dgCategorias.ItemsSource = listaCategorias.Where(c => c.Nombre.ToLower().Contains(filtro)).ToList();
        }

        /// <summary>
        /// Muestra un cuadro de diálogo para agregar una nueva categoría.
        /// </summary>
        private void BtnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            string nuevaCategoria = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la nueva categoría:", "Agregar Categoría", "");
            if (!string.IsNullOrEmpty(nuevaCategoria))
            {
                int nuevoId = listaCategorias.Max(c => c.Id) + 1;
                listaCategorias.Add(new Categoria { Id = nuevoId, Nombre = nuevaCategoria });
                dgCategorias.ItemsSource = null;
                dgCategorias.ItemsSource = listaCategorias;
            }
        }

        /// <summary>
        /// Permite editar el nombre de una categoría seleccionada.
        /// </summary>
        private void BtnEditarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (dgCategorias.SelectedItem is Categoria categoriaSeleccionada)
            {
                string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox("Modificar nombre de categoría:", "Editar Categoría", categoriaSeleccionada.Nombre);
                if (!string.IsNullOrEmpty(nuevoNombre))
                {
                    categoriaSeleccionada.Nombre = nuevoNombre;
                    dgCategorias.ItemsSource = null;
                    dgCategorias.ItemsSource = listaCategorias;
                }
            }
            else
            {
                MessageBox.Show("Seleccione una categoría para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Elimina una categoría seleccionada con confirmación previa.
        /// </summary>
        private void BtnEliminarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (dgCategorias.SelectedItem is Categoria categoriaSeleccionada)
            {
                MessageBoxResult result = MessageBox.Show($"¿Está seguro de que desea eliminar la categoría '{categoriaSeleccionada.Nombre}'?",
                                                          "Confirmar Eliminación",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    listaCategorias.Remove(categoriaSeleccionada);
                    dgCategorias.ItemsSource = null;
                    dgCategorias.ItemsSource = listaCategorias;
                }
            }
            else
            {
                MessageBox.Show("Seleccione una categoría para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    /// <summary>
    /// Modelo de datos para las categorías.
    /// </summary>
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
