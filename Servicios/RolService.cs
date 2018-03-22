using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class RolService
    {
        public List<Rol> getByIdUser(int id_usuario, ref string resultado)
        {
            try
            {
                return RolDataProvider.getByIdUsuario(id_usuario);
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, "Servicios", "RolService", "getByIdUser");
                resultado = ex.Message;
                return new List<Rol>();
            }
        }
    }
}
