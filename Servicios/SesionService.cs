using Datos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class SesionService
    {

        public Sesion getByToken(string token, ref string respuesta)
        {
            try
            {
                return SesionDataProvider.getByToken(token);
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
                return null;
            }
        }

        public void insert(Sesion sesion, ref string respuesta)
        {
            try
            {
                Sesion sesionExistente = SesionDataProvider.getByIdUser(sesion.usuario_logueado.id_usuario);

                if (sesionExistente != null)
                    SesionDataProvider.delete(sesionExistente);

                SesionDataProvider.insert(sesion);
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
        }
    }
}
