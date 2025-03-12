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
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Password;

            string rol = LoginController.AutenticarUsuario(usuario, contrasena);

            if (rol != null)
            {
                MessageBox.Show($"Bienvenido {usuario} - Rol: {rol}", "Acceso Permitido", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationController.NavigateTo(rol); // Redirigir según el rol
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Inténtalo de nuevo.", "Error de Autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
