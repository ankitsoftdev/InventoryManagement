using System.Web.Mvc;

namespace InventoryManagement.Areas.InventoryMaster
{
    public class InventoryMasterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "InventoryMaster";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "InventoryMaster_default",
                "InventoryMaster/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
