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
    public class MesesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Meses
        public ActionResult Index()
        {
            return View(db.Meses.ToList());
        }

        // GET: Meses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meses meses = db.Meses.Find(id);
            if (meses == null)
            {
                return HttpNotFound();
            }
            return View(meses);
        }

        // GET: Meses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Activo")] Meses meses)
        {
            if (ModelState.IsValid)
            {
                db.Meses.Add(meses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meses);
        }

        // GET: Meses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meses meses = db.Meses.Find(id);
            if (meses == null)
            {
                return HttpNotFound();
            }
            return View(meses);
        }

        // POST: Meses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Activo")] Meses meses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meses);
        }

        // GET: Meses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meses meses = db.Meses.Find(id);
            if (meses == null)
            {
                return HttpNotFound();
            }
            return View(meses);
        }

        // POST: Meses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meses meses = db.Meses.Find(id);
            db.Meses.Remove(meses);
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
