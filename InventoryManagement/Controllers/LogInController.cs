using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HDMEntity;


using System.Web.Security;
using System.IO;
using System.Data;
using System.Data.Common;

using ServiceLayer;
using HotelManagementErp_Main.Helper;
namespace InventoryManagement.Areas.Controllers
{
    
    public class LogInController : Controller
    {
  
            #region Attributes
            //
            // GET: /LogIn/
       
            #endregion
        [HttpGet]
            public ActionResult LogIn(int id = 0, string Section = "")
            {

               HttpContext.Session["dbConnection"] = "";

                #region Form Authentication Signout
                FormsAuthentication.SignOut();
                Response.Cookies.Clear();
                #endregion
                //if (id > 0)
                //{
                //    Session.Clear();
                //    Session.Abandon();
                //    Session.RemoveAll();
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                //    Response.Cache.SetNoStore();
                //    FormsAuthentication.SignOut();
                //}

                //ViewBag.Section = Section;
                return View();


            }
           
            public ActionResult LogIn(int Code, String email, String password, string Section = "")
            {
             
      //    Session["dbConnection"] = "data source=52.24.116.86;initial catalog=INVENTORY_DB;persist security info=True;user id=sa;password=general@1234;MultipleActiveResultSets=True;App=EntityFramework";
          Session["dbConnection"] = "data source=techsmart;initial catalog=NEW_INVENTORY_DB;persist security info=True;user id=sa;password=#secur@1234;MultipleActiveResultSets=True;App=EntityFramework";
                return RedirectToAction("Index", "Dashboard", new { area = "Dashboard" });
                //var K = _Log.IsAvail(email, password, Code);
                //if (K != null && Section == "Hotel")
                //{
                //    #region Form Authentication Start
                //    var authTicket = new FormsAuthenticationTicket(Code, email,
                //                        DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //                        "Administrator"
                //                                                    );
                //    string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //    {

                //        Expires = authTicket.Expiration,
                //        Path = FormsAuthentication.FormsCookiePath
                //    };

                //    Response.Cookies.Add(cookie);
                //    #endregion
                //    #region  Dashboard Setting Option
                //    Session["dashboard"] = K;
                //    ViewBag.Msg = K;
                //    var menus = _Log.GetDashBoardData(K.Employee_Id);
                //    Session["chart"] = _Log.Chart_Info(K.Employee_Id);
                //    Session["menu"] = menus;
                //    Session["bgcolor"] = _Log.GetTheme(K.Hotel_Id, K.Branch_Id, K.Employee_Id);
                //    #endregion
                //    return View("DashBoard");
                //}
                //else
                //    if (K != null && Section == "Accounts")
                //    {
                //        #region Form Authentication Start
                //        var authTicket = new FormsAuthenticationTicket(Code, email,
                //                            DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //                            "Administrator"
                //                                                        );
                //        string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //        {

                //            Expires = authTicket.Expiration,
                //            Path = FormsAuthentication.FormsCookiePath
                //        };

                //        Response.Cookies.Add(cookie);
                //        #endregion
                //        #region  Dashboard Setting Option
                //        Session["dashboard"] = K;
                //        ViewBag.Msg = K;



                //        #endregion
                //        return RedirectToAction("Index", "Home", new { area = "Account" });
                //    }
                //    else if (K != null && Section == "Inventory")
                //    {
                //        #region Form Authentication Start
                //        var authTicket = new FormsAuthenticationTicket(Code, email,
                //                            DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //                            "Administrator"
                //                                                        );
                //        string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //        {

                //            Expires = authTicket.Expiration,
                //            Path = FormsAuthentication.FormsCookiePath
                //        };

                //        Response.Cookies.Add(cookie);
                //        #endregion
                //        #region  Dashboard Setting Option
                //        Session["dashboard"] = K;
                //        ViewBag.Msg = K;
                //        var menus = _Log.GetDashBoardData(K.Employee_Id);
                //        Session["chart"] = _Log.Chart_Info(K.Employee_Id);
                //        Session["menu"] = menus;
                //        Session["bgcolor"] = _Log.GetTheme(K.Hotel_Id, K.Branch_Id, K.Employee_Id);
                //        #endregion
                //        return RedirectToAction("InventoryDash", "Inventory", new { area = "Inventory" });
                //    }
                //    else if (K != null && Section == "Front Office")
                //    {
                //        #region Form Authentication Start
                //        var authTicket = new FormsAuthenticationTicket(Code, email,
                //                            DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //                            "Administrator"
                //                                                        );
                //        string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //        {

                //            Expires = authTicket.Expiration,
                //            Path = FormsAuthentication.FormsCookiePath
                //        };

                //        Response.Cookies.Add(cookie);
                //        #endregion
                //        #region  Dashboard Setting Option
                //        Session["dashboard"] = K;
                //        ViewBag.Msg = K;
                //        var menus = _Log.GetDashBoardData(K.Employee_Id);
                //        Session["chart"] = _Log.Chart_Info(K.Employee_Id);
                //        Session["menu"] = menus;
                //        Session["bgcolor"] = _Log.GetTheme(K.Hotel_Id, K.Branch_Id, K.Employee_Id);
                //        #endregion
                //        return RedirectToAction("Index", "FrontOffice", new { area = "FrontOffice" });
                //    }
                //    else
                //        if (K != null && Section == "Resturant")
                //        {
                //            #region Form Authentication Start
                //            //var authTicket = new FormsAuthenticationTicket(Code, email,
                //            //                    DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //            //                    "Administrator"
                //            //                                                );
                //            //string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //            //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //            //{

                //            //    Expires = authTicket.Expiration,
                //            //    Path = FormsAuthentication.FormsCookiePath
                //            //};

                //            //Response.Cookies.Add(cookie);
                //            //#endregion
                //            //#region  Dashboard Setting Option
                //            //Session["dashboard"] = K;
                //            //ViewBag.Msg = K;



                //            #endregion
                //            return RedirectToAction("TableLayout", "Restaurant", new { area = "Restaurant",  Tagname = "Restaurant" });
                //        }
                //    else if (K != null && Section == "Hotel_Minibar")
                //    {
                //        #region Form Authentication Start
                //        var authTicket = new FormsAuthenticationTicket(Code, email,
                //                            DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //                            "Administrator"
                //                                                        );
                //        string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //        {

                //            Expires = authTicket.Expiration,
                //            Path = FormsAuthentication.FormsCookiePath
                //        };

                //        Response.Cookies.Add(cookie);
                //        #endregion
                //        #region  Dashboard Setting Option
                //        Session["dashboard"] = K;
                //        ViewBag.Msg = K;
                //        var menus = _Log.GetDashBoardData(K.Employee_Id);
                //        Session["chart"] = _Log.Chart_Info(K.Employee_Id);
                //        Session["menu"] = menus;
                //        Session["bgcolor"] = _Log.GetTheme(K.Hotel_Id, K.Branch_Id, K.Employee_Id);
                //        #endregion
                //        return RedirectToAction("MiniBarDashBoard", "MiniBar", new { area = "Hotel_Minibar", Tagname = "Minibar" });
                //    }
                //        else
                //            if (K != null && Section == "Banquet")
                //            {
                //                #region Form Authentication Start
                //                var authTicket = new FormsAuthenticationTicket(Code, email,
                //                                    DateTime.Now, DateTime.Now.AddMinutes(30), true,
                //                                    "Administrator"
                //                                                                );
                //                string cookieContents = FormsAuthentication.Encrypt(authTicket);
                //                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                //                {

                //                    Expires = authTicket.Expiration,
                //                    Path = FormsAuthentication.FormsCookiePath
                //                };

                //                Response.Cookies.Add(cookie);
                //                #endregion
                //                #region  Dashboard Setting Option
                //                Session["dashboard"] = K;
                //                ViewBag.Msg = K;



                //                #endregion
                //                return RedirectToAction("Index", "Home", new { area = "Banquet" });
                //            }
                //else
                //    if (String.IsNullOrWhiteSpace(Section) && K!=null)
                //    {
                //        ViewBag.Msg = null;
                //        //return View("FrontLogin");
                //        return RedirectToAction("Error", "Home", new { area = "LandingDashBoard" });
                //    }
                //    else
                //    {
                //        ViewBag.Msg = null;
                //        //return View("FrontLogin");
                //        return RedirectToAction("LogIn", "LogIn", new { area = "" });
                //    }
            }

        //     public ActionResult DashBoard()
        //    {
        //            #region DashBoard Option
                    
        //            //var k = (HDMEntity.DashBoard)Session["dashboard"];
        //            //ViewBag.Msg = k;
        //            //Session["chart"] = _Log.Chart_Info(k.Employee_Id);
        //            //var menus = _Log.GetDashBoardData(k.Employee_Id);
        //            //Session["menu"] = menus;
        //            #endregion
        //            return View("DashBoard");
        //        //}
        //        //else
        //        //{
        //        //    return RedirectToAction("Login","LogIn");
        //        //}

        //    }

        //    [Authorize]
        //    public ActionResult UserInfo()
        //    {
        //        ViewBag.Msg = (HDMEntity.DashBoard)Session["dashboard"];
        //        return View();
        //    }

        //    [Authorize]
        //    public ActionResult Setting()
        //    {
        //        ViewBag.Msg = (HDMEntity.DashBoard)Session["dashboard"];
        //        return View();
        //    }
      
        //    [Authorize]
        //    public ActionResult HideMenu(String id, String empid)
        //    {
        //        #region DashBoard Data
        //        //Menu_Update m = new Menu_Update();
        //        //m.Hide_menu(id, Int32.Parse(empid));
        //        ViewBag.Msg = (HDMEntity.DashBoard)Session["dashboard"];
        //        #endregion
        //        return RedirectToAction("DashBoard");
        //    }
            
        //    [Authorize]
        //[HttpPost]
        //    public JsonResult PieChart()
        //    {

        //        int emp = 1000;
        //        int Visitor = 500;
        //        int RoomBooking = 400;
        //        int order = 200;
        //        int maintenance = 300;

        //        List<Data> _data = new List<Data>();

        //        _data.Add(new Data { Name = "Employee", Dt = emp });
        //        _data.Add(new Data { Name = "Vistor", Dt = Visitor });
        //        _data.Add(new Data { Name = "Room Booking", Dt = RoomBooking });
        //        _data.Add(new Data { Name = "Order", Dt = order });
        //        _data.Add(new Data { Name = "Maintenance", Dt = maintenance });


        //        return Json(_data, JsonRequestBehavior.AllowGet);
        //    }

        //    [HttpPost]
        //    public ActionResult BarChart()
        //    {
        //        int emp = 1000;
        //        int Visitor = 500;
        //        int RoomBooking = 400;
        //        int order = 200;
        //        int maintenance = 300;

        //        List<Data> _data = new List<Data>();

        //        _data.Add(new Data { Name = "Employee", Dt = emp });
        //        _data.Add(new Data { Name = "Vistor", Dt= Visitor });
        //        _data.Add(new Data { Name = "Room Booking", Dt = RoomBooking });
        //        _data.Add(new Data { Name = "Order", Dt = order });
        //        _data.Add(new Data { Name = "Maintenance", Dt = maintenance });


        //        return Json(_data, JsonRequestBehavior.AllowGet);

        //    }
        //    [Authorize]
        //    public JsonResult Area()
        //    {


        //        int emp = 15000;
        //        int Visitor = 5200;
        //        int RoomBooking = 4000;
        //        int order = 2000;
        //        int maintenance = 2000;

        //        List<Data> _data = new List<Data>();

        //        _data.Add(new Data { Name = "Employee", Dt = emp });
        //        _data.Add(new Data { Name = "Vistor", Dt = Visitor });
        //        _data.Add(new Data { Name = "Room Booking", Dt = RoomBooking });
        //        _data.Add(new Data { Name = "Order", Dt = order });
        //        _data.Add(new Data { Name = "Maintenance", Dt = maintenance });


        //        return Json(_data, JsonRequestBehavior.AllowGet);

               
        //    }

        //    [HttpPost]
        //    public JsonResult ComboChart()
        //    {
              
        //        int emp = 15000;
        //        int Visitor = 5200;
        //        int RoomBooking = 4000;
        //        int order = 2000;
        //        int maintenance = 2000;
       
       
               
        //        List<Data1> _data = new List<Data1>();

        //        for (int i = 0; i < 7; i++)
        //        {
                  
        //            _data.Add(new Data1 { Name = DateTime.Today.AddDays(-(double)i).ToShortDateString(), Dt = emp, visit = Visitor, roombooking = RoomBooking, order = order, maintinance = maintenance });
        //        }
              
        //        return Json(_data, JsonRequestBehavior.AllowGet);

        //    }

          

          

        //    [Authorize]
        //    public PartialViewResult _UnderConstrction()
        //    {
        //        ViewBag.Msg = (HDMEntity.DashBoard)Session["dashboard"];
        //        return PartialView();
        //    }

        //    [Authorize]
        //    public PartialViewResult Password_Chage()
        //    {
        //        ViewBag.Msg = (HDMEntity.DashBoard)Session["dashboard"];

        //        return PartialView();
        //    }

        //    [Authorize]
        //    [HttpPost]
        //    public ActionResult Password_Chage(String email, String Old_pass, String New_pass)
        //    {
        //        ViewBag.Msg = (HDMEntity.DashBoard)Session["dashboard"];
        //      //  int k = _Log.Change_Password(email, Old_pass, New_pass);
        //        if (0 > 0)
        //        {
        //            return RedirectToAction("FrontLogin", new { id = "Password Successfully Changed.Please Login with New Password" });
        //        }
        //        else
        //        {
        //            return RedirectToAction("FrontLogin", new { id = "Something going wrong.Contact Service Provider" });

        //        }
        //    }

          

          

            public ActionResult LockScreen()
            {
                var k = (HDMEntity.DashBoard)Session["dashboard"];
                ViewBag.Msg = k;
                return View();
            }
        public ActionResult CheckPage()
            {

                var k = (HDMEntity.DashBoard)Session["dashboard"];
                ViewBag.Msg = k;
                return View();
            }
        public ActionResult Error(String Code="")
        {
            ViewBag.ErrorCode = Code;
            return View();
        }
        public ActionResult FrontLogin(String id="")
        {
            ViewBag.CredentialInfo = id;
         //   var login = landingdashboard.Login();

        //    ViewBag.data = new SelectList(login.ToList(), "Id", "Name");

            return View();
        }
       
    }


    class Data
    {
        public int Dt { get; set; }
        public String Name { get; set; }
    }
    class Data1
    {
        public int Dt { get; set; }
        public String Name { get; set; }
        public String Date { get; set; }
        public int visit { get; set; }
        public int roombooking { get; set; }
        public int order { get; set; }
        public int maintinance { get; set; }
    }

}
