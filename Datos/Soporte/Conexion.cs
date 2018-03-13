using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Datos
{
    public class Conexion
    {
        //private LoguedorService logService = new LoguedorService();
        private static SqlConnection conexion;

        public static bool probarConexion(string cadenaConexion)
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);

                conexion.Open();

                conexion.Close();

                return true;
            }
            catch (Exception ex)
            {
                //logService.loguear(ex.ToString(), "CheloComparer.Servicios", "SqlConexionService", "probarConexion");
                return false;
            }

        }

        private static bool conectar(string cadenaConexion)
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);

                conexion.Open();

                return true;
            }
            catch (Exception ex)
            {
                //TODO Loguear la excepcion
                return false;
                //return false;
                throw ex;
            }
        }

        private static void desconectar()
        {
            try
            {
                //SqlConnection conexion = new SqlConnection(cadenaConexion);
                if (conexion.State == ConnectionState.Open)
                {

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable executeQuery(string cadenaConexion, string comandoSql)
        {
            SqlCommand comando = new SqlCommand();
            DataTable result = new DataTable();

            try
            {

                if(conectar(cadenaConexion))
                { 
                    //comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = comandoSql;

                    result.Load(comando.ExecuteReader());

                    desconectar();
                }

                

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                desconectar();
            }


            return result;
        }

        public static DataTable executeQueryProc(string cadenaConexion, string storedProcedure, SqlParameter[] parametros)
        {
            SqlCommand comando = new SqlCommand();
            DataTable result = new DataTable();

            try
            {

                if (conectar(cadenaConexion))
                {
                    //comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = storedProcedure;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        comando.Parameters.Add(parametros[i]);
                    }

                    result.Load(comando.ExecuteReader());

                    desconectar();
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                desconectar();
            }


            return result;
        }

        public static void executeNonQuery(string cadenaConexion, string comandoSql)
        {
            SqlCommand comando = new SqlCommand();
            DataTable result = new DataTable();

            try
            {
                if(conectar(cadenaConexion))
                { 
                    //comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = comandoSql;

                    comando.ExecuteNonQuery();

                    desconectar();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                desconectar();
            }

        }

        public static void executeNonQueryProc(string cadenaConexion, string storedProcedure, SqlParameter[] parametros)
        {
            SqlCommand comando = new SqlCommand();
            DataTable result = new DataTable();

            try
            {
                if (conectar(cadenaConexion))
                {
                    //comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = storedProcedure;
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        comando.Parameters.Add(parametros[i]);
                    }

                    result.Load(comando.ExecuteReader());

                    desconectar();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                desconectar();
            }

        }
    }
}