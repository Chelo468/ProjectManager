using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Entidades
{
    public class Sesion
    {

        public Sesion()
        {
            usuario_logueado = new Usuario();
            rol_logueo = new Rol();
        }

        public Usuario usuario_logueado { get; set; }
        public string token { get; set; }
        public DateTime fecha_inicio { get; set; }
        public Rol rol_logueo { get; set; }
    }
}
