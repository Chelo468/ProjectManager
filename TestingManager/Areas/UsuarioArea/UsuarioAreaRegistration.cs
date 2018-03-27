using System.Web.Mvc;

namespace TestingManager.Areas.Usuario
{
    public class UsuarioAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Usuario";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Usuario_default",
                "Usuario/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "UsuarioApiGetUsers",
                "api/usuario/getUsers",
                new { controller = "UsuarioApi", action = "getUsers" }
                );

            context.MapRoute(
                "UsuarioApiGetByFilters",
                "api/usuario/getByFilters/{usuario}/{id_rol}",
                new { controller = "UsuarioApi", action = "getByFilters", usuario = UrlParameter.Optional, id_rol = UrlParameter.Optional }
                );

            context.MapRoute(
                "UsuarioApigetUserById",
                "api/usuario/getUserById/{id_usuario}",
                new { controller = "UsuarioApi", action = "getUserById", id_usuario = UrlParameter.Optional }
                );

            context.MapRoute(
                "UsuarioApiLogin",
                "api/usuario/login/{login_name}/{password}",
                new { controller = "UsuarioApi", action = "login", login_name = UrlParameter.Optional, password = UrlParameter.Optional }
                );

            context.MapRoute(
                "UsuarioApiCrear",
                "api/usuario/crear/",
                new { controller = "UsuarioApi", action = "crear" }
                );

            context.MapRoute(
                "UsuarioApiLogOut",
                "api/usuario/logout/",
                new { controller = "UsuarioApi", action = "logout" }
                );

            context.MapRoute(
                "UsuarioApiUpdateRoles",
                "api/usuario/updateRoles",
                new { controller = "UsuarioApi", action = "updateRoles" }
                );
        }
    }
}
