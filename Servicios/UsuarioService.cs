using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UsuarioService
    {
        public Usuario getById(int idUsuario)
        {
            Usuario usuario = UsuarioDataProvider.getById(idUsuario);

            return usuario;
        }

        public Usuario getByUserNamePassword(string login_name, string password)
        {
            try
            {

                Usuario usuario = UsuarioDataProvider.getByUserNamePassword(login_name, password);

                return usuario;
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        public int crear(Usuario nuevoUsuario, ref string respuesta)
        {
            try
            {
                return UsuarioDataProvider.crear(nuevoUsuario);
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;                
            }
        }
    }
}
