using RegionalFF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace RegionalFF.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ReturnTo()
        {
            CargarSesion();
            if (User.IsInRole("Administrador") || User.IsInRole("Facilitador"))
            {
                return RedirectToAction("Index", "Facilitaciones");
            }
            if (User.IsInRole("Administrador") || User.IsInRole("Fiscalizador"))
            {
                return RedirectToAction("Index", "Fiscalizaciones");
            }
            return RedirectToAction("Index");
        }

        public void CargarSesion()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            if (RegionalFF != null)
            {
                var imagen = RegionalFF.Values["Imagen"];
                var archivo = Path.GetFileName(imagen);
                var path = Path.Combine(Server.MapPath("~/Content/img/Uploads/Usuarios/Thumbnail/" + archivo), archivo + ".png");
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                ViewBag.ImagenUsuarioCurrent = imageDataURL;
                TempData["imageDataURL"] = imageDataURL;

            }
        }


        public ActionResult Index()
        {
            TempData["imageDataURL"] = TempData["imageDataURL"];
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

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            var UserId = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View();
        }
    }
}