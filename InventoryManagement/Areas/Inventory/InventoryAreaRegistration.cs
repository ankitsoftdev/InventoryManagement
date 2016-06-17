using System.Web.Mvc;

namespace HotelManagementErp_Main.Areas.Inventory
{
    public class InventoryAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Inventory";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Inventory_default",
                "{Domain}-{User}Inventory/Inverntory Management Portal/{Role}-{controller}.{action}/{id}",
                new { action = "Index", Domain = "Fortune.Marketing.Pvt.Ltd", User = "Testing", Role = "Admin", id = UrlParameter.Optional }
            );
        }
    }
}
