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
    public class MenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menus
        public ActionResult Index()
        {
            return View(db.Menus.ToList());
        }

        [ChildActionOnly]
        public ActionResult MenuPrincipal()
        {
            //Administrador
            if (User.IsInRole("Administrador"))
            {
                List<Menu> menuItems = db.Menus.Where(b => b.PadreId == 0).Where(b => b.Activo == true).ToList();
                //Get the menuItems collection from somewhere
                if (menuItems != null || menuItems.Count > 0)
                {
                    return View(menuItems);
                }
                TempData["notice"] = "Listado de menús vacíos";
                return View();
            }
            else
            {
                //Facilitador o Fiscalizador
                String rol = "";
                if (User.IsInRole("Facilitador"))
                {
                    rol = "Facilitador";
                }
                else if (User.IsInRole("Fiscalizador"))
                {
                    rol = "Fiscalizador";
                }
                else
                {
                    rol = "-";
                }
                List<Menu> menuItems = db.Menus.Where(b => b.Perfil.Contains(rol)).Where(b => b.PadreId == 0).Where(b => b.Activo == true).ToList();
                //Get the menuItems collection from somewhere
                if (menuItems != null || menuItems.Count > 0)
                {
                    return View(menuItems);
                }
                TempData["notice"] = "Listado de menús vacíos";
                return View();
            }
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Id,PadreId,Nombre,Descripcion,Perfil,Accion,Controlador,Activo")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                TempData["notice"] = "El Menú " + menu.Nombre + " fue creado correctamente";
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        [Authorize(Roles = "Administrador")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PadreId,Nombre,Descripcion,Perfil,Accion,Controlador,Activo")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "El Menú " + menu.Nombre + " fue modificado correctamente";
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            TempData["notice"] = "El Menú " + menu.Nombre + " fue eliminado correctamente";
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
