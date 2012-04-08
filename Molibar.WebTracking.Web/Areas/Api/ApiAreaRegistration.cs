using System.Web.Mvc;

namespace Molibar.WebTracking.Web.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AddFormEvent",
                "Api/Tracking",
                new
                {
                    controller = "Tracking",
                    action = "FormEvent",
                    formEventPostModel = UrlParameter.Optional
                }
            );
            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
