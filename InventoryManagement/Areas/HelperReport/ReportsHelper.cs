using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Areas.HelperReport
{
    public static  class ReportsHelper
    {
        //
        // GET: /HelperReport/ReportsHelper/

        public static byte[] SetReportDefault(dynamic dataset, string DatasetName, string id, string path, out string mimeType, string CompanyName, string DayDate, string reportName, string Image="")
        {
            LocalReport lr = new LocalReport();

            lr.ReportPath = path;
           
            ReportDataSource rd = new ReportDataSource(DatasetName, dataset);
            lr.DataSources.Add(rd);
            lr.SetParameters(new ReportParameter("CompanyName", CompanyName));
            lr.SetParameters(new ReportParameter("DayDate", DayDate));
            lr.SetParameters(new ReportParameter("ReportName", reportName));
            lr.SetParameters(new ReportParameter("Image", Image));
            lr.GetParameters();
            string reportType = id;

            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            " <OutputFormat>" + id + "</OutputFormat>" +
            " <PageWidth>10.5in</PageWidth>" +
            " <PageHeight>11in</PageHeight>" +
            " <MarginTop>0.5in</MarginTop>" +
            " <MarginLeft>1in</MarginLeft>" +
            " <MarginRight>1in</MarginRight>" +
            " <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;

       
                
   return   lr.Render(
   reportType,
   deviceInfo,
   out mimeType,
   out encoding,
   out fileNameExtension,
   out streams,
   out warnings);
           
          
        }

    }
}
