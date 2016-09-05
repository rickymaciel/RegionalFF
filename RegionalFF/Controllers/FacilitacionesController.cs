using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RegionalFF.Models;

namespace RegionalFF.Controllers
{
    public class FacilitacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Facilitaciones
        public ActionResult Index()
        {
            var facilitacions = db.Facilitacions.Include(f => f.Año).Include(f => f.Ciudad).Include(f => f.Mes).Include(f => f.Pais).Include(f => f.Usuario);
            return View(facilitacions.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacion(Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                TempData["notice"] = "La Facilitación fue registrada correctamente";
            }
            return RedirectToAction("Index", "Facilitaciones");
        }

        // GET: Facilitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilitacion facilitacion = db.Facilitacions.Find(id);
            if (facilitacion == null)
            {
                return HttpNotFound();
            }
            return View(facilitacion);
        }

        // GET: Facilitaciones/Create
        public ActionResult Create()
        {
            ViewBag.AñoId = new SelectList(db.Año, "Id", "Anho");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.MesId = new SelectList(db.Meses, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View();
        }

        // POST: Facilitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,MesId,AñoId,PaisId,CiudadId,Fecha,FechaEdicion,Cantidad,Estadia,Observaciones")] Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AñoId = new SelectList(db.Año, "Id", "Anho", facilitacion.AñoId);
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
            ViewBag.MesId = new SelectList(db.Meses, "Id", "Nombre", facilitacion.MesId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", facilitacion.PaisId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", facilitacion.UserId);
            return View(facilitacion);
        }

        // GET: Facilitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilitacion facilitacion = db.Facilitacions.Find(id);
            if (facilitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.AñoId = new SelectList(db.Año, "Id", "Anho", facilitacion.AñoId);
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
            ViewBag.MesId = new SelectList(db.Meses, "Id", "Nombre", facilitacion.MesId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", facilitacion.PaisId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", facilitacion.UserId);
            return View(facilitacion);
        }

        // POST: Facilitaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,MesId,AñoId,PaisId,CiudadId,Fecha,FechaEdicion,Cantidad,Estadia,Observaciones")] Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AñoId = new SelectList(db.Año, "Id", "Anho", facilitacion.AñoId);
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
            ViewBag.MesId = new SelectList(db.Meses, "Id", "Nombre", facilitacion.MesId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", facilitacion.PaisId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", facilitacion.UserId);
            return View(facilitacion);
        }

        // GET: Facilitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilitacion facilitacion = db.Facilitacions.Find(id);
            if (facilitacion == null)
            {
                return HttpNotFound();
            }
            return View(facilitacion);
        }

        // POST: Facilitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facilitacion facilitacion = db.Facilitacions.Find(id);
            db.Facilitacions.Remove(facilitacion);
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
