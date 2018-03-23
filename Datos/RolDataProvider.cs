using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Datos
{
    public class RolDataProvider : GenericDataProvider
    {

        private static Rol Mapear(DataRow lector)
        {
            Rol rol = new Rol();

            rol.id_rol = Convert.ToInt32(lector["id_rol"].ToString());
            rol.nombre = lector["nombre"].ToString();

            return rol;
        }

        public static List<Rol> getByIdUsuario(int id_usuario)
        {
            List<Rol> roles = new List<Rol>();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("id_usuario", id_usuario);

            DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "rolesGetByIdUsuario", parametros);

            for (int i = 0; i < usuarioResult.Rows.Count; i++)
            {
                Rol rol = new Rol();
                rol = Mapear(usuarioResult.Rows[i]);

                roles.Add(rol);
            }
                

            return roles;
        }

        

        internal static Rol getById(int id_rol)
        {
            Rol rol = new Rol();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("id_rol", id_rol);

            DataTable rolResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "rolesGetById", parametros);

           if(rolResult.Rows.Count > 0)
            {
                rol = Mapear(rolResult.Rows[0]);
            }


           return rol;
        }
    }
}
