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
    public class PaisesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paises
        public ActionResult Index()
        {
            return View(db.Pais.ToList().OrderBy(x => x.Nombre));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroPais(Pais pais)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Pais.Add(pais);
                    db.SaveChanges();
                    TempData["notice"] = "El pais de origen fue registrado correctamente";
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroPaisFiscalizacion(Pais pais)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Pais.Add(pais);
                    db.SaveChanges();
                    TempData["notice"] = "El pais de origen fue registrado correctamente";
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
        // GET: Paises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                db.Pais.Add(pais);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pais);
        }

        // GET: Paises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pais).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        // GET: Paises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                return HttpNotFound();
            }
            return View(pais);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pais pais = db.Pais.Find(id);
            db.Pais.Remove(pais);
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
