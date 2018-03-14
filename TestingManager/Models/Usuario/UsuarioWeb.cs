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
    }
}