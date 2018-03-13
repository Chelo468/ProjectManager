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
        public static Proyecto getByIdUser(int id_usuario)
        {
            Proyecto usuario = new Proyecto();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("idUser", id_usuario);

            DataTable proyectoResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "proyectosGetByIdUser", parametros);

            if (proyectoResult.Rows.Count > 0)
            { 
                usuario = Mapear(proyectoResult.Rows[0]);
                return usuario;
            }
            return null;
        }

        private static Proyecto Mapear(DataRow dataRow)
        {
            Proyecto proyecto = new Proyecto();

            proyecto.nombre = dataRow["nombre"].ToString();

            return proyecto;
        }
    }
}
