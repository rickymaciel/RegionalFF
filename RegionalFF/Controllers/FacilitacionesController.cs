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

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");

            var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);
            return View(facilitacions.ToList());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacion(Facilitacion facilitacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Facilitacions.Add(facilitacion);
                    db.SaveChanges();
                    TempData["notice"] = "La Facilitación fue registrada correctamente";
                    return RedirectToAction("Index", "Facilitaciones");
                }
                else
                {
                    TempData["notice"] = "Error de Validaciones";
                    return RedirectToAction("Index", "Facilitaciones");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = "Error " + ex + " ";
                throw;
            }
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
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View();
        }

        // POST: Facilitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Fecha,PaisId,CiudadId,Cantidad,Estadia,Observaciones")] Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
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
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", facilitacion.PaisId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", facilitacion.UserId);
            return View(facilitacion);
        }

        // POST: Facilitaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Fecha,PaisId,CiudadId,Cantidad,Estadia,Observaciones")] Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
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
