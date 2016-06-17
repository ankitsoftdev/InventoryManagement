using System.Web.Mvc;

namespace InventoryManagement.Areas.AccountsMaster
{
    public class AccountsMasterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AccountsMaster";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AccountsMaster_default",
                "AccountsMaster/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
