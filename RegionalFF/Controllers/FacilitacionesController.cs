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
    public class FacilitacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Facilitaciones
        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult Index()
        {
            var Email = User.Identity.GetUserName();
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);

            return View(facilitacions.ToList().Where(u => u.Usuario.Email == Email).Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Day == Fecha.Day).Where(u => u.UserId == User.Identity.GetUserId()).OrderByDescending(f => f.Fecha));
        }

        // GET: Facilitaciones
        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InformeFecha(DateTime Desde, DateTime Hasta, bool Incluir = false)
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var UserName = User.Identity.GetUserName();
            ViewBag.User = User.Identity.GetUserName();
            ViewBag.Fecha = Fecha;
            ViewBag.Desde = Desde;
            ViewBag.Hasta = Hasta;
            var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);
            if (Incluir == true)
            {
                ViewBag.Filtro = "Todos los usuarios";
                return View(facilitacions.ToList().Where(u => u.Fecha >= Desde).Where(u => u.Fecha <= Hasta).OrderByDescending(f => f.Fecha));
            }
            else
            {
                ViewBag.Filtro = User.Identity.GetUserName();
                return View(facilitacions.ToList().Where(u => u.Usuario.Email == UserName).Where(u => u.Fecha >= Desde).Where(u => u.Fecha <= Hasta).OrderByDescending(f => f.Fecha));
            }
        }

        // GET: Facilitaciones
        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult Informe()
        {
            return View();
        }

        // GET: Facilitaciones
        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult VerTodo()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);

            return View(facilitacions.ToList().OrderByDescending(f => f.Fecha));
        }

        [Authorize(Roles = "Facilitador,Administrador")]
        // GET: Facilitaciones
        public ActionResult EsteMes()
        {
            var Email = User.Identity.GetUserName();
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);

            return View(facilitacions.ToList().Where(u => u.Usuario.Email == Email).Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Year == Fecha.Year).OrderByDescending(f => f.Fecha));
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        // GET: Facilitaciones
        public ActionResult EsteAño()
        {
            var Email = User.Identity.GetUserName();
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);

            return View(facilitacions.ToList().Where(u => u.Usuario.Email == Email).Where(u => u.Fecha.Year == Fecha.Year).OrderByDescending(f => f.Fecha));
        }

        public JsonResult ComprobarDuplicacion(string Nombre)
        {
            var data = db.Ciudads.Where(p => p.Nombre.Equals(Nombre, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (data != null)
            {
                return Json("Sorry, this name already exists", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult aja(Facilitacion facilitacion)
        {

            if (ModelState.IsValid)
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Facilitacions.Add(facilitacion);
                        db.SaveChanges();
                        TempData["notice"] = "La Facilitación fue registrada correctamente";
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        TempData["notice"] = "No se pudo realizar la transacción!" + ex.Message;
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Facilitaciones");
            }
            else
            {
                TempData["notice"] = "Error de Validaciones";
                return RedirectToAction("Index", "Facilitaciones");
            }
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacion(Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                facilitacion.Fecha = Fecha;
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                TempData["notice"] = "La Facilitación fue registrada correctamente";
            }
            else
            {
                TempData["notice"] = "Hubo un error y la Facilitación no fue registrada correctamente";
                return RedirectToAction("Index", "Facilitaciones");
            }
            return RedirectToAction("Index", "Facilitaciones");
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacionMes(Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                facilitacion.Fecha = Fecha;
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                TempData["notice"] = "La Facilitación fue registrada correctamente";
            }
            else
            {
                TempData["notice"] = "Hubo un error y la Facilitación no fue registrada correctamente";
                return RedirectToAction("EsteMes", "Facilitaciones");
            }
            return RedirectToAction("EsteMes", "Facilitaciones");
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacionAño(Facilitacion facilitacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                facilitacion.Fecha = Fecha;
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                TempData["notice"] = "La Facilitación fue registrada correctamente";
            }
            else
            {
                TempData["notice"] = "Hubo un error y la Facilitación no fue registrada correctamente";
                return RedirectToAction("EsteAño", "Facilitaciones");
            }
            return RedirectToAction("EsteAño", "Facilitaciones");
        }

        // GET: Facilitaciones/Details/5
        [Authorize(Roles = "Facilitador,Administrador")]
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

        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult Create()
        {
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");

            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View();
        }

        // POST: Facilitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Facilitador,Administrador")]
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

            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View(facilitacion);
        }


        // GET: Facilitaciones/Edit/5
        [Authorize(Roles = "Facilitador,Administrador")]
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

            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            return View(facilitacion);
        }

        // POST: Facilitaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Facilitador,Administrador")]
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


            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", facilitacion.CiudadId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", facilitacion.PaisId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", facilitacion.UserId);
            return View(facilitacion);
        }

        //Resumen Total Registro - Hoy por usuario
        public int? getTotalRegistroHoy(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Usuario.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return hoy;
        }

        //Resumen Total Registro - Hoy Todo
        public int? getTotalRegistroHoy()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return hoy;
        }


        //Resumen Total Registro - Este Mes por usuario
        public int? getTotalRegistroMes(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int mes = db.Facilitacions.Where(x => x.Usuario.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return mes;
        }

        //Resumen Total Registro - Este Año por usuario
        public int? getTotalRegistroAnio(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int anio = db.Facilitacions.Where(x => x.Usuario.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return anio;
        }

        //Resumen Total Registro - Este Año Todo
        public int? getTotalRegistroAnio()
        {
            DateTime Fecha = DateTime.Now;
            int anio = db.Facilitacions.Where(x => x.Fecha.Year == Fecha.Year).Count();
            return anio;
        }

        //Resumen Total Cantidad Registro - Hoy por usuario
        public int? getCantidadRegistroHoy(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Usuario.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.Cantidad);
            return hoy;
        }


        //Resumen Total Cantidad Registro Hoy
        public int? getCantidadRegistroHoy()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.Cantidad);
            return hoy;
        }



        //Resumen Total Registro Este Mes - por usuario
        public int? getCantidadRegistroMes(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Usuario.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.Cantidad);
            return hoy;
        }


        //Resumen Total Registro Este Mes - Todo
        public int? getCantidadRegistroMes()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.Cantidad);
            return hoy;
        }


        //Resumen Total Registro Este Año -  por usuario
        public int? getCantidadRegistroAnio(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Usuario.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.Cantidad);
            return hoy;
        }


        //Resumen Total Registro Este Año -  Todo
        public int? getCantidadRegistroAnio()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Facilitacions.Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.Cantidad);
            return hoy;
        }


        // GET: Facilitaciones/Delete/5
        [Authorize(Roles = "Facilitador,Administrador")]
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


        [Authorize(Roles = "Facilitador,Administrador")]
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
