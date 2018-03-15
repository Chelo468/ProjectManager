﻿using System;
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
            LogueadorService.loguear("Parametros:" + login_name + "," + password, "Testingmanager.Areas.Usuario.Controllers", "UsuarioApiController", "login");
            Entidades.Usuario user = serviceUsuario.getByUserNamePassword(login_name, password);

            if(user != null)
            {
                LogueadorService.loguear("user != null: Id: " + user.id_usuario, "Testingmanager.Areas.Usuario.Controllers", "UsuarioApiController", "login");
                UsuarioWeb usuarioResult = new UsuarioWeb();
                try
                {
                    usuarioResult = MapearUsuarioWeb(user);
                }
                catch (Exception ex)
                {
                    LogueadorService.loguear(ex.Message, "Testingmanager.Areas.Usuario.Controllers", "UsuarioApiController", "login");                   
                }
                

                usuarioResult.tokenSession = "asdqwe";

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
