using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ProyectoService
    {
        public Proyecto getByIdUser(int id_usuario)
        {
            Proyecto usuario = ProyectoDataProvider.getByIdUser(id_usuario);

            return usuario;
        }
    }
}
