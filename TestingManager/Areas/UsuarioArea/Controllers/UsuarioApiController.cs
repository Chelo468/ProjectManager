using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using TestingManager.Models;
using Servicios;
using Newtonsoft.Json;

namespace TestingManager.Areas.Usuario.Controllers
{
    public class UsuarioApiController : Controller
    {
        //
        // GET: /Usuario/UsuarioApi/
        UsuarioService serviceUsuario;

        public UsuarioApiController()
        {
            serviceUsuario = new UsuarioService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public string login(string login_name, string password)
        {
            //Request.

            //Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
            //Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            //Response.AddHeader("Access-Control-Max-Age", "1728000");
            //Response.AppendHeader("Access-Control-Allow-Origin", "*");
            //Response.ContentType = "application/json";
            //Response.AddHeader("Content-Type", "application/json");

            //dynamic objPersona = new System.Dynamic.ExpandoObject();
            //objPersona.login_name = Request.Headers["login_name"];
            //objPersona.password = Request.Headers["password"];

            Entidades.Usuario user = serviceUsuario.getByUserNamePassword(login_name, password);

            if(user != null)
            { 
                UsuarioWeb usuarioResult = MapearUsuarioWeb(user);

                return JsonConvert.SerializeObject(usuarioResult);
            }

            return JsonConvert.SerializeObject(user);
            //return Json(new { Error = true, Message = "Operación HTTP desconocida o imposible de ejecutar" }, JsonRequestBehavior.AllowGet);

        }

        private UsuarioWeb MapearUsuarioWeb(Entidades.Usuario user)
        {
            UsuarioWeb usuarioResult = new UsuarioWeb();

            usuarioResult.id_usuario = user.id_usuario;
            usuarioResult.login_name = user.login_name;
            usuarioResult.email = user.email;
            usuarioResult.habilitado = user.habilitado;
            usuarioResult.token_clave = user.token_clave;

            return usuarioResult;
        }

    }
}
