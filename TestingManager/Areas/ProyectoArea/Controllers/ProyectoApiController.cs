using Entidades;
using Entidades.Entidades;
using Newtonsoft.Json;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
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
        RolService serviceRol;

        public ProyectoApiController()
        {
            serviceProyecto = new ProyectoService();
            serviceRol = new RolService();

        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult getById(int idProyecto)
        {
            string token = Request.Headers["X-AUTH-TOKEN"];
            string resultado = "";
            if (Request.HttpMethod == "GET")
            {
                if (token != null)
                {
                    Sesion sesionActual = getSesionByToken(token);

                    if (sesionActual != null)
                    {
                        string error = "";
                        Proyecto proyecto = serviceProyecto.getById(idProyecto, ref error);

                        if (proyecto.usuario_creador.id_usuario != sesionActual.usuario_logueado.id_usuario)
                        {
                            List<Rol> rolesUsuarioLogueado = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);
                            bool esAdministrador = false;
                            foreach (var rol in rolesUsuarioLogueado)
                            {
                                if(rol.id_rol == 1)
                                {
                                    esAdministrador = true;
                                    break;
                                }
                            }
                            if(!esAdministrador)
                            {
                                error = "Usted no tiene permiso sobre este proyecto.";
                            }
                        }

                        if (!string.IsNullOrEmpty(error))
                        {
                            return Json(new { Error = true, Mensaje = error }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(proyecto, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        resultado = "No se encontró una sesión activa";
                    }
                }
                else
                {
                    resultado = "token no válido";
                }
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult getByIdUser(string token)
        {
            string resultado = "";
            if (Request.HttpMethod == "GET")
            {
                Sesion sesionActual = getSesionByToken(token);
                if (token != null && sesionActual != null)
                {
                    List<Rol> rolesUsuario = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);

                    bool esAdministrador = false;

                    foreach (var rol in rolesUsuario)
                    {
                        if(rol.id_rol == 1)
                        {
                            esAdministrador = true;
                            break;
                        }
                    }
                    List<Proyecto> proyectos = new List<Proyecto>();
                    if (esAdministrador)
                        proyectos = serviceProyecto.getAll(ref resultado);
                    else
                        proyectos = serviceProyecto.getByIdUser(sesionActual.usuario_logueado.id_usuario);

                    if (proyectos.Count > 0)
                    {
                        //return JsonConvert.SerializeObject(proyecto);
                        return Json(proyectos, JsonRequestBehavior.AllowGet);
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
            else
            {
                return Json(new { Error = true, Mensaje = "" }, JsonRequestBehavior.AllowGet);
            }


        }

        [AllowAnonymous]
        public JsonResult getUsersById(int id_proyecto)
        {
            string resultado = "";
            if (Request.HttpMethod == "GET")
            {
                string token = Request.Headers["X-AUTH-TOKEN"];

                if (token != null)
                {
                    Sesion sesionActual = getSesionByToken(token);

                    if (sesionActual != null)
                    {
                        string error = "";

                        List<Rol> rolesUsuarioLogueado = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);
                        bool esAdministrador = false;
                        foreach (var rol in rolesUsuarioLogueado)
                        {
                            if (rol.id_rol == 1)
                            {
                                esAdministrador = true;
                                break;
                            }
                        }
                        if (!esAdministrador)
                        {
                            error = "Usted no tiene permiso para ver la información.";
                        }
                        

                        if (!string.IsNullOrEmpty(error))
                        {
                            return Json(new { Error = true, Mensaje = error }, JsonRequestBehavior.AllowGet);
                        }


                        List<Proyecto_Usuario> usuarios = serviceProyecto.getUsersById(id_proyecto, ref error);

                        return Json(usuarios, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        resultado = "No se encontró una sesión activa";
                    }
                }
                else
                {
                    resultado = "token no válido";
                }
            }
            
            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);
          
        }

        [AllowAnonymous]
        public JsonResult crear()
        {
            if (Request.HttpMethod == "POST")
            {
                try
                {
                    string token = Request.Headers["X-AUTH-TOKEN"];
                    Sesion sesionActual = getSesionByToken(token);
                    if (sesionActual != null)
                    {
                        Request.InputStream.Seek(0, SeekOrigin.Begin);
                        string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                        dynamic objProyecto = JsonConvert.DeserializeObject(jsonData, typeof(object));

                        if (!string.IsNullOrEmpty(jsonData))
                        {

                            ProyectoService serviceProyecto = new ProyectoService();

                            string respuesta = "";

                            Proyecto nuevoProyecto = new Proyecto();
                            nuevoProyecto.nombre = objProyecto.nombre;
                            nuevoProyecto.descripcion = objProyecto.descripcion;
                            nuevoProyecto.urlTesting = objProyecto.urlTesting;
                            nuevoProyecto.urlProduccion = objProyecto.urlProduccion;
                            nuevoProyecto.usuario_creador.id_usuario = sesionActual.usuario_logueado.id_usuario;
                            nuevoProyecto.fecha_alta = DateTime.Now;

                            try
                            {
                                nuevoProyecto.id_proyecto = serviceProyecto.crear(nuevoProyecto, ref respuesta);
                            }
                            catch (Exception)
                            {
                                return Json(new { Error = true, Mensaje = "Ocurrió un error al crear el proyecto" }, JsonRequestBehavior.AllowGet);
                            }

                            if (nuevoProyecto.id_proyecto == -1)
                                return Json(new { Error = true, Mensaje = "No tiene permiso para crear proyectos." }, JsonRequestBehavior.AllowGet);
                            else
                                return Json(nuevoProyecto, JsonRequestBehavior.AllowGet);


                        }
                        else
                        {
                            return Json(new { Error = true, Mensaje = "No existen datos." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Error = true, Mensaje = "token no válido." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    LogueadorService.loguear(ex.Message, GetType().Namespace, GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return Json(new { Error = true, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Error = true, Mensaje = "" }, JsonRequestBehavior.AllowGet);
            }
           
        }

        [AllowAnonymous]
        public JsonResult actualizar(int idProyecto)
        {
            string token = Request.Headers["X-AUTH-TOKEN"];
            string resultado = "";
            if (Request.HttpMethod == "POST")
            {
                if (token != null)
                {
                    Sesion sesionActual = getSesionByToken(token);

                    if (sesionActual != null)
                    {
                        string error = "";
                        Proyecto proyecto = serviceProyecto.getById(idProyecto, ref error);

                        if (proyecto.usuario_creador.id_usuario != sesionActual.usuario_logueado.id_usuario)
                        {
                            List<Rol> rolesUsuarioLogueado = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);
                            bool esAdministrador = false;
                            foreach (var rol in rolesUsuarioLogueado)
                            {
                                if (rol.id_rol == 1)
                                {
                                    esAdministrador = true;
                                    break;
                                }
                            }
                            if (!esAdministrador)
                            {
                                error = "Usted no tiene permiso sobre este proyecto.";
                            }
                        }

                        if (!string.IsNullOrEmpty(error))
                        {
                            return Json(new { Error = true, Mensaje = error }, JsonRequestBehavior.AllowGet);
                        }

                        if (proyecto.id_proyecto > 0)
                        {
                            Request.InputStream.Seek(0, SeekOrigin.Begin);
                            string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                            dynamic objProyecto = JsonConvert.DeserializeObject(jsonData, typeof(object));

                            proyecto.nombre = objProyecto.nombre;
                            proyecto.descripcion = objProyecto.descripcion;
                            proyecto.urlTesting = objProyecto.urlTesting;
                            proyecto.urlProduccion = objProyecto.urlProduccion;
                            proyecto.fecha_ultima_modif = DateTime.Now;

                            serviceProyecto.actualizar(proyecto, ref resultado);
                        }
                        if (string.IsNullOrEmpty(resultado))
                            return Json(proyecto, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        resultado = "No se encontró una sesión activa";
                    }
                }
                else
                {
                    resultado = "token no válido";
                }
            }

            return Json(new { Error = true, Mensaje = resultado }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult eliminar()
        {
            string token = Request.Headers["X-AUTH-TOKEN"];
            string resultado = "";
            if (Request.HttpMethod == "POST")
            {
                if (token != null)
                {
                    Sesion sesionActual = getSesionByToken(token);

                    if (sesionActual != null)
                    {
                        string error = "";

                        Proyecto proyectoAux = new Proyecto();

                        Request.InputStream.Seek(0, SeekOrigin.Begin);
                        string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                        dynamic objProyecto = JsonConvert.DeserializeObject(jsonData, typeof(object));

                        proyectoAux.id_proyecto = objProyecto.id_proyecto;

                        Proyecto proyecto = serviceProyecto.getById(proyectoAux.id_proyecto, ref error);

                        if (proyecto.usuario_creador.id_usuario != sesionActual.usuario_logueado.id_usuario)
                        {
                            List<Rol> rolesUsuarioLogueado = serviceRol.getByIdUser(sesionActual.usuario_logueado.id_usuario, ref resultado);
                            bool esAdministrador = false;
                            foreach (var rol in rolesUsuarioLogueado)
                            {
                                if (rol.id_rol == 1)
                                {
                                    esAdministrador = true;
                                    break;
                                }
                            }
                            if (!esAdministrador)
                            {
                                error = "Usted no tiene permiso sobre este proyecto.";
                            }
                        }

                        if (!string.IsNullOrEmpty(error))
                        {
                            return Json(new { Error = true, Mensaje = error }, JsonRequestBehavior.AllowGet);
                        }

                        if (proyecto.id_proyecto > 0)
                        {
                            proyecto.fecha_baja = DateTime.Now;

                            serviceProyecto.eliminar(proyecto, ref resultado);

                            return Json(proyecto, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { Error = true, Mensaje = "El proyecto no existe o no tiene permiso a él." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Error = true, Mensaje = "Token no válido." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Error = true, Mensaje = "Token ausente." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Error = true, Mensaje = "" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
