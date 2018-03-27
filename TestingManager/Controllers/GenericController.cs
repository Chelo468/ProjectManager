using Entidades.Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TestingManager.Controllers
{
    public class GenericController : Controller
    {
        //
        // GET: /Generic/

        public Sesion getSesionByToken(string token)
        {
            SesionService serviceSesion = new SesionService();

            string respuesta = "";

            Sesion sesion = serviceSesion.getByToken(token,ref respuesta);

            if(!string.IsNullOrEmpty(respuesta) || sesion == null)
            {
                return null;
            }
            else
            {
                return sesion;
            }
        }

        public string crearSesion(int id_usuario)
        {
            Sesion sesion = new Sesion();

            sesion.usuario_logueado.id_usuario = id_usuario;
            //TODO: Generar Random
            sesion.token = generarTokenAleatorio();
            sesion.fecha_inicio = DateTime.Now;

            SesionService serviceSesion = new SesionService();

            string respuesta = "";

            serviceSesion.insert(sesion, ref respuesta);

            return string.IsNullOrEmpty(respuesta) ? sesion.token : "";

        }

        private string generarTokenAleatorio()
        {
            int longitud = 7;
            const string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder token = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                token.Append(alfabeto[indice]);
            }

            return token.ToString();
        }

        public int validarToken(HttpRequestBase request,ref Sesion sesionActual, ref string resultado)
        {
            string token = request.Headers["X-AUTH-TOKEN"];

            if (token != null)
            {
                sesionActual = getSesionByToken(token);

                if (sesionActual != null && sesionActual.usuario_logueado.id_usuario > 0)
                {
                    return 1;
                }
                else
                {
                    resultado = "Token no válido";
                    return -1;
                }
            }
            else
            {
                resultado = "Token ausente";
                return 0;
            }
        }
    }
}
