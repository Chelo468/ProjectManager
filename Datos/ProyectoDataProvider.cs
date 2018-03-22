using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ProyectoDataProvider : GenericDataProvider
    {
        public static int crear(Proyecto proyecto)
        {
            try
            {
                int idProyecto = -1;
                SqlParameter[] parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("nombre", proyecto.nombre);
                parametros[1] = new SqlParameter("descripcion", proyecto.descripcion);
                parametros[2] = new SqlParameter("urlTesting", proyecto.urlTesting);
                parametros[3] = new SqlParameter("urlProduccion", proyecto.urlProduccion);
                parametros[4] = new SqlParameter("id_usuario_creador", proyecto.usuario_creador.id_usuario);
                parametros[5] = new SqlParameter("fecha_alta", proyecto.fecha_alta);

                DataTable proyectoResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectosInsert", parametros);

                if (proyectoResult.Rows.Count > 0)
                {
                    idProyecto = Convert.ToInt32(proyectoResult.Rows[0][0].ToString());
                }

                return idProyecto;
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, "Datos", "ProyectoDataProvider", "crear");
                throw ex;
            }
            
        }

        public static void actualizar(Proyecto proyecto)
        {
            try
            {
                int idProyecto = -1;
                SqlParameter[] parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("nombre", proyecto.nombre);
                parametros[1] = new SqlParameter("descripcion", proyecto.descripcion);
                parametros[2] = new SqlParameter("urlTesting", proyecto.urlTesting);
                parametros[3] = new SqlParameter("urlProduccion", proyecto.urlProduccion);
                parametros[4] = new SqlParameter("id_proyecto", proyecto.id_proyecto);
                parametros[5] = new SqlParameter("fecha_ultima_modif", proyecto.fecha_ultima_modif);

                executeNonQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectosUpdate", parametros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Proyecto> getByIdUser(int id_usuario)
        {
            List<Proyecto> proyectos = new List<Proyecto>();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("idUser", id_usuario);

            DataTable proyectoResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectosGetByIdUser", parametros);
            for (int i = 0; i < proyectoResult.Rows.Count; i++)
            {
                Proyecto proyecto = new Proyecto();
                proyecto = Mapear(proyectoResult.Rows[i]);
                proyectos.Add(proyecto);
            }


            return proyectos;
        }

        private static Proyecto Mapear(DataRow dataRow)
        {
            Proyecto proyecto = new Proyecto();

            proyecto.nombre = dataRow["nombre"].ToString();
            proyecto.descripcion = dataRow["descripcion"].ToString();
            proyecto.fecha_alta = Convert.ToDateTime(dataRow["fecha_alta"].ToString());
            try
            {
                proyecto.fecha_baja = Convert.ToDateTime(dataRow["fecha_baja"].ToString());
            }
            catch (Exception){ proyecto.fecha_baja = null; }
            
            try
            {
                proyecto.fecha_ultima_modif = Convert.ToDateTime(dataRow["fecha_ultima_modif"].ToString());
            }
            catch (Exception) { }

            proyecto.id_proyecto = Convert.ToInt32(dataRow["id_proyecto"].ToString());
            proyecto.usuario_creador.id_usuario = Convert.ToInt32(dataRow["id_usuario_creador"].ToString());
            proyecto.urlProduccion = dataRow["urlProduccion"].ToString();
            proyecto.urlTesting = dataRow["urlTesting"].ToString();
            return proyecto;
        }



        public static Proyecto getById(int idProyecto, int idUsuario)
        {
            Proyecto proyecto = new Proyecto();

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("idUser", idUsuario);
            parametros[1] = new SqlParameter("idProyecto", idProyecto);

            DataTable proyectoResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectosGetByIdProyectoIdUser", parametros);
            if(proyectoResult.Rows.Count > 0)
            {
                proyecto = Mapear(proyectoResult.Rows[0]);
            }


            return proyecto;
        }

        public static void eliminar(Proyecto proyecto)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("id_proyecto", proyecto.id_proyecto);
                parametros[1] = new SqlParameter("fecha_baja", proyecto.fecha_baja);

                executeNonQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectosDelete", parametros);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
