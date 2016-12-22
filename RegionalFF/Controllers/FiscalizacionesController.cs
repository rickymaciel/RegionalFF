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
using ClosedXML.Excel;

namespace RegionalFF.Controllers
{
    public class FiscalizacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Fiscalizador,Administrador")]
        // GET: Fiscalizaciones
        public ActionResult Index()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            var fiscalizacions = db.Fiscalizacions.Include(f => f.Ciudad).Include(f => f.Fiscal).Include(f => f.Pais).Include(f => f.Transporte);
            return View(fiscalizacions.ToList().Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Day == Fecha.Day).Where(u => u.UserId == User.Identity.GetUserId()).OrderByDescending(f => f.Fecha));
        }

        // GET: Fiscalizaciones
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult VerTodo()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            var fiscalizaciones = db.Fiscalizacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Fiscal);

            return View(fiscalizaciones.ToList().OrderByDescending(f => f.Fecha));
        }

        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult EsteMes()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            var fiscalizacions = db.Fiscalizacions.Include(f => f.Ciudad).Include(f => f.Fiscal).Include(f => f.Pais).Include(f => f.Transporte);
            return View(fiscalizacions.ToList().Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.UserId == User.Identity.GetUserId()).OrderByDescending(f => f.Fecha));
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult EsteAño()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            ViewBag.MarcaId = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.ConductorId = new SelectList(db.Conductors, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Numero");
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            var fiscalizacions = db.Fiscalizacions.Include(f => f.Ciudad).Include(f => f.Fiscal).Include(f => f.Pais).Include(f => f.Transporte);
            return View(fiscalizacions.ToList().Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.UserId == User.Identity.GetUserId()).OrderByDescending(f => f.Fecha));
        }

        [Authorize(Roles = "Fiscalizador,Administrador")]
        // GET: Fiscalizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            if (fiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(fiscalizacion);
        }

        [Authorize(Roles = "Fiscalizador,Administrador")]
        // GET: Fiscalizaciones/Create
        public ActionResult Create()
        {
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial");

            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View();
        }

        // POST: Fiscalizaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Fecha,TransporteId,CiudadId,PaisId,CantidadPasajeros,Observaciones,Habilitado,Activo")] Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                fiscalizacion.Activo = true;
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);
            return View(fiscalizacion);
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacion(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                fiscalizacion.Fecha = Fecha;
                fiscalizacion.Activo = true;
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            else
            {
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "error";
                TempData["notice"] = "Hubo un error y la Fiscalización no fue registrada correctamente";
                return RedirectToAction("Index", "Fiscalizaciones");
            }
            return RedirectToAction("Index", "Fiscalizaciones");
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacionMes(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                fiscalizacion.Fecha = Fecha;
                fiscalizacion.Activo = true;
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            else
            {
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "error";
                TempData["notice"] = "Hubo un error y la Fiscalización no fue registrada correctamente";
                return RedirectToAction("EsteMes", "Fiscalizaciones");
            }
            return RedirectToAction("EsteMes", "Fiscalizaciones");
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacionAño(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                fiscalizacion.Fecha = Fecha;
                fiscalizacion.Activo = true;
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            else
            {
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "error";
                TempData["notice"] = "Hubo un error y la Fiscalización no fue registrada correctamente";
                return RedirectToAction("EsteAño", "Fiscalizaciones");
            }
            return RedirectToAction("EsteAño", "Fiscalizaciones");
        }


        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFiscalizacionTodo(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                fiscalizacion.Fecha = Fecha;
                fiscalizacion.Activo = true;
                db.Fiscalizacions.Add(fiscalizacion);
                db.SaveChanges();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Fiscalización fue registrada correctamente";
            }
            else
            {
                TempData["title"] = "Operación cancelada";
                TempData["type"] = "error";
                TempData["notice"] = "Hubo un error y la Fiscalización no fue registrada";
                return RedirectToAction("VerTodo", "Fiscalizaciones");
            }
            return RedirectToAction("VerTodo", "Fiscalizaciones");
        }


        // GET: Fiscalizaciones/Edit/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            fiscalizacion.UserId = User.Identity.GetUserId();
            fiscalizacion.Fecha = Fecha;
            if (fiscalizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);

            ViewBag.User = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;
            return View(fiscalizacion);
        }

        // POST: Fiscalizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Fecha,TransporteId,CiudadId,PaisId,CantidadPasajeros,Observaciones,Habilitado,Activo")] Fiscalizacion fiscalizacion)
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            fiscalizacion.Fecha = Fecha;
            if (ModelState.IsValid)
            {
                db.Entry(fiscalizacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Fiscalización fue modificada correctamente";
                return RedirectToAction("Index");
            }
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre", fiscalizacion.CiudadId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", fiscalizacion.UserId);
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre", fiscalizacion.PaisId);
            ViewBag.TransporteId = new SelectList(db.Transportes, "Id", "RazonSocial", fiscalizacion.TransporteId);
            return View(fiscalizacion);
        }

        // GET: Fiscalizaciones/Delete/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            if (fiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(fiscalizacion);
        }

        // POST: Fiscalizaciones/Delete/5
        [Authorize(Roles = "Fiscalizador,Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fiscalizacion fiscalizacion = db.Fiscalizacions.Find(id);
            db.Fiscalizacions.Remove(fiscalizacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Total de registro Hoy por usuario
        public int? getTotalRegistroHoyUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return hoy;
        }

        //Total de registro Hoy Global
        public int? getTotalRegistroHoyGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return hoy;
        }


        //Total de registro Mes Usuario
        public int? getTotalRegistroMesUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int mes = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return mes;
        }

        //Total de registro Mes Global
        public int? getTotalRegistroMesGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int mes = db.Fiscalizacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return mes;
        }


        //Total de registro Año Usuario
        public int? getTotalRegistroAnioUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int anio = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).Count();
            return anio;
        }

        //Total de registro Año Global
        public int? getTotalRegistroAnioGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int anio = db.Fiscalizacions.Where(x => x.Fecha.Year == Fecha.Year).Count();
            return anio;
        }

        //Total de registro Año Usuario
        public int? getTotalBusesNotificadosHoyUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }

        //Total de registro Año Global
        public int? getTotalBusesNotificadosHoyGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }


        //Total de registro Año Usuario
        public int? getTotalBusesNotificadosMesUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }

        //Total de registro Año Global
        public int? getTotalBusesNotificadosMesGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }

        //Total de registro Año Usuario
        public int? getTotalBusesNotificadosAnioUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }

        //Total de registro Año Global
        public int? getTotalBusesNotificadosAnioGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Year == Fecha.Year).Where(x => x.Habilitado == false).Count();
            return hoy;
        }
        //Total de pasajeros Hoy Global
        public int? getTotalPasajerosHoyGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }

        //Total de pasajeros Hoy Usuario
        public int? getTotalPasajerosHoyUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }

        //Total de pasajeros Hoy Usuario
        public int? getCantidadRegistroHoyUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Day == Fecha.Day).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        //Total de pasajeros Este Mes Usuario
        public int? getCantidadRegistroMesUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }

        //Total de pasajeros Este Mes Global
        public int? getCantidadRegistroMesGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }


        //Total de pasajeros Este año Usuario
        public int? getCantidadRegistroAnioUsuario(String usuario)
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fiscal.Email == usuario).Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
        }

        public ActionResult GenerarReporteHoy()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var usuario = RegionalFF.Values["usuario"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Fiscalizacion.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "FISCALIZACION " + Fecha.Day + "/" + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year + " (" + usuario + ")";
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var fiscalizaciones = db.Fiscalizacions.Where(u => u.UserId == UserId).Where(u => u.Fecha.Year == Fecha.Year)
                .Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Day == Fecha.Day)
                .Select(g => new { Fecha = g.Fecha, RazonSocial = g.Transporte.RazonSocial, ChapaNro = g.Transporte.ChapaNro, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Cantidad = g.CantidadPasajeros, Fiscal = g.Fiscal.Nombre + " " + g.Fiscal.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = fiscalizaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "FISCALIZACION_" + usuario + "_" + Fecha.Day + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year + "_" + usuario;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Fiscalizaciones");
        }

        //Reporte Mes (usuario)
        public ActionResult GenerarReporteEsteMes()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var usuario = RegionalFF.Values["usuario"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Fiscalizacion.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "FISCALIZACION " + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year + " (" + usuario + ")";
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var fiscalizaciones = db.Fiscalizacions.Where(u => u.UserId == UserId).Where(u => u.Fecha.Year == Fecha.Year)
                 .Where(u => u.Fecha.Month == Fecha.Month)
                 .Select(g => new { Fecha = g.Fecha, RazonSocial = g.Transporte.RazonSocial, ChapaNro = g.Transporte.ChapaNro, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Cantidad = g.CantidadPasajeros, Fiscal = g.Fiscal.Nombre + " " + g.Fiscal.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = fiscalizaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year + "_" + usuario;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Fiscalizaciones");
        }

        //Reporte Año (usuario)
        public ActionResult GenerarReporteAño()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var usuario = RegionalFF.Values["usuario"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Fiscalizacion.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Year + " (" + usuario + ")";
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var fiscalizaciones = db.Fiscalizacions.Where(u => u.UserId == UserId).Where(u => u.Fecha.Year == Fecha.Year)
                 .Select(g => new { Fecha = g.Fecha, RazonSocial = g.Transporte.RazonSocial, ChapaNro = g.Transporte.ChapaNro, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Cantidad = g.CantidadPasajeros, Fiscal = g.Fiscal.Nombre + " " + g.Fiscal.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = fiscalizaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "FISCALIZACION_" + Fecha.Year + "_" + usuario;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Fiscalizaciones");
        }

        //Reporte Hoy (todos)
        public ActionResult GenerarReporteHoyTodos()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Fiscalizacion.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Day + "/" + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var fiscalizaciones = db.Fiscalizacions.Where(u => u.Fecha.Year == Fecha.Year)
                .Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Day == Fecha.Day)
                .Select(g => new { Fecha = g.Fecha, RazonSocial = g.Transporte.RazonSocial, ChapaNro = g.Transporte.ChapaNro, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Cantidad = g.CantidadPasajeros, Fiscal = g.Fiscal.Nombre + " " + g.Fiscal.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = fiscalizaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "FISCALIZACION_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Fiscalizaciones");
        }


        //Reporte Mes (todos)
        public ActionResult GenerarReporteMesTodos()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Fiscalizacion.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var fiscalizaciones = db.Fiscalizacions.Where(u => u.Fecha.Year == Fecha.Year)
                .Where(u => u.Fecha.Month == Fecha.Month)
                .Select(g => new { Fecha = g.Fecha, RazonSocial = g.Transporte.RazonSocial, ChapaNro = g.Transporte.ChapaNro, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Cantidad = g.CantidadPasajeros, Fiscal = g.Fiscal.Nombre + " " + g.Fiscal.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = fiscalizaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "FISCALIZACION_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Fiscalizaciones");
        }

        //Reporte Año (todos)
        public ActionResult GenerarReporteAñoTodos()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Fiscalizacion.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var fiscalizaciones = db.Fiscalizacions.Where(u => u.Fecha.Year == Fecha.Year)
               .Select(g => new { Fecha = g.Fecha, RazonSocial = g.Transporte.RazonSocial, ChapaNro = g.Transporte.ChapaNro, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Cantidad = g.CantidadPasajeros, Fiscal = g.Fiscal.Nombre + " " + g.Fiscal.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = fiscalizaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "FISCALIZACION_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Fiscalizaciones");
        }

        //Total de pasajeros Este año Global
        public int? getCantidadRegistroAnioGlobal()
        {
            DateTime Fecha = DateTime.Now;
            int hoy = db.Fiscalizacions.Where(x => x.Fecha.Year == Fecha.Year).DefaultIfEmpty().Sum(x => x == null ? 0 : x.CantidadPasajeros);
            return hoy;
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
