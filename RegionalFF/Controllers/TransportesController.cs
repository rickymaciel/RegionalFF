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
    public class TransportesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transportes
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Index()
        {
            var transportes = db.Transportes.Include(t => t.Conductor).Include(t => t.Marca);
            return View(transportes.ToList());
        }


        [HttpPost]
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroTransporte(Transporte transporte)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Transportes.Add(transporte);
                    db.SaveChanges();
                    TempData["notice"] = "El Transporte fue registrado correctamente";
                    return RedirectToAction("Index", "Fiscalizaciones");
                }
                else
                {
                    TempData["notice"] = "Error de Validaciones";
                    return RedirectToAction("Index", "Fiscalizaciones");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = "Error " + ex + " ";
                throw;
            }
        }

        // GET: Transportes/Details/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = db.Transportes.Find(id);
            if (transporte == null)
            {
                return HttpNotFound();
            }
            return View(transporte);
        }

        // GET: Transportes/Create
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Create()
        {
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            return View();
        }

        // POST: Transportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Create([Bind(Include = "Id,RazonSocial,ConductorId,MarcaId,ChapaNro,Activo")] Transporte transporte)
        {
            if (ModelState.IsValid)
            {
                db.Transportes.Add(transporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre", transporte.ConductorId);
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre", transporte.MarcaId);
            return View(transporte);
        }

        // GET: Transportes/Edit/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = db.Transportes.Find(id);
            if (transporte == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre", transporte.ConductorId);
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre", transporte.MarcaId);
            return View(transporte);
        }

        // POST: Transportes/Edit/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RazonSocial,ConductorId,MarcaId,ChapaNro,Activo")] Transporte transporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre", transporte.ConductorId);
            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre", transporte.MarcaId);
            return View(transporte);
        }

        // GET: Transportes/Delete/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = db.Transportes.Find(id);
            if (transporte == null)
            {
                return HttpNotFound();
            }
            return View(transporte);
        }

        [Authorize(Roles = "Fiscalizador,Administrador")]
        // POST: Transportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transporte transporte = db.Transportes.Find(id);
            db.Transportes.Remove(transporte);
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
