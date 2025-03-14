using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Controls;
using MediSync.Views;

namespace MediSync.Controllers
{
    public static class NavigationController
    {
        private static Frame _mainFrame;

        public static void SetMainFrame(Frame frame)
        {
            _mainFrame = frame;
        }

        public static void NavigateTo(string viewName)
        {
            if (_mainFrame == null)
                throw new Exception("El Frame principal no está inicializado.");

            switch (viewName)
            {
                case "Login":
                    _mainFrame.Navigate(new LoginView());
                    break;
                case "AdminDashboardView":
                    _mainFrame.Navigate(new AdminDashboardView());
                    break;
                case "SupervisorDashboardView":
                    _mainFrame.Navigate(new SupervisorDashboardView());
                    break;
                case "EmpleadoDashboardView":
                    _mainFrame.Navigate(new EmpleadoDashboardView());
                    break;
                case "GestionProductosView":
                    _mainFrame.Navigate(new GestionProductosView());
                    break;
                case "ConsultaProductosView": 
                    _mainFrame.Navigate(new ConsultaProductosView());
                    break;
                case "GestionCategoriasView":
                    _mainFrame.Navigate(new GestionCategoriasView());
                    break;
                case "GestionProductosSupervisorView":
                    _mainFrame.Navigate(new GestionProductosSupervisorView());
                    break;
                case "GestionMovimientosSupervisor":
                    _mainFrame.Navigate(new GestionMovimientosSupervisorView());
                    break;
                case "GestionReportesSupervisorView":
                    _mainFrame.Navigate(new GestionReportesSupervisorView());
                    break;
                case "ConsultaProductosEmpleadoView": 
                    _mainFrame.Navigate(new ConsultaProductosEmpleadoView());
                    break;
                case "GestionReportesEmpleadoView":
                    _mainFrame.Navigate(new GestionReportesEmpleadoView());
                    break;
                case "GestionMovimientosEmpleadoView":
                    _mainFrame.Navigate(new GestionMovimientosEmpleadoView());
                    break;


                default:
                    throw new Exception("Vista no encontrada.");
            }
        }
    }
}
