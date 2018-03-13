using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Entidades;
using Servicios;
using TestingManager.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TestingManager.Controllers
{
    public class UsuarioController : ApiController
    {
        UsuarioService serviceUsuario;

        public UsuarioController()
        {
            serviceUsuario = new UsuarioService();
        }

        
        public string Get(int id)
        {
            
            Usuario user = serviceUsuario.getById(id);

            string output = JsonConvert.SerializeObject(user);
                        
            return output;
            //return ActionContext.Request.CreateResponse(HttpStatusCode.OK, output);            
        }


        public string login(string login_name, string password)
        {


            Usuario user = serviceUsuario.getByUserNamePassword(login_name, password);

            UsuarioWeb usuarioResult = MapearUsuarioWeb(user);

            return JsonConvert.SerializeObject(usuarioResult);
        }

        private UsuarioWeb MapearUsuarioWeb(Usuario user)
        {
            UsuarioWeb usuarioResult = new UsuarioWeb();

            usuarioResult.id_usuario = user.id_usuario;
            usuarioResult.login_name = user.login_name;
            usuarioResult.email = user.email;
            usuarioResult.habilitado = user.habilitado;
            usuarioResult.token_clave = user.token_clave;

            return usuarioResult;
        }


        public JsonResult ObtenerPersona(string nombre, string apellido) 
        {
            //Request.

            dynamic objPersona = new System.Dynamic.ExpandoObject();
            objPersona.Nombre = nombre;
            objPersona.Apellido = apellido;

            //return Json(objPersona, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(objPersona);

        }
            

    }
}
