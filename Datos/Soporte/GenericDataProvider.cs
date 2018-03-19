using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class GenericDataProvider
    {
        public static DataTable executeQueryProc(string cadenaConexion, string storedProcedure, SqlParameter[] parametros)
        {
            try
            {
                return Conexion.executeQueryProc(cadenaConexion, storedProcedure, parametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static void executeNonQueryProc(string cadenaConexion, string storedProcedure, SqlParameter[] parametros)
        {
            try
            {
                Conexion.executeNonQueryProc(cadenaConexion, storedProcedure, parametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
