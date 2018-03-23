using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Entidades
{
    public class Proyecto_Usuario
    {
        public Proyecto_Usuario()
        {
            proyecto = new Proyecto();
            usuario = new Usuario();
            rol = new Rol();
        }

        public Proyecto proyecto { get; set; }
        public Usuario usuario { get; set; }
        public Rol rol { get; set; }
        public DateTime fecha_desde { get; set; }
    }
}
