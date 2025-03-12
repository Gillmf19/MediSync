using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MediSync.Controllers
{
    public static class LoginController
    {
        // Simulación de usuarios y roles (sin base de datos aún)
        private static readonly Dictionary<string, (string Password, string Role)> usuarios = new()
        {
            { "admin", ("1234", "AdminDashboardView") },
            { "supervisor", ("1234", "SupervisorDashboardView") },
            { "empleado", ("1234", "EmpleadoDashboardView") }
        };

        public static string AutenticarUsuario(string usuario, string contrasena)
        {
            if (usuarios.ContainsKey(usuario) && usuarios[usuario].Password == contrasena)
            {
                return usuarios[usuario].Role; // Retorna la vista según el rol
            }
            return null;
        }
    }
}
