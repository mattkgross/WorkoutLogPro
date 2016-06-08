using System.Web.Mvc;

namespace WorkoutLogPro.Areas.Teams
{
    public class TeamsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Teams";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Teams_default",
                "Teams/{controller}/{action}/{id}",
                new {controller = "Teams", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}