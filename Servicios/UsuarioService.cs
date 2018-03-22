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
        public Usuario getById(int idUsuario, ref string resultado)
        {
            try
            {
                Usuario usuario = UsuarioDataProvider.getById(idUsuario);

                usuario.roles = RolDataProvider.getByIdUsuario(idUsuario);

                return usuario;
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
                return new Usuario();
            }
            
        }

        public Usuario getByUserNamePassword(string login_name, string password)
        {
            try
            {

                Usuario usuario = UsuarioDataProvider.getByUserNamePassword(login_name, password);

                if(usuario.id_usuario > 0)
                {
                    usuario.roles = RolDataProvider.getByIdUsuario(usuario.id_usuario);
                }

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

        public List<Usuario> getAll(ref string resultado)
        {
            try
            {
                return UsuarioDataProvider.getAll();
            }
            catch (Exception ex)
            {
                
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new List<Usuario>();  
            }
        }

        public void updateRoles(Usuario user, ref string resultado)
        {
            try
            {
                UsuarioDataProvider.updateRoles(user);
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
