using System.Web.Mvc;

namespace TestingManager.Areas.ProyectoArea
{
    public class ProyectoAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ProyectoArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProyectoArea_default",
                "ProyectoArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ProyectoApi",
                "api/proyecto/getByIdUser/{id_usuario}",
                new { controller = "ProyectoApi", action = "getByIdUser", id_usuario = UrlParameter.Optional }
                );
        }
    }
}
