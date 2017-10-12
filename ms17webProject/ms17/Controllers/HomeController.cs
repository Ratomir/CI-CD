using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ms17.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //neki komentar
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //ViewBag.Message = "Fake message.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}
