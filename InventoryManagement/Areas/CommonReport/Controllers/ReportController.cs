using HotelManagementErp_Main.Helper;
using Microsoft.Reporting.WebForms;
using ServiceLayer.CommonReport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Areas.CommonReport.Controllers
{
    public class ReportController : InventoryBaseController
    {
        //
        // GET: /CommonReport/Report/
        ReportServices objReportServices = new ReportServices();
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Unit()
        {
            return View();
        }
        public ActionResult UnitList()
        {
            return View(objReportServices.GetUnitReports());
        }
       
        public ActionResult UnitReport(string ReportType = "")
        {
            string path = Path.Combine(Server.MapPath("~/Areas/ReportView"), "UnitReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                path = path;

            }
            else
            {
                return RedirectToAction("Index");
            }
            string CompanyName = "fortune mkt pvt.";
            string DayDate = DateTime.Now.ToString("dd-MM-yyyy");
            string reportName = "Unit Master";

            byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Areas/ReportView/Image"), "logo.png"));
            string Image = Convert.ToBase64String(imageArray);

            string mimeType;
            byte[] renderedBytes = InventoryManagement.Areas.HelperReport.ReportsHelper.SetReportDefault(objReportServices.GetUnitReports(), "Unit", ReportType, path, out mimeType, CompanyName, DayDate, reportName, Image);
         
            return File(renderedBytes, mimeType);
        }
        public ActionResult FinancialYear()
        {
            return View();
        }
        public ActionResult FinancialYearList()
        {
            return View(objReportServices.GetFinancialYearReports().ToList());
        }
        public ActionResult FinancialYearReport(string ReportType = "")
        {
            string path = Path.Combine(Server.MapPath("~/Areas/ReportView"), "FinancialYearReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                path = path;

            }
            else
            {
                return RedirectToAction("Index");
            }
            string CompanyName = "fortune mkt pvt.";
            string DayDate = DateTime.Now.ToString("dd-MM-yyyy");
            string reportName = "FinancialYear";

            byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Areas/ReportView/Image"), "logo.png"));
            string Image = Convert.ToBase64String(imageArray);

            string mimeType;

            byte[] renderedBytes = InventoryManagement.Areas.HelperReport.ReportsHelper.SetReportDefault(objReportServices.GetFinancialYearReports(), "FinancialYear", ReportType, path, out mimeType, CompanyName, DayDate, reportName, Image);
            return File(renderedBytes, mimeType);
        }
        public ActionResult Tax()
        {
            return View();
        }
        public ActionResult TaxList()
        {
            return View(objReportServices.GetTaxMaster().ToList());
        }
        public ActionResult TaxListReport(string ReportType = "")
        {
            string path = Path.Combine(Server.MapPath("~/Areas/ReportView"), "TaxReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                path = path;

            }
            else
            {
                return RedirectToAction("Index");
            }
            string CompanyName = "fortune mkt pvt.";
            string DayDate = DateTime.Now.ToString("dd-MM-yyyy");
            string reportName = "Tax Master";

            byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Areas/ReportView/Image"), "logo.png"));
            string Image = Convert.ToBase64String(imageArray);

            string mimeType;

            byte[] renderedBytes = InventoryManagement.Areas.HelperReport.ReportsHelper.SetReportDefault(objReportServices.GetTaxMaster(), "Tax", ReportType, path, out mimeType, CompanyName, DayDate, reportName, Image);
            return File(renderedBytes, mimeType);
        }
        public ActionResult LedgerGroup()
        {
            return View();
        }
        public ActionResult LedgerGroupList()
        {
            return View(objReportServices.GetLedgerGroup().ToList());
        }
        public ActionResult LedgerGroupReport(string ReportType = "")
        {
            string path = Path.Combine(Server.MapPath("~/Areas/ReportView"), "LedgerGroupReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                path = path;

            }
            else
            {
                return RedirectToAction("Index");
            }
            string CompanyName = "fortune mkt pvt.";
            string DayDate = DateTime.Now.ToString("dd-MM-yyyy");
            string reportName = "Ledger Group";

            byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Areas/ReportView/Image"), "logo.png"));
            string Image = Convert.ToBase64String(imageArray);

            string mimeType;

            byte[] renderedBytes = InventoryManagement.Areas.HelperReport.ReportsHelper.SetReportDefault(objReportServices.GetLedgerGroup(), "LedgerGroupReports", ReportType, path, out mimeType, CompanyName, DayDate, reportName, Image);
            return File(renderedBytes, mimeType);
        }
        public ActionResult Supplier(string TagName)
        {
            ViewBag.TagName = TagName;
            return View();
        }
        public ActionResult SupplierList(string TagName)
        {
            return View(objReportServices.GetSupplierCustomerInfo(TagName).ToList());
        }
        public ActionResult SupplierReport(string ReportType = "",string TagName="")
        {
            string path = Path.Combine(Server.MapPath("~/Areas/ReportView"), "CustomersSupplierReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                path = path;

            }
            else
            {
                return RedirectToAction("Index");
            }
            string CompanyName = "fortune mkt pvt.";
            string DayDate = DateTime.Now.ToString("dd-MM-yyyy");
             string reportName="";
            if (TagName == "Customers")
            {
                reportName = "Customers Master";
            }
            else
            {
                 reportName = "Supplier Master";
            }
            byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Areas/ReportView/Image"), "logo.png"));
            string Image = Convert.ToBase64String(imageArray);

            string mimeType="";
            byte[] renderedBytes = renderedBytes = InventoryManagement.Areas.HelperReport.ReportsHelper.SetReportDefault(objReportServices.GetSupplierCustomerInfo(TagName), "CustomersSupplier", ReportType, path, out mimeType, CompanyName, DayDate, reportName, Image);
            
           
            return File(renderedBytes, mimeType);
        }
        public ActionResult StockGroup()
        {
            return View();
        }
        public ActionResult StockGroupList()
        {
            return View(objReportServices.GetStockGroup().ToList());
        }
        public ActionResult StockGroupReports(string ReportType = "")
        {
            string path = Path.Combine(Server.MapPath("~/Areas/ReportView"), "LedgerGroupReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                path = path;

            }
            else
            {
                return RedirectToAction("Index");
            }
            string CompanyName = "fortune mkt pvt.";
            string DayDate = DateTime.Now.ToString("dd-MM-yyyy");
            string reportName = "Stock Group";

            byte[] imageArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Areas/ReportView/Image"), "logo.png"));
            string Image = Convert.ToBase64String(imageArray);

            string mimeType;
            byte[] renderedBytes = InventoryManagement.Areas.HelperReport.ReportsHelper.SetReportDefault(objReportServices.GetStockGroup(), "LedgerGroupReports", ReportType, path, out mimeType, CompanyName, DayDate, reportName, Image);

            return File(renderedBytes, mimeType);
        }
        
    }
}
