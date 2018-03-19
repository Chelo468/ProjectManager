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
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                respuesta = ex.Message;
            }
        }

        public void delete(Sesion sesionActual)
        {
            try
            {
                SesionDataProvider.delete(sesionActual);
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
    }
}
