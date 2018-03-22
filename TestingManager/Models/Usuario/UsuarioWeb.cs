using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingManager.Models
{
    public class UsuarioWeb
    {
        public int id_usuario { get; set; }
        public string login_name { get; set; }
        public string email { get; set; }
        public string token_clave { get; set; }
        public bool habilitado { get; set; }
        public string tokenSession { get; set; }
        public string fecha_alta { get; set; }
        public string fecha_baja { get; set; }
        public string fecha_ultima_modif { get; set; }

        public List<Rol> roles { get; set; }
    }
}