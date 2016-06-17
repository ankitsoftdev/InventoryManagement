using System.Web.Mvc;

namespace InventoryManagement.Areas.CommonReport
{
    public class CommonReportAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CommonReport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CommonReport_default",
                "CommonReport/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
