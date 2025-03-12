using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediSync.Controllers;
using MediSync.Views;

namespace MediSync
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationController.SetMainFrame(MainFrame); // Vincular el Frame con el controlador de navegación
            NavigationController.NavigateTo("Login"); // Cargar la vista de Login al inicio
        }
    }
}
