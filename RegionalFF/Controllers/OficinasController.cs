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
    public class OficinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Oficinas
        public ActionResult Index()
        {
            return View(db.Oficinas.ToList());
        }

        // GET: Oficinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return HttpNotFound();
            }
            return View(oficina);
        }




        // GET: Oficinas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oficinas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Sigla,Departamento,Ciudad,Direccion,Telefono")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                db.Oficinas.Add(oficina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oficina);
        }

        // GET: Oficinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return HttpNotFound();
            }
            return View(oficina);
        }

        // POST: Oficinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Sigla,Departamento,Ciudad,Direccion,Telefono")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oficina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oficina);
        }

        // GET: Oficinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return HttpNotFound();
            }
            return View(oficina);
        }

        // POST: Oficinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oficina oficina = db.Oficinas.Find(id);
            db.Oficinas.Remove(oficina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public String getEmpresaUsers(string term)
        {
            var mostrar = db.Users.Where(r => r.Email.Trim() == term.Trim()).Select(c => c.Oficina.Nombre).FirstOrDefault();
            return mostrar;
        }

        public String getEmpresaSiglaUsers(string term)
        {
            var mostrar = db.Users.Where(r => r.Email.Trim() == term.Trim()).Select(c => c.Oficina.Sigla).FirstOrDefault();
            return mostrar;
        }

        //public String getUsersRoles(string term)
        //{
        //    var UserID = User.Identity.GetUserId();
        //    var userRoles = db.Roles.Include(r => r.Users).ToList();

        //    var userRoleNames = (from r in userRoles from u in r.Users where u.UserId == UserID select r.Name).ToList();

        //    return userRoleNames;
        //}

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
