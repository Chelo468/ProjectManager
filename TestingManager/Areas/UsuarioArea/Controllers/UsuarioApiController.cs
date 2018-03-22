using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using TestingManager.Models;
using Servicios;
using Newtonsoft.Json;
using TestingManager.Controllers;
using Entidades.Entidades;
using System.IO;

namespace TestingManager.Areas.Usuario.Controllers
{
    public class UsuarioApiController : GenericController
    {
        //
        // GET: /Usuario/UsuarioApi/
        UsuarioService serviceUsuario;
        RolService serviceRol;

        public UsuarioApiController()
        {
            serviceUsuario = new UsuarioService();
            serviceRol = new RolService();
        }

        [AllowAnonymous]
        public JsonResult crear()
        {
            string resultado = "";
            try
            {
                Request.InputStream.Seek(0, SeekOrigin.Begin);
                string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                dynamic objUsuario = JsonConvert.DeserializeObject(jsonData, typeof(object));

                if (!string.IsNullOrEmpty(jsonData))
                {
                    
                    Entidades.Usuario nuevoUsuario = new Entidades.Usuario();
                    try
                    {
                        nuevoUsuario.nombre = objUsuario.nombre;
                        nuevoUsuario.apellido = objUsuario.apellido;
                        nuevoUsuario.email = objUsuario.email;
                        nuevoUsuario.login_name = objUsuario.login_name;
                        nuevoUsuario.password = objUsuario.password;
                        nuevoUsuario.fecha_alta = DateTime.Now;
                        nuevoUsuario.habilitado = false;
                        nuevoUsuario.telefono = objUsuario.telefono;

                        //TODO: Generar random de token para habilitar usuario
                        nuevoUsuario.token_clave = "asdqwe";
                    }
                    catch (Exception)
                    {
                        resultado = "Faltan datos";
                    }
                                     
                    if(string.IsNullOrEmpty(resultado))
                    { 
                        try
                        {
                            nuevoUsuario.id_usuario = serviceUsuario.crear(nuevoUsuario, ref resultado);
                            return Json(nuevoUsuario, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception)
                        {
                            resultado = "Ocurrió un error al crear el proyecto";// }, JsonRequestBehavior.AllowGet);
                        }
                        
                    }                    
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);
         }

        [AllowAnonymous]
        public JsonResult login(string login_name, string password)
        {
            LogueadorService.loguear("Parametros:" + login_name + "," + password, "Testingmanager.Areas.Usuario.Controllers", "UsuarioApiController", "login");
            Entidades.Usuario user = serviceUsuario.getByUserNamePassword(login_name, password);

            if (user != null)
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
                
                string token = crearSesion(usuarioResult.id_usuario);
                
                usuarioResult.tokenSession = token;

                //return JsonConvert.SerializeObject(usuarioResult);
                return Json(usuarioResult, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Error = true, Mensaje = "Usuario y/o contraseña incorrecta." }, JsonRequestBehavior.AllowGet);//JsonConvert.SerializeObject(user);
            //return Json(new { Error = true, Message = "Operación HTTP desconocida o imposible de ejecutar" }, JsonRequestBehavior.AllowGet);
            
        }

        [AllowAnonymous]
        public JsonResult updateRoles()
        {
            string resultado = "";
            try
            {
                string token = Request.Headers["X-AUTH-TOKEN"];

                if (!string.IsNullOrEmpty(token))
                {
                    SesionService sesionService = new SesionService();

                    Sesion sesionActual = sesionService.getByToken(token, ref resultado);

                    if (sesionActual != null)
                    {
                        List<Rol> rolesUsuario = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);

                        foreach (var rol in rolesUsuario)
                        {
                            if(rol.id_rol == 1)
                            {
                                Request.InputStream.Seek(0, SeekOrigin.Begin);
                                string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                                dynamic objUsuario = JsonConvert.DeserializeObject(jsonData, typeof(object));

                                int idUserActualizable = objUsuario.roles.idUser;

                                Entidades.Usuario user = serviceUsuario.getById(idUserActualizable, ref resultado);
                                if(user.id_usuario > 0)
                                {
                                    List<Rol> rolesAAsignar = new List<Rol>();
                                    bool administrador = objUsuario.roles.administrador;
                                    bool creador = objUsuario.roles.creador;
                                    bool analista = objUsuario.roles.analista;
                                    bool desarrollador = objUsuario.roles.desarrollador;
                                    bool evaluador = objUsuario.roles.evaluador;
                                    bool implementador = objUsuario.roles.implementador;

                                    if (administrador)
                                        rolesAAsignar.Add(new Rol { id_rol = 1});
                                    if (creador)
                                        rolesAAsignar.Add(new Rol { id_rol = 2 });
                                    if (analista)
                                        rolesAAsignar.Add(new Rol { id_rol = 3 });
                                    if (desarrollador)
                                        rolesAAsignar.Add(new Rol { id_rol = 4 });
                                    if (evaluador)
                                        rolesAAsignar.Add(new Rol { id_rol = 5 });
                                    if (implementador)
                                        rolesAAsignar.Add(new Rol { id_rol = 6 });

                                    user.roles = new List<Rol>();

                                    user.roles.AddRange(rolesAAsignar);

                                    serviceUsuario.updateRoles(user, ref resultado);
                                }

                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);
        }

        private bool existeUsuario(int idUserActualizable)
        {
            string resultado = "";
            var usuario = serviceUsuario.getById(idUserActualizable, ref resultado);
            if (usuario.id_usuario > 0)
                return true;
            else
                return false;
        }

        [AllowAnonymous]
        public JsonResult logout()
        {
            string resultado = "";
            try
            {
                string token = Request.Headers["X-AUTH-TOKEN"];

                if (!string.IsNullOrEmpty(token))
                {
                    SesionService sesionService = new SesionService();

                    Sesion sesionActual = sesionService.getByToken(token, ref resultado);

                    if(sesionActual != null)
                        sesionService.delete(sesionActual);
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult getUserById(int id_usuario)
        {
            string resultado = "";
            try
            {
                string token = Request.Headers["X-AUTH-TOKEN"];

                if (!string.IsNullOrEmpty(token))
                {
                    SesionService sesionService = new SesionService();

                    Sesion sesionActual = sesionService.getByToken(token, ref resultado);

                    List<Rol> roles = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);

                    if (roles.Count > 0)
                    {
                        foreach (var rol in roles)
                        {
                            if (rol.id_rol == 1)
                            {
                                //Si tiene el rol 1 quiere decir que es administrador
                                Entidades.Usuario usuario = serviceUsuario.getById(id_usuario, ref resultado);

                                if(string.IsNullOrEmpty(resultado))
                                { 
                                    UsuarioWeb usuarioWeb = MapearUsuarioWeb(usuario);
                                    return Json(usuarioWeb, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);

        }

        [AllowAnonymous]
        public JsonResult getUsers()
        {
            string resultado = "";
            try
            {
                string token = Request.Headers["X-AUTH-TOKEN"];

                if (!string.IsNullOrEmpty(token))
                {
                    SesionService sesionService = new SesionService();

                    Sesion sesionActual = sesionService.getByToken(token, ref resultado);

                    List<Rol> roles = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);

                    if(roles.Count > 0)
                    {
                        foreach (var rol in roles)
                        {
                            if(rol.id_rol == 1)
                            {
                                //Si tiene el rol 1 quiere decir que es administrador
                                List<Entidades.Usuario> usuarios = serviceUsuario.getAll(ref resultado);

                                List<UsuarioWeb> usuariosWeb = MapearListUsuarioWeb(usuarios);                                    
                                return Json(usuariosWeb, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);

        }

        private List<UsuarioWeb> MapearListUsuarioWeb(List<Entidades.Usuario> usuarios)
        {
            List<UsuarioWeb> usuariosWeb = new List<UsuarioWeb>();

            foreach (var usuario in usuarios)
            {
                UsuarioWeb usuarioWeb = MapearUsuarioWeb(usuario);
                usuariosWeb.Add(usuarioWeb);
            }

            return usuariosWeb;
        }



        private UsuarioWeb MapearUsuarioWeb(Entidades.Usuario user)
        {
            UsuarioWeb usuarioResult = new UsuarioWeb();

            usuarioResult.id_usuario = user.id_usuario;
            usuarioResult.login_name = user.login_name;
            usuarioResult.email = user.email;
            usuarioResult.habilitado = user.habilitado;
            usuarioResult.token_clave = user.token_clave;
            usuarioResult.fecha_alta = user.fecha_alta.ToShortDateString();
            usuarioResult.fecha_baja = user.fecha_baja.ToShortDateString();
            usuarioResult.fecha_ultima_modif = user.fecha_ultima_modif.ToShortDateString();

            usuarioResult.roles = user.roles;

            return usuarioResult;
        }

    }
}
