using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Summer_School_MVC1.Models;

namespace Summer_School_MVC1.Controllers
{
    public class HomeController : Controller
    {
        private SummerSchoolEntities db = new SummerSchoolEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}