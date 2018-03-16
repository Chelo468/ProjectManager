using Entidades.Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
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
            sesion.token = "asdqwe";
            sesion.fecha_inicio = DateTime.Now;

            SesionService serviceSesion = new SesionService();

            string respuesta = "";

            serviceSesion.insert(sesion, ref respuesta);

            return string.IsNullOrEmpty(respuesta) ? sesion.token : "";

        }

    }
}
