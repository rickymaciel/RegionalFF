using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NotAuthorized()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}