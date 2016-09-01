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
    public class AccesosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accesos
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(db.MenuAdmins.ToList());
        }


        [ChildActionOnly]
        [Authorize(Roles = "Administrador")]
        public ActionResult MenuAdmin()
        {
            List<MenuAdmin> menuadminItems = db.MenuAdmins.Where(b => b.PadreId == 0).Where(b => b.Activo == true).ToList();

            //Get the menuItems collection from somewhere
            if (menuadminItems != null || menuadminItems.Count > 0)
            {
                return View(menuadminItems);
            }
            TempData["notice"] = "Listado de menús vacíos";
            return View();
        }


        // GET: Accesos/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuAdmin menuAdmin = db.MenuAdmins.Find(id);
            if (menuAdmin == null)
            {
                return HttpNotFound();
            }
            return View(menuAdmin);
        }

        // GET: Accesos/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accesos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PadreId,Nombre,Descripcion,Accion,Controlador,Activo")] MenuAdmin menuAdmin)
        {
            if (ModelState.IsValid)
            {
                db.MenuAdmins.Add(menuAdmin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuAdmin);
        }

        // GET: Accesos/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuAdmin menuAdmin = db.MenuAdmins.Find(id);
            if (menuAdmin == null)
            {
                return HttpNotFound();
            }
            return View(menuAdmin);
        }

        // POST: Accesos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PadreId,Nombre,Descripcion,Accion,Controlador,Activo")] MenuAdmin menuAdmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuAdmin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuAdmin);
        }

        // GET: Accesos/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuAdmin menuAdmin = db.MenuAdmins.Find(id);
            if (menuAdmin == null)
            {
                return HttpNotFound();
            }
            return View(menuAdmin);
        }

        // POST: Accesos/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuAdmin menuAdmin = db.MenuAdmins.Find(id);
            db.MenuAdmins.Remove(menuAdmin);
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
