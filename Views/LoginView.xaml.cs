using System;
using System.Collections.Generic;
using System.Configuration;
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
using Microsoft.Data.SqlClient;

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

            string conectionString = ConfigurationManager.ConnectionStrings["MediSyncConection"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(conectionString))
                {
                    connection.Open();
                    string query = "select nombre_usuario, contraseña from Usuarios where nombre_usuario = @usuario and contraseña = @contraseña";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@usuario", usuario);
                        command.Parameters.AddWithValue("@contraseña", contrasena);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Content = new AdminDashboardView();
                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




       
    }
}
