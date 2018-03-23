using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Proyecto_UsuarioDataProvider : GenericDataProvider
    {
        public static List<Proyecto_Usuario> getUsersByIdProyecto(int id_proyecto)
        {
            try
            {
                List<Proyecto_Usuario> usuarios = new List<Proyecto_Usuario>();

                SqlParameter[] parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("idProyecto", id_proyecto);

                DataTable proyectoResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectoGetUsuarioById", parametros);

                for (int i = 0; i < proyectoResult.Rows.Count; i++)
                {
                    Proyecto_Usuario usuario = Mapear(proyectoResult.Rows[i]);
                    usuarios.Add(usuario);
                }


                return usuarios;

            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, "Datos", "Proyecto_UsuarioDataProvider", "getUsersByIdProyecto");
                throw ex;
            }
        }

        private static Proyecto_Usuario Mapear(DataRow lector)
        {
            Proyecto_Usuario usuario = new Proyecto_Usuario();

            usuario.proyecto = ProyectoDataProvider.getById(Convert.ToInt32(lector["id_proyecto"].ToString()));
            usuario.usuario = UsuarioDataProvider.getById(Convert.ToInt32(lector["id_usuario"].ToString()));
            usuario.rol = RolDataProvider.getById(Convert.ToInt32(lector["id_proyecto"].ToString()));
            usuario.fecha_desde = Convert.ToDateTime(lector["fecha_desde"].ToString());

            return usuario;
        }
    }
}
