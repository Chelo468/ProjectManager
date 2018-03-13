using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string login_name { get; set; }
        public string password { get; set; }
        public DateTime fecha_alta { get; set; }
        public string email { get; set; }
        public string token_clave { get; set; }
        public bool habilitado { get; set; }
        public Rol[] roles { get; set; }
    }
}
