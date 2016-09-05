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
    public class AñosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Años
        public ActionResult Index()
        {
            return View(db.Año.ToList());
        }

        // GET: Años/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Año año = db.Año.Find(id);
            if (año == null)
            {
                return HttpNotFound();
            }
            return View(año);
        }

        // GET: Años/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Años/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Anho,Activo")] Año año)
        {
            if (ModelState.IsValid)
            {
                db.Año.Add(año);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(año);
        }

        // GET: Años/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Año año = db.Año.Find(id);
            if (año == null)
            {
                return HttpNotFound();
            }
            return View(año);
        }

        // POST: Años/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Anho,Activo")] Año año)
        {
            if (ModelState.IsValid)
            {
                db.Entry(año).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(año);
        }

        // GET: Años/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Año año = db.Año.Find(id);
            if (año == null)
            {
                return HttpNotFound();
            }
            return View(año);
        }

        // POST: Años/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Año año = db.Año.Find(id);
            db.Año.Remove(año);
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
