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

        private static Rol Mapear(DataRow lector)
        {
            Rol rol = new Rol();

            rol.id_rol = Convert.ToInt32(lector["id_rol"].ToString());
            rol.nombre = lector["nombre"].ToString();

            return rol;
        }
    }
}
