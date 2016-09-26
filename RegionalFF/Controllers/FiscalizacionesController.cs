using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RegionalFF.Models;
using Microsoft.AspNet.Identity;

namespace RegionalFF.Controllers
{
    public class FiscalizacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fiscalizaciones
        public ActionResult Index()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            var fiscalizacions = db.Fiscalizacions.Include(f => f.Ciudad).Include(f => f.Fiscal).Include(f => f.Pais).Include(f => f.Transporte);
            return View(fiscalizacions.ToList().Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Day == Fecha.Day).Where(u => u.UserId == User.Identity.GetUserId()).OrderByDescending(f => f.Fecha));
        }

        // GET: Fiscalizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            if (fiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(fiscalizacion);
        }

        // GET: Fiscalizaciones/Create
        public ActionResult Create()
        {
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");

            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View();
        }

        // POST: Fiscalizaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Fecha,TransporteId,CiudadId,PaisId,CantidadPasajeros,Observaciones,Habilitado,Activo")] Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);
            return View(fiscalizacion);
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacion(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                fiscalizacion.Fecha = Fecha;
                fiscalizacion.Activo = true;
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            else
            {
                TempData["notice"] = "Hubo un error y la Fiscalización no fue registrada correctamente";
                return RedirectToAction("Index", "Fiscalizaciones");
            }
            return RedirectToAction("Index", "Fiscalizaciones");
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacionMes(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            return RedirectToAction("EsteMes", "Fiscalizaciones");
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacionAño(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            return RedirectToAction("EsteAño", "Fiscalizaciones");
        }


        // GET: Fiscalizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            fiscalizacion.UserId = User.Identity.GetUserId();
            fiscalizacion.Fecha = Fecha;
            if (fiscalizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);

            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View(fiscalizacion);
        }

        // POST: Fiscalizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Fecha,TransporteId,CiudadId,PaisId,CantidadPasajeros,Observaciones,Habilitado,Activo")] Fiscalizacion fiscalizacion)
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            fiscalizacion.Fecha = Fecha;
            if (ModelState.IsValid)
            {
                db.Entry(fiscalizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);
            return View(fiscalizacion);
        }

        // GET: Fiscalizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            if (fiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(fiscalizacion);
        }

        // POST: Fiscalizaciones/Delete/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            db.Fiscalizacions.Remove(fiscalizacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public int? getTotalRegistroHoy(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return hoy;
        }

        public int? getTotalBusesNotificadosHoy(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }

        public int? getTotalPasajerosHoy(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }

        public int? getCantidadRegistroHoy(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        public int? getCantidadRegistroMes(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        public int? getCantidadRegistroAnio(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        // Total todos usuarios

        public int? getCantidadRegistroHoy()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        public int? getCantidadRegistroMes()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        public int? getCantidadRegistroAnio()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }



        public int? getTotalRegistroMes(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int mes = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return mes;
        }

        public int? getTotalRegistroAnio(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int anio = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return anio;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
