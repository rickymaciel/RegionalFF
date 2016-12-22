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
using System.IO;

namespace RegionalFF.Controllers
{
    public class FacilitacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void CargarSesion()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            if (RegionalFF != null)
            {
                var imagen = RegionalFF.Values["Imagen"];
                var archivo = Path.GetFileName(imagen);
                var path = Path.Combine(Server.MapPath("~/Content/img/Uploads/Usuarios/Thumbnail/" + archivo), archivo + ".png");
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                ViewBag.ImagenUsuarioCurrent = imageDataURL;
                TempData["imageDataURL"] = imageDataURL;

            }
        }

        // GET: Facilitaciones
        public ActionResult Index()
        {
            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            DateTime Fecha = DateTime.Now;
            var UserId = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            CargarSesion();
            //var facilitacions = db.Facilitacions.Include(f => f.Ciudad).Include(f => f.Pais).Include(f => f.Usuario);
            return View(db.Facilitacions.ToList().Where(u => u.UserId == UserId).Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Day == Fecha.Day).OrderByDescending(f => f.Fecha));
        }

        public ActionResult Generado()
        {
            TempData["path"] = TempData["path"];
            TempData["archivo"] = TempData["archivo"];
            TempData["Desde"] = TempData["Desde"];
            TempData["Hasta"] = TempData["Hasta"];
            return View();
        }

        //Reporte Hoy (usuario)
        public ActionResult GenerarReporteHoy()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var usuario = RegionalFF.Values["usuario"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Day + "/" + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year + " (" + usuario + ")";
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.UserId == UserId).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Day == Fecha.Day).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + usuario + "_" + Fecha.Day + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year + "_" + usuario;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Facilitaciones");
        }

        //Reporte Mes (usuario)
        public ActionResult GenerarReporteEsteMes()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var usuario = RegionalFF.Values["usuario"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year + " (" + usuario + ")";
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.UserId == UserId).Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Month == Fecha.Month).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year + "_" + usuario ;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Facilitaciones");
        }

        //Reporte Año (usuario)
        public ActionResult GenerarReporteAño()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var usuario = RegionalFF.Values["usuario"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Year + " (" + usuario + ")";
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.Fecha.Year == Fecha.Year).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.Year + "_" + usuario;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Facilitaciones");
        }

        //Reporte Hoy (todos)
        public ActionResult GenerarReporteHoyTodos()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Day + "/" + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Month == Fecha.Month).Where(u => u.Fecha.Day == Fecha.Day).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Facilitaciones");
        }


        //Reporte Mes (todos)
        public ActionResult GenerarReporteMesTodos()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.ToString("MMMM").ToUpper() + "/" + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Month == Fecha.Month).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Facilitaciones");
        }

        //Reporte Año (todos)
        public ActionResult GenerarReporteAñoTodos()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.Fecha.Year == Fecha.Year).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("Index", "Facilitaciones");
        }


        public ActionResult GenerarReporte10()
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.ToString("MMMM").ToUpper() + " " + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            var facilitaciones = db.Facilitacions.Where(u => u.Fecha.Year == Fecha.Year).Where(u => u.Fecha.Month == Fecha.Month).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell("A12").Value = facilitaciones;
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            return RedirectToAction("PaisesPrincipales10", "Facilitaciones");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FiltrarReporte(DateTime Desde, DateTime Hasta, bool Incluir)
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            var UserId = User.Identity.GetUserId();
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES " + Fecha.ToString("MMMM").ToUpper() + " " + Fecha.Year;
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            if (Incluir == true)
            {
                ViewBag.Filtro = "Todos los usuarios";
                var facilitaciones = db.Facilitacions.Where(u => u.Fecha >= Desde).Where(u => u.Fecha <= Hasta).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
                worksheet.Cell("A12").Value = facilitaciones;
            }
            else
            {
                ViewBag.Filtro = User.Identity.GetUserId();
                var facilitaciones = db.Facilitacions.Where(u => u.UserId == UserId).Where(u => u.Fecha >= Desde).Where(u => u.Fecha <= Hasta).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
                worksheet.Cell("A12").Value = facilitaciones;
            }
            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Fecha.ToString("MMMM").ToUpper() + "_" + Fecha.Year;
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            ViewBag.Desde = Desde;
            ViewBag.Hasta = Hasta;
            return RedirectToAction("PaisesPrincipales10", "Facilitaciones");
        }

        public ActionResult ResultadoReporte(DateTime Desde,DateTime Hasta)
        {
            HttpCookie RegionalFF = Request.Cookies["RegionalFF"];
            DateTime Fecha = DateTime.Now;
            var Plantilla = Server.MapPath("~/Content/Archivos/Plantilla/Plantilla.xlsx");
            var workbook = new XLWorkbook(Plantilla);
            var worksheet = workbook.Worksheets.Worksheet(1);
            worksheet.Cell("A7").Value = "REGISTRO DE VISITANTES - Desde: " + Desde.ToString("dd/MMMM/yyyy").ToUpper() + " Hasta: " + Hasta.ToString("dd/MMMM/yyyy").ToUpper();
            worksheet.Cell("A8").Value = RegionalFF.Values["Oficina"];
            worksheet.Cell("A9").Value = RegionalFF.Values["Ciudad"] + " - " + RegionalFF.Values["Departamento"];
            ViewBag.Filtro = "Todos los usuarios";
            var facilitaciones = db.Facilitacions.Where(u => u.Fecha >= Desde).Where(u => u.Fecha <= Hasta).Select(g => new { Fecha = g.Fecha, Cantidad = g.Cantidad, Origen = g.Pais.Nombre, Destino = g.Ciudad.Nombre, Estadia = g.Estadia, Usuario = g.Usuario.Nombre + " " + g.Usuario.Apellido }).OrderBy(x => x.Fecha);
            worksheet.Cell("A12").Value = facilitaciones;

            var rngDates = worksheet.Range("A12:A582");
            rngDates.Style.DateFormat.Format = "dd/MM/yyyy";
            var archivo = "REGISTRO_DE_VISITANTES_" + Desde.ToString("dd-MMMM-yyyy").ToUpper() + "_" + Hasta.ToString("dd-MMMM-yyyy").ToUpper();
            var Exportar = Server.MapPath("~/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx");
            var Descargar = "http://localhost:2491/Content/Archivos/Plantilla/Exportar/" + archivo + ".xlsx";
            workbook.SaveAs(Exportar);
            var path = Descargar;
            TempData["path"] = path;
            TempData["archivo"] = archivo;
            TempData["Desde"] = Desde.ToString("dd/MMMM/yyyy").ToUpper();
            TempData["Hasta"] = Hasta.ToString("dd/MMMM/yyyy").ToUpper();
            return RedirectToAction("Generado", "Facilitaciones");
        }

        // GET: Facilitaciones
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InformeFecha(DateTime Desde, DateTime Hasta, bool Incluir)
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

        public List<String> getPaises(DateTime Desde, DateTime Hasta, bool Incluir = false)
        {
            if (Incluir == true)
            {
                var paises = (from s in db.Facilitacions where s.Fecha >= Desde where s.Fecha <= Hasta select s.Ciudad.Nombre).Distinct().ToList();
                return paises.ToList();
            }
            else
            {
                var paises = (from s in db.Facilitacions where s.Fecha >= Desde where s.Fecha <= Hasta select s.Ciudad.Nombre).Distinct().ToList();
                return paises.ToList();
            }
            
        }

        // GET: Facilitaciones
        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult Busqueda()
        {
            return View();
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resultados(DateTime Desde, DateTime Hasta, bool Incluir = false)
        {
            var UserId = User.Identity.GetUserId();
            var i = 0;
            List<VisitSource> paisesLista = new List<VisitSource>();
            List<String> color = new List<String>();
            color.Add("#FF0F00");
            color.Add("#FF6600");
            color.Add("#FF9E01");
            color.Add("#FCD202");
            color.Add("#F8FF01");

            color.Add("#B0DE09");
            color.Add("#04D215");
            color.Add("#0D8ECF");
            color.Add("#0D52D1");
            color.Add("#2A0CD0");

            //agregar mas colores
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            //
            if (Incluir == true)
            {
                ViewBag.Filtro = "Todos los usuarios";
                var paisesQuery = db.Facilitacions.Where(x => x.Fecha >= Desde).Where(x => x.Fecha <= Hasta).GroupBy(x => x.Pais.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Pais.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).ToList();
                foreach (var item in paisesQuery)
                {
                    paisesLista.Add(new VisitSource()
                    {
                        name = item.Nombre,
                        value = item.Cantidad.ToString(),
                        color = color[i],
                    });
                    i++;
                }
            }
            else
            {
                ViewBag.Filtro = User.Identity.GetUserName();
                var paisesQuery = db.Facilitacions.Where(x => x.Usuario.Id == UserId).Where(x => x.Fecha >= Desde).Where(x => x.Fecha <= Hasta).GroupBy(x => x.Pais.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Pais.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).ToList();
                foreach (var item in paisesQuery)
                {
                    paisesLista.Add(new VisitSource()
                    {
                        name = item.Nombre,
                        value = item.Cantidad.ToString(),
                        color = color[i],
                    });
                    i++;
                }
            }

            var pie = new PieMapViewModel()
            {
                LegendData = getPaises(Desde,Hasta,Incluir),
                SeriesData = paisesLista
            };

            ViewBag.LegendData = pie.LegendData;
            ViewBag.SeriesData = pie.SeriesData;

            ViewBag.Desde = Desde;
            ViewBag.Hasta = Hasta;
            return View();
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultadosDestinos(DateTime Desde, DateTime Hasta, bool Incluir = false)
        {
            var UserId = User.Identity.GetUserId();
            var i = 0;
            List<VisitSource> paisesLista = new List<VisitSource>();
            List<String> color = new List<String>();
            color.Add("#FF0F00");
            color.Add("#FF6600");
            color.Add("#FF9E01");
            color.Add("#FCD202");
            color.Add("#F8FF01");

            color.Add("#B0DE09");
            color.Add("#04D215");
            color.Add("#0D8ECF");
            color.Add("#0D52D1");
            color.Add("#2A0CD0");

            //agregar mas colores
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            color.Add("#2A0CD0");
            //
            if (Incluir == true)
            {
                ViewBag.Filtro = "Todos los usuarios";
                var paisesQuery = db.Facilitacions.Where(x => x.Fecha >= Desde).Where(x => x.Fecha <= Hasta).GroupBy(x => x.Ciudad.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Ciudad.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).ToList();
                foreach (var item in paisesQuery)
                {
                    paisesLista.Add(new VisitSource()
                    {
                        name = item.Nombre,
                        value = item.Cantidad.ToString(),
                        color = color[i],
                    });
                    i++;
                }
            }
            else
            {
                ViewBag.Filtro = User.Identity.GetUserName();
                var paisesQuery = db.Facilitacions.Where(x => x.Usuario.Id == UserId).Where(x => x.Fecha >= Desde).Where(x => x.Fecha <= Hasta).GroupBy(x => x.Ciudad.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Ciudad.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).ToList();
                foreach (var item in paisesQuery)
                {
                    paisesLista.Add(new VisitSource()
                    {
                        name = item.Nombre,
                        value = item.Cantidad.ToString(),
                        color = color[i],
                    });
                    i++;
                }
            }

            var pie = new PieMapViewModel()
            {
                LegendData = getPaises(Desde, Hasta, Incluir),
                SeriesData = paisesLista
            };

            ViewBag.LegendData = pie.LegendData;
            ViewBag.SeriesData = pie.SeriesData;

            ViewBag.Desde = Desde;
            ViewBag.Hasta = Hasta;
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


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AjaxRegistroFacilitacion1(Facilitacion facilitacion)
        //{
        //    facilitacion.Fecha = DateTime.Now;
        //    if (ModelState.IsValid)
        //    {
        //        using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                db.Facilitacions.Add(facilitacion);
        //                db.SaveChanges();
        //                dbTran.Commit();
        //                ActualizarCookiesFacilitacion();
        //                TempData["notice"] = "La Facilitación fue registrada correctamente";
        //            }
        //            catch (Exception ex)
        //            {
        //                dbTran.Rollback();
        //                TempData["notice"] = "No se pudo realizar la transacción!" + ex.Message;
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return RedirectToAction("Index", "Facilitaciones");
        //    }
        //    else
        //    {
        //        TempData["notice"] = "No se pudo realizar la transacción! Complete los campos requeridos y vuelva a intentar!";
        //        return RedirectToAction("Index", "Facilitaciones");
        //    }
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacion(Facilitacion facilitacion)
        {
            facilitacion.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                ActualizarCookiesFacilitacion();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Facilitación fue registrada correctamente";
                return RedirectToAction("Index", "Facilitaciones");
            }
            else
            {
                TempData["title"] = "Operación cancelada";
                TempData["type"] = "error";
                TempData["notice"] = "Por favor compruebe que los campos requeridos esten ingresados y vuelva a intentarlo";
                return RedirectToAction("Index", "Facilitaciones");
            }
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacionMes(Facilitacion facilitacion)
        {
            facilitacion.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                ActualizarCookiesFacilitacion();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Facilitación fue registrada correctamente";
                return RedirectToAction("EsteMes", "Facilitaciones");
            }
            else
            {
                TempData["title"] = "Operación cancelada";
                TempData["type"] = "error";
                TempData["notice"] = "Por favor compruebe que los campos requeridos esten ingresados y vuelva a intentarlo";
                return RedirectToAction("EsteMes", "Facilitaciones");
            }
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacionAño(Facilitacion facilitacion)
        {
            facilitacion.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                ActualizarCookiesFacilitacion();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Facilitación fue registrada correctamente";
                return RedirectToAction("EsteAño", "Facilitaciones");
            }
            else
            {
                TempData["title"] = "Operación cancelada";
                TempData["type"] = "error";
                TempData["notice"] = "Por favor compruebe que los campos requeridos esten ingresados y vuelva a intentarlo";
                return RedirectToAction("EsteAño", "Facilitaciones");
            }
        }

        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxRegistroFacilitacionTodo(Facilitacion facilitacion)
        {
            facilitacion.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Facilitacions.Add(facilitacion);
                db.SaveChanges();
                ActualizarCookiesFacilitacion();
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Facilitación fue registrada correctamente";
                return RedirectToAction("VerTodo", "Facilitaciones");
            }
            else
            {
                TempData["title"] = "Operación cancelada";
                TempData["type"] = "error";
                TempData["notice"] = "Por favor compruebe que los campos requeridos esten ingresados y vuelva a intentarlo";
                return RedirectToAction("VerTodo", "Facilitaciones");
            }
        }


        #region public bool ActualizarCookiesFacilitacion()
        public bool ActualizarCookiesFacilitacion()
        {
            HttpCookie RegionalFF = HttpContext.Request.Cookies["RegionalFF"];
            RegionalFF.Values["CantRegistroHoy"] = getCantidadRegistroHoy().ToString();
            RegionalFF.Values["CantTotalMes"] = getCantidadRegistroMes().ToString();
            RegionalFF.Values["CantTotalAnio"] = getCantidadRegistroAnio().ToString();
            //RegionalFF.Expires.Add(TimeSpan.FromDays(1.0));
            Response.Cookies.Add(RegionalFF);
            return true;
        }
        #endregion


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
            ViewBag.User = User.Identity.GetUserId();
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
                DateTime Fecha = DateTime.Now;
                if (Fecha.Year == facilitacion.Fecha.Year && Fecha.Month == facilitacion.Fecha.Month && Fecha.Day == facilitacion.Fecha.Day)
                {
                    db.Entry(facilitacion).State = EntityState.Modified;
                    db.SaveChanges();
                    ActualizarCookiesFacilitacion();
                    TempData["title"] = "Operación exitosa";
                    TempData["type"] = "success";
                    TempData["notice"] = "La Facilitación fue modificada correctamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["title"] = "Operación cancelada";
                    TempData["type"] = "notice";
                    TempData["notice"] = "La Facilitación ya no puede ser modificada";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.User = User.Identity.GetUserId();
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

        //Informes Paises Principales
        [Authorize(Roles = "Facilitador,Administrador")]
        public List<String> getPrincipalesPaisesMes()
        {
            DateTime Fecha = DateTime.Now;
            var scopeNames = (from s in db.Facilitacions where s.Fecha.Month == Fecha.Month where s.Fecha.Year == Fecha.Year select s.Pais.Nombre).Distinct().ToList();
            return scopeNames.ToList();
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        public List<String> getPrincipalesPaisesAño()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var scopeNames = (from s in db.Facilitacions where s.Fecha.Year == Fecha.Year select s.Pais.Nombre).Distinct().ToList();
            return scopeNames.ToList();
        }

        //[Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult PaisesPrincipales10()
        {
            DateTime Fecha = DateTime.Now;
            var paisesQuery = db.Facilitacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).GroupBy(x => x.Pais.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Pais.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).Take(10).ToList();
            
            var i = 0;

            List<VisitSource> paisesLista = new List<VisitSource>();
            List<String> color = new List<String>();
            color.Add("#FF0F00");
            color.Add("#FF6600");
            color.Add("#FF9E01");
            color.Add("#FCD202");
            color.Add("#F8FF01");

            color.Add("#B0DE09");
            color.Add("#04D215");
            color.Add("#0D8ECF");
            color.Add("#0D52D1");
            color.Add("#2A0CD0");

            foreach (var item in paisesQuery)
            {
                paisesLista.Add(new VisitSource()
                {
                    name = item.Nombre,
                    value = item.Cantidad.ToString(),
                    color = color[i],
                });
                i++;
            }


            var pie = new PieMapViewModel()
            {
                LegendData = getPrincipalesPaisesMes(),
                SeriesData = paisesLista
            };

            ViewBag.LegendData = pie.LegendData;
            ViewBag.SeriesData = pie.SeriesData;

            ViewBag.Mes = Fecha.ToString("MMMM");
            ViewBag.Año = Fecha.Year;


            ViewBag.CiudadId = new SelectList(db.Ciudads, "Id", "Nombre");
            ViewBag.PaisId = new SelectList(db.Pais, "Id", "Nombre");
            var UserId = User.Identity.GetUserId();
            ViewBag.Fecha = Fecha;

            return View();
        }

        // GET: Facilitaciones
        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult FiltroFacilitacion()
        {
            return View();
        }

        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult FiltroFacilitaciones(DateTime Desde, DateTime Hasta)
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //Consultas a la base de datos
            var paisesQuery = db.Facilitacions.Where(x => x.Fecha.Month >= Desde.Month).Where(x => x.Fecha.Year >= Desde.Year).Where(x => x.Fecha.Month <= Hasta.Month).Where(x => x.Fecha.Year <= Hasta.Year).GroupBy(x => x.Pais.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Pais.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).Take(10).ToList();

            var i = 0;

            List<VisitSource> paisesLista = new List<VisitSource>();
            List<String> color = new List<String>();
            color.Add("#FF0F00");
            color.Add("#FF6600");
            color.Add("#FF9E01");
            color.Add("#FCD202");
            color.Add("#F8FF01");

            color.Add("#B0DE09");
            color.Add("#04D215");
            color.Add("#0D8ECF");
            color.Add("#0D52D1");
            color.Add("#2A0CD0");

            foreach (var item in paisesQuery)
            {
                paisesLista.Add(new VisitSource()
                {
                    name = item.Nombre,
                    value = item.Cantidad.ToString(),
                    color = color[i],
                });
                i++;
            }


            var pie = new PieMapViewModel()
            {
                LegendData = getPrincipalesPaisesMes(),
                SeriesData = paisesLista
            };

            ViewBag.LegendData = pie.LegendData;
            ViewBag.SeriesData = pie.SeriesData;

            ViewBag.Mes = Fecha.ToString("MMMM");
            ViewBag.Año = Fecha.Year;
            ViewBag.Desde = Desde;
            ViewBag.Hasta = Hasta;
            return View();
        }

        //Informes Destinos Principales
        [Authorize(Roles = "Facilitador,Administrador")]
        public List<String> getPrincipalesDestinosMes()
        {
            DateTime Fecha = DateTime.Now;
            var scopeNames = (from s in db.Facilitacions where s.Fecha.Month == Fecha.Month where s.Fecha.Year == Fecha.Year select s.Ciudad.Nombre).Distinct().ToList();
            return scopeNames.ToList();
        }


        [Authorize(Roles = "Facilitador,Administrador")]
        public List<String> getPrincipalesDestinosAño()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var scopeNames = (from s in db.Facilitacions where s.Fecha.Year == Fecha.Year select s.Ciudad.Nombre).Distinct().ToList();
            return scopeNames.ToList();
        }

        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult DestinosPrincipales()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //Consultas a la base de datos
            var destinosQueryMes = db.Facilitacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).GroupBy(x => x.Ciudad.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Ciudad.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).Take(10).ToList();
            var destinosQueryAño = db.Facilitacions.Where(x => x.Fecha.Year == Fecha.Year).GroupBy(x => x.Ciudad.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Ciudad.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).Take(10).ToList();


            List<VisitSource> destinosListaMes = new List<VisitSource>();
            List<VisitSource> destinosListaAño = new List<VisitSource>();

            foreach (var item in destinosQueryMes)
            {
                destinosListaMes.Add(new VisitSource()
                {
                    name = item.Nombre,
                    value = item.Cantidad.ToString()
                });
            }

            foreach (var item in destinosQueryAño)
            {
                destinosListaAño.Add(new VisitSource()
                {
                    name = item.Nombre,
                    value = item.Cantidad.ToString()
                });
            }


            var pieMes = new PieMapViewModel()
            {
                LegendData = getPrincipalesDestinosMes(),
                SeriesData = destinosListaMes
            };

            var pieAño = new PieMapViewModel()
            {
                LegendData = getPrincipalesDestinosAño(),
                SeriesData = destinosListaAño
            };

            ViewBag.LegendDataMes = pieMes.LegendData;
            ViewBag.SeriesDataMes = pieMes.SeriesData;

            ViewBag.LegendDataAño = pieAño.LegendData;
            ViewBag.SeriesDataAño = pieAño.SeriesData;
            ViewBag.Mes = Fecha.ToString("MMMM");
            ViewBag.Año = Fecha.Year;
            return View();
        }

        //[Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult DestinosPrincipales10()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Fecha = DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //Consultas a la base de datos
            var destinosQuery = db.Facilitacions.Where(x => x.Fecha.Month == Fecha.Month).Where(x => x.Fecha.Year == Fecha.Year).GroupBy(x => x.Ciudad.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Ciudad.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).Take(10).ToList();
           
            var i = 0;

            List<VisitSource> destinosListaMes = new List<VisitSource>();
            List<String> color = new List<String>();
            color.Add("#FF0F00");
            color.Add("#FF6600");
            color.Add("#FF9E01");
            color.Add("#FCD202");
            color.Add("#F8FF01");

            color.Add("#B0DE09");
            color.Add("#04D215");
            color.Add("#0D8ECF");
            color.Add("#0D52D1");
            color.Add("#2A0CD0");

            foreach (var item in destinosQuery)
            {
                destinosListaMes.Add(new VisitSource()
                {
                    name = item.Nombre,
                    value = item.Cantidad.ToString(),
                    color = color[i],
                });
                i++;
            }


            var pie = new PieMapViewModel()
            {
                LegendData = getPrincipalesDestinosMes(),
                SeriesData = destinosListaMes
            };

            ViewBag.LegendData = pie.LegendData;
            ViewBag.SeriesData = pie.SeriesData;

            ViewBag.Mes = Fecha.ToString("MMMM");
            ViewBag.Año = Fecha.Year;
            return View();
        }



        [Authorize(Roles = "Facilitador,Administrador")]
        public ActionResult InformePaises()
        {
            return View();
        }

        [Authorize(Roles = "Facilitador,Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrincipalesPaises(DateTime Desde, DateTime Hasta)
        {
            var paisesQuery = db.Facilitacions.Where(u => u.Fecha >= Desde).Where(u => u.Fecha <= Hasta).GroupBy(x => x.Pais.Nombre).Select(g => new { Nombre = g.FirstOrDefault().Pais.Nombre, Cantidad = g.Sum(s => s.Cantidad) }).OrderByDescending(x => x.Cantidad).Take(10);
            List<VisitSource> paisesList = new List<VisitSource>();
            foreach (var item in paisesQuery)
            {
                paisesList.Add(new VisitSource()
                {
                    name = item.Nombre,
                    value = item.Cantidad.ToString()
                });
            }

            var pie = new PieMapViewModel()
            {
                LegendData = getPrincipalesPaisesAño(),
                SeriesData = paisesList
            };

            ViewBag.LegendData = pie.LegendData;
            ViewBag.SeriesData = pie.SeriesData;
            ViewBag.Desde = Desde;
            ViewBag.Hasta = Hasta;
            return View();
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
