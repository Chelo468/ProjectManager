using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ConfiguracionDataProvider
    {
        public static string obtenerCadenaConexion()
        {
            //return @"Data Source=10.100.100.102\SqlServer2005;Initial Catalog=testingProyectos;Persist Security Info=True;User ID=sa;Password=123456;";
            return ConfigurationManager.AppSettings["sqlconn"];// @"Data Source=.\sqlexpress;Initial Catalog=testingProyectos;Integrated Security=True";
        }
    }
}
