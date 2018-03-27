using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Datos
{
    public class SesionDataProvider : GenericDataProvider
    {

        public static void insert(Sesion sesion)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("id_usuario", sesion.usuario_logueado.id_usuario);
                parametros[1] = new SqlParameter("token", sesion.token);
                parametros[2] = new SqlParameter("fecha_inicio", sesion.fecha_inicio);

                executeNonQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "sesionesInsert", parametros);

            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.ToString(), "Datos", "SesionDataProvider", "sesionesInsert");
                throw ex;
            }
        }

        public static void delete(Sesion sesion)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("id_usuario", sesion.usuario_logueado.id_usuario);

                executeNonQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "sesionesDelete", parametros);

            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.ToString(), "Datos", "SesionDataProvider", "sesionesInsert");
                throw ex;
            }
        }

        public static Sesion getByIdUser(int id_usuario)
        {
            try
            {
                Sesion sesionExistente = null;
                string storedProcedure = "sesionesGetByIdUser";
                SqlParameter[] parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("id_usuario", id_usuario);

                DataTable sesion = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), storedProcedure, parametros);

                if (sesion.Rows.Count > 0)
                    sesionExistente = Mapear(sesion.Rows[0]);

                return sesionExistente;
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.Message, "Datos", "SesionDataProvider", "getByIdUser");
                throw ex;
            }
        }

        public static Sesion getByToken(string token)
        {
            try
            {
                Sesion sesion = new Sesion();

                SqlParameter[] parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("token", token);

                DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "sesionesGetByToken", parametros);

                if (usuarioResult.Rows.Count > 0)
                { 
                    sesion = Mapear(usuarioResult.Rows[0]);
                    sesion.usuario_logueado = UsuarioDataProvider.getById(sesion.usuario_logueado.id_usuario);
                }

                return sesion;
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.ToString(), "Datos", "SesionDataProvider", "getByToken");
                throw ex;
            }
        }

        private static Sesion Mapear(DataRow dataRow)
        {
            Sesion sesion = new Sesion();
            int id_rol = 0;

            sesion.usuario_logueado.id_usuario = int.Parse(dataRow["id_usuario"].ToString());
            int.TryParse(dataRow["id_rol"].ToString(), out id_rol);

            sesion.rol_logueo.id_rol = id_rol;

            sesion.fecha_inicio = Convert.ToDateTime(dataRow["fecha_inicio"].ToString());
            sesion.token = dataRow["token"].ToString();

            return sesion;
        }
    }
}
