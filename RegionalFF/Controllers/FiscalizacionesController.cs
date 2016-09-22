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

        // GET: Fiscalizaciones/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);
            return View(fiscalizacion);
        }

        // POST: Fiscalizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Fecha,TransporteId,CiudadId,PaisId,CantidadPasajeros,Observaciones,Habilitado,Activo")] Fiscalizacion fiscalizacion)
        {
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            db.Fiscalizacions.Remove(fiscalizacion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
