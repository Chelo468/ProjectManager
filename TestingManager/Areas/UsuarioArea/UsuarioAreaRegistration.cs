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
                "UsuarioApi",
                "api/usuario/login/{login_name}/{password}",
                new { controller = "UsuarioApi", action = "login", login_name = UrlParameter.Optional, password = UrlParameter.Optional }
                );
        }
    }
}
