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

using MediSync.Controllers;

namespace MediSync.Views
{
    public partial class RecuperarContraseñaView : UserControl
    {
        public RecuperarContraseñaView()
        {
            InitializeComponent();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            string correo = txtCorreo.Text;

            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Se enviaron instrucciones a: {correo}", "Recuperación de Contraseña", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TxtVolver_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationController.NavigateTo("Login");
        }
    }
}
