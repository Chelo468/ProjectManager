using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Datos
{
    public static class LogueadorService
    {
        public static void loguear(string message, string nameSpace, string clase, string metodo)
        {
            string pathApp = ConfigurationManager.AppSettings["logPath"];
            string pathArchivo = pathApp + "/log" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString() + ".txt";

            if (!Directory.Exists(pathApp))
            {
                Directory.CreateDirectory(pathApp);
            }

            if(Convert.ToBoolean(ConfigurationManager.AppSettings["enableDebugLog"].ToString()))
            { 
                var strBuider = new StringBuilder();
                strBuider.Append("FECHA: ");
                strBuider.Append(DateTime.Now.ToShortDateString() + " : " + DateTime.Now.ToShortTimeString() + ", " + Environment.NewLine);
                strBuider.Append("NIVEL EN DEPLOY: " + nameSpace + ". " + Environment.NewLine);
                strBuider.Append("CLASE GENERADORA: " + clase + ". " + Environment.NewLine);
                strBuider.Append("METODO: " + metodo + ". " + Environment.NewLine);
                strBuider.Append("Excepcion completa: " + Environment.NewLine + message + Environment.NewLine);
                strBuider.Append("-------------------------------------------------------------------- " + Environment.NewLine + Environment.NewLine);

                var sw = new StreamWriter(pathArchivo, true, Encoding.UTF8);
                sw.WriteLine(strBuider.ToString()); //message);
                sw.Close();
            }
        }
    }
}
