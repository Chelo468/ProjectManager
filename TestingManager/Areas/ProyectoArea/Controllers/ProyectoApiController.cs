using Entidades;
using Newtonsoft.Json;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingManager.Controllers;

namespace TestingManager.Areas.ProyectoArea.Controllers
{
    public class ProyectoApiController : GenericController
    {
        //
        // GET: /ProyectoArea/ProyectoApi/

        ProyectoService serviceProyecto;

        public ProyectoApiController()
        {
            serviceProyecto = new ProyectoService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult getByIdUser(int id_usuario, string token)
        {
            if(token != null && getSesionByToken(token) != null)
            { 
                Proyecto proyecto = serviceProyecto.getByIdUser(id_usuario);

                if(proyecto != null)
                {
                    //return JsonConvert.SerializeObject(proyecto);
                    return Json(proyecto, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Mensaje = "no se encuentran proyectos" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Error = true, Mensaje = "token no válido" }, JsonRequestBehavior.AllowGet);
            }


        }

        [AllowAnonymous]
        public JsonResult crear()
        {
            return Json(new { Error = false }, JsonRequestBehavior.AllowGet);
        }

    }
}
