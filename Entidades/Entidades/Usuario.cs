﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public Usuario()
        {
            roles = new List<Rol>();
        }

        public int id_usuario { get; set; }
        public string login_name { get; set; }
        public string password { get; set; }
        public DateTime fecha_alta { get; set; }
        public DateTime fecha_ultima_modif { get; set; }
        public DateTime fecha_baja { get; set; }
        public string email { get; set; }
        public string token_clave { get; set; }
        public bool habilitado { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public List<Rol> roles { get; set; }
    }
}
