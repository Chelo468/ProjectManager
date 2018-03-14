using Entidades;
using Newtonsoft.Json;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingManager.Areas.ProyectoArea.Controllers
{
    public class ProyectoApiController : Controller
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
        public JsonResult getByIdUser(int id_usuario)
        {
            Proyecto proyecto = serviceProyecto.getByIdUser(id_usuario);

            if(proyecto != null)
            {
                //return JsonConvert.SerializeObject(proyecto);
                return Json(proyecto, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Error = true }, JsonRequestBehavior.AllowGet);
            }

            
        }

    }
}
