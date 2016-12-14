using RegionalFF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace RegionalFF.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            DateTime Fecha = DateTime.Now;
            var UserId = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View();
        }
    }
}