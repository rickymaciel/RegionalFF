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
            DateTime Fecha = DateTime.Now;
            var paisesMes = db.Facilitacions.GroupBy(x => x.Fecha.Year).Select(g => new { year = g.FirstOrDefault().Fecha, value = g.Sum(s => s.Cantidad) }).ToList();
            var i = 0;
            List<VisitaYear> paisesLista = new List<VisitaYear>();

            foreach (var item in paisesMes)
            {
                paisesLista.Add(new VisitaYear()
                {
                    year = item.year.ToString("yyyy"),
                    value = item.value,
                });
                i++;
            }
            var pie = new VisitasYearViewModel()
            {
                SeriesData = paisesLista
            };

            ViewBag.SeriesData = pie.SeriesData;

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            var UserId = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View();
        }
    }
}