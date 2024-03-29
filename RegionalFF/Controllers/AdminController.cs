﻿#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using RegionalFF.Models;
using PagedList;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

#endregion Includes

namespace RegionalFF.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;



        // Controllers
        // GET: /Admin/EditarRol/5
        [Authorize(Roles = "Administrador")]
        public ActionResult EditarRol(string roleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarRol(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                TempData["notice"] = "El Rol fue modificado con éxito";
                return RedirectToAction("Roles");
            }
            catch
            {
                return View();
            }
        }

        // GET: /Admin/
        #region public ActionResult Index()
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            List<UsuarioAmpliado> col_Usuario = new List<UsuarioAmpliado>();
            var result = UserManager.Users.ToList().OrderBy(x => x.Email);
            foreach (var item in result)
            {
                UsuarioAmpliado objUsuario = new UsuarioAmpliado();

                objUsuario.UserName = item.UserName;
                objUsuario.Nombre = item.Nombre;
                objUsuario.Apellido = item.Apellido;
                objUsuario.Numero = item.Numero;
                objUsuario.OficinaId = item.OficinaId;
                objUsuario.Oficina = item.Oficina;
                objUsuario.Documento = item.Documento;
                objUsuario.Email = item.Email;
                objUsuario.LockoutEndDateUtc = item.LockoutEndDateUtc;
                objUsuario.PhoneNumber = item.PhoneNumber;
                objUsuario.Imagen = item.Imagen;
                objUsuario.Direccion = item.Direccion;

                col_Usuario.Add(objUsuario);
            }
            return View(col_Usuario);
        }

        #endregion


        // GET: /Admin/
        #region public ActionResult Usuarios()
        [Authorize(Roles = "Administrador")]
        public ActionResult Usuarios()
        {
            List<UsuarioAmpliado> col_Usuario = new List<UsuarioAmpliado>();
            var result = UserManager.Users.ToList().OrderBy(x => x.Email);
            foreach (var item in result)
            {
                UsuarioAmpliado objUsuario = new UsuarioAmpliado();

                objUsuario.UserName = item.UserName;
                objUsuario.Nombre = item.Nombre;
                objUsuario.Apellido = item.Apellido;
                objUsuario.Numero = item.Numero;
                objUsuario.OficinaId = item.OficinaId;
                objUsuario.Oficina = item.Oficina;
                objUsuario.Documento = item.Documento;
                objUsuario.Email = item.Email;
                objUsuario.LockoutEndDateUtc = item.LockoutEndDateUtc;
                objUsuario.PhoneNumber = item.PhoneNumber;
                objUsuario.Imagen = item.Imagen;
                objUsuario.Direccion = item.Direccion;

                col_Usuario.Add(objUsuario);
            }
            return View(col_Usuario);
        }

        #endregion


        // Users *****************************

        // GET: /Admin/Edit/Create 
        [Authorize(Roles = "Administrador")]
        #region public ActionResult Create()
        public ActionResult Create()
        {
            UsuarioAmpliado objUsuarioAmpliado = new UsuarioAmpliado();

            ViewBag.Roles = GetTodoRolesComoSelectList();

            ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre");

            return View(objUsuarioAmpliado);
        }
        #endregion

        // PUT: /Admin/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Create(UsuarioAmpliado paramUsuarioAmpliado, HttpPostedFileBase Imagen)
        public ActionResult Create(UsuarioAmpliado paramUsuarioAmpliado, HttpPostedFileBase Imagen)
        {
            try
            {
                if (paramUsuarioAmpliado == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var Email = paramUsuarioAmpliado.Email.Trim();
                var UserName = paramUsuarioAmpliado.Email.Trim();
                var Nombre = paramUsuarioAmpliado.Nombre.Trim();
                var Apellido = paramUsuarioAmpliado.Apellido.Trim();
                var Documento = paramUsuarioAmpliado.Documento;
                var Numero = paramUsuarioAmpliado.Numero;
                var IImagen = paramUsuarioAmpliado.Imagen;
                var Direccion = paramUsuarioAmpliado.Direccion;
                var PhoneNumber = paramUsuarioAmpliado.PhoneNumber.Trim();
                var Password = paramUsuarioAmpliado.Password.Trim();
                var OficinaId = paramUsuarioAmpliado.OficinaId;
                var Rol = Convert.ToString(Request.Form["Roles"]);

                // Setear UserName con Email en minuscula
                Email = Email.ToLower();
                if (IImagen == "")
                {
                    IImagen = "default";
                }
                // Crear Usuario
                var objNewAdminUsuario = new ApplicationUser { UserName = UserName, Email = Email, PhoneNumber = PhoneNumber, Nombre = Nombre, Apellido = Apellido, Numero = Numero, Documento = Documento, OficinaId = OficinaId, Imagen = IImagen, Direccion = Direccion };

                var AdminUserCreateResult = UserManager.Create(objNewAdminUsuario, Password);
                if (AdminUserCreateResult.Succeeded == true)
                {
                    string strNewRole = Convert.ToString(Request.Form["Roles"]);
                    if (strNewRole != "" || strNewRole != null)
                    {
                        // Setear Rol en Usuario
                        UserManager.AddToRole(objNewAdminUsuario.Id, strNewRole);
                    }
                    ApplicationUser usua = UserManager.FindByEmail(Email);
                    if (Imagen != null)
                    {
                        CreateThumbnail(Imagen, objNewAdminUsuario);
                        var nombrearchivo = CreateImagen(Imagen, objNewAdminUsuario);
                        usua.Imagen = nombrearchivo;
                        UserManager.Update(usua);
                    }
                    TempData["notice"] = "El usuario " + paramUsuarioAmpliado.Nombre + " " + paramUsuarioAmpliado.Apellido + " ha sido creado!";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Roles = GetTodoRolesComoSelectList();
                    ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", paramUsuarioAmpliado.OficinaId);
                    TempData["error"] = "Error : No se ha podido crear el usuario . Compruebe que todos los campos requeridos esten ingresados.";
                    return View(paramUsuarioAmpliado);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Roles = GetTodoRolesComoSelectList();
                TempData["error"] = "Error : No se ha podido crear el usuario . Compruebe que todos los campos requeridos esten ingresados.";
                TempData["e"] = ex;
                ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre");
                return View("Create");
            }
        }
        #endregion

        // GET: /Admin/Edit/TestUser 
        [Authorize(Roles = "Administrador")]
        #region public ActionResult EditarUsuario(string Email)
        public ActionResult EditarUsuario(string Email)
        {
            if (Email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Email == User.Identity.GetUserName())
            {
                return RedirectToAction("ModificarUsuario", "Admin");
            }
            UsuarioAmpliado objExpandedUserDTO = GetUser(Email);
            ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", objExpandedUserDTO.OficinaId);
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion


        #region public ActionResult CambiarImagen()
        public ActionResult CambiarImagen()
        {
            var Email = User.Identity.GetUserName();
            if (Email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAmpliado objExpandedUserDTO = GetUsuario(Email);
            ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre");
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }


        #region public void CrearImagen(HttpPostedFileBase Imagen)
        public string CrearImagen(HttpPostedFileBase Imagen)
        {
            var fileName = Path.GetFileName(User.Identity.GetUserId());
            var path = Server.MapPath("~/Content/img/Uploads/Usuarios/Perfil/" + User.Identity.GetUserId());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string outputPath = Path.Combine(path, User.Identity.GetUserId() + ".png");
            Imagen.SaveAs(outputPath);
            return fileName;
        }
        #endregion


        #region public void CreateImagen(HttpPostedFileBase Imagen)
        public string CreateImagen(HttpPostedFileBase Imagen, ApplicationUser user)
        {
            var fileName = Path.GetFileName(user.Id);
            var path = Server.MapPath("~/Content/img/Uploads/Usuarios/Perfil/" + user.Id);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string outputPath = Path.Combine(path, user.Id + ".png");
            Imagen.SaveAs(outputPath);
            return fileName;
        }
        #endregion
        #region public void CreateThumbnail(HttpPostedFileBase Imagen, ApplicationUser user)
        public void CreateThumbnail(HttpPostedFileBase Imagen, ApplicationUser user)
        {
            using (var image = Image.FromStream(Imagen.InputStream, true, true)) /* Creates Image from specified data stream */
            {
                using (var thumb = image.GetThumbnailImage(100, /* width*/ 100, /* height*/ () => false, IntPtr.Zero))
                {
                    var jpgInfo = ImageCodecInfo.GetImageEncoders().Where(codecInfo => codecInfo.MimeType == "image/png").First(); /* Returns array of image encoder objects built into GDI+ */
                    using (var encParams = new EncoderParameters(1))
                    {
                        var appDataThumbnailPath = Server.MapPath("~/Content/img/Uploads/Usuarios/Thumbnail/" + user.Id);
                        if (!Directory.Exists(appDataThumbnailPath))
                        {
                            Directory.CreateDirectory(appDataThumbnailPath);
                        }
                        string outputPath = Path.Combine(appDataThumbnailPath, user.Id + ".png");
                        long quality = 8;
                        encParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                        thumb.Save(outputPath, jpgInfo, encParams);
                    }
                }
            }
        }
        #endregion


        #region public void CrearThumbnail(HttpPostedFileBase Imagen)
        public void CrearThumbnail(HttpPostedFileBase Imagen)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            using (var image = Image.FromStream(Imagen.InputStream, true, true)) /* Creates Image from specified data stream */
            {
                using (var thumb = image.GetThumbnailImage( 100, /* width*/ 100, /* height*/ () => false, IntPtr.Zero))
                {
                    var jpgInfo = ImageCodecInfo.GetImageEncoders().Where(codecInfo => codecInfo.MimeType == "image/png").First(); /* Returns array of image encoder objects built into GDI+ */
                    using (var encParams = new EncoderParameters(1))
                    {
                        var appDataThumbnailPath = Server.MapPath("~/Content/img/Uploads/Usuarios/Thumbnail/" + User.Identity.GetUserId());
                        if (!Directory.Exists(appDataThumbnailPath))
                        {
                            Directory.CreateDirectory(appDataThumbnailPath);
                        }
                        string outputPath = Path.Combine(appDataThumbnailPath, User.Identity.GetUserId()+".png");
                        long quality = 8;
                        encParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                        thumb.Save(outputPath, jpgInfo, encParams);
                    }
                }
            }
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCambiarImagen(HttpPostedFileBase Imagen)
        {
            try
            {
                //obtener usario logueado

                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());

                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UsuarioAmpliado objExpandedUserDTO = GetUsuario(user.Email);
                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                if (Imagen != null)
                {
                    CrearThumbnail(Imagen);
                    var nombrearchivo =  CrearImagen(Imagen);

                    objExpandedUserDTO.Imagen = nombrearchivo;
                    UsuarioAmpliado objExpandedUserDTO2 = ModifUsuario(objExpandedUserDTO);
                    if (objExpandedUserDTO2 == null)
                    {
                        return HttpNotFound();
                    }
                    ActualizarCookies(objExpandedUserDTO.Email);
                    TempData["title"] = "Operación exitosa";
                    TempData["type"] = "success";
                    TempData["notice"] = "La Imagen de Perfil fue modificado satisfactoriamente!";
                    ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", objExpandedUserDTO.OficinaId);
                    return RedirectToAction("Index", "Manage");
                }

                TempData["title"] = "Operación cancelada";
                TempData["type"] = "warning";
                TempData["notice"] = "La Imagen es requerida!";
                return RedirectToAction("Index", "Manage");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex); ;
                TempData["notice"] = "Error: " + ex;

                TempData["title"] = "Operación fallida";
                TempData["type"] = "error";
                TempData["notice"] = "Error: " + ex;

                return View("Index");
            }
        }
        #endregion
        // PUT: /Admin/CambiarImagen
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult CambiarImagen(UsuarioAmpliado paramExpandedUserDTO)
        public ActionResult CambiarImagen(UsuarioAmpliado paramExpandedUserDTO, HttpPostedFileBase Imagen)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser user = UserManager.FindByEmail(paramExpandedUserDTO.Email);
                CreateThumbnail(Imagen, user);

                UsuarioAmpliado objExpandedUserDTO = ModifUsuario(paramExpandedUserDTO);
                ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", paramExpandedUserDTO.OficinaId);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                ActualizarCookies(paramExpandedUserDTO.Email);
                TempData["title"] = "Operación exitosa";
                TempData["type"] = "success";
                TempData["notice"] = "La Imagen de Perfil fue modificado satisfactoriamente!";
                return RedirectToAction("Index", "Manage");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex); ;

                TempData["title"] = "Operación cancelada";
                TempData["type"] = "success";
                TempData["notice"] = "Ocurrió un error y no se guardaron los cambios";
                return View("ModificarUsuario");
            }
        }
        #endregion



        #region public ActionResult ModificarUsuario()
        public ActionResult ModificarUsuario()
        {
            var Email = User.Identity.GetUserName();
            if (Email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAmpliado objExpandedUserDTO = GetUsuario(Email);
            ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", objExpandedUserDTO.OficinaId);
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // PUT: /Admin/ModificarUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult ModificarUsuario(UsuarioAmpliado paramExpandedUserDTO)
        public ActionResult ModificarUsuario(UsuarioAmpliado paramExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser user = UserManager.FindByEmail(paramExpandedUserDTO.Email);
                paramExpandedUserDTO.Imagen = user.Imagen;
                UsuarioAmpliado objExpandedUserDTO = ModifUsuario(paramExpandedUserDTO);
                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", paramExpandedUserDTO.OficinaId);
                TempData["notice"] = "El Usuario fue modificado satisfactoriamente!";
                return RedirectToAction("Index", "Manage");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex); ;
                TempData["notice"] = "Error: " + ex;
                return View("ModificarUsuario");
            }
        }
        #endregion


        // PUT: /Admin/EditarUsuario
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditarUsuario(UsuarioAmpliado paramExpandedUserDTO)
        public ActionResult EditarUsuario(UsuarioAmpliado paramExpandedUserDTO, HttpPostedFileBase Imagen)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                UsuarioAmpliado objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);
                ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", paramExpandedUserDTO.OficinaId);
                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                TempData["notice"] = "El Usuario fue modificado satisfactoriamente!";

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex); ;
                TempData["notice"] = "Error: " + ex;
                return View("EditarUsuario", GetUser(paramExpandedUserDTO.Email));
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUser
        [Authorize(Roles = "Administrador")]
        #region public ActionResult DeleteUser(string Email)
        public ActionResult DeleteUser(string Email)
        {
            try
            {
                if (Email == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (Email.ToLower() == this.User.Identity.Name.ToLower())
                {
                    ModelState.AddModelError(
                        string.Empty, "Error : No se puede suprimir la función de administrador para el usuario actual");
                    TempData["notice"] = "Error : No se puede suprimir la función de administrador para el usuario actual";

                    return View("EditarUsuario");
                }

                UsuarioAmpliado objExpandedUserDTO = GetUser(Email);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                    TempData["notice"] = "El Usuario fue eliminado satisfactoriamente!";
                }

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                TempData["notice"] = "Error: " + ex;
                return View("EditarUsuario", GetUser(Email));
            }
        }
        #endregion

        // GET: /Admin/EditarRoles/TestUser 
        [Authorize(Roles = "Administrador")]
        #region ActionResult EditarRoles(string Email)
        public ActionResult EditarRoles(string Email)
        {
            if (Email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Email = Email.ToLower();

            // Check that we have an actual user
            UsuarioAmpliado objExpandedUserDTO = GetUser(Email);

            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }

            UsuarioyRoles objUserAndRolesDTO =
                GetUserAndRoles(Email);

            ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(Email));

            return View(objUserAndRolesDTO);
        }
        #endregion

        // PUT: /Admin/EditarRoles/TestUser 
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditarRoles(UserAndRolesDTO paramUserAndRolesDTO)
        public ActionResult EditarRoles(UsuarioyRoles paramUserAndRolesDTO)
        {
            try
            {
                if (paramUserAndRolesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                string Email = paramUserAndRolesDTO.Email;
                string strNewRole = Convert.ToString(Request.Form["NuevoRol"]);

                if (strNewRole != "No se encontraron roles")
                {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByEmail(Email);

                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }

                ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(Email));

                UsuarioyRoles objUserAndRolesDTO = GetUserAndRoles(Email);

                return View(objUserAndRolesDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditarRoles");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteRole?UserName="TestUser&RoleName=Administrator
        [Authorize(Roles = "Administrador")]
        #region public ActionResult DeleteRole(string Email, string RoleName)
        public ActionResult DeleteRole(string Email, string RoleName)
        {
            try
            {
                if ((Email == null) || (RoleName == null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Email = Email.ToLower();

                // Check that we have an actual user
                UsuarioAmpliado objExpandedUserDTO = GetUser(Email);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                if (Email.ToLower() ==
                    this.User.Identity.Name.ToLower() && RoleName == "Administrador")
                {
                    ModelState.AddModelError(string.Empty,
                        "Error : No se puede suprimir la función de administrador para el usuario actual");
                }

                // Go get the User
                ApplicationUser user = UserManager.FindByEmail(Email);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, RoleName);
                UserManager.Update(user);

                ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(Email));

                return RedirectToAction("EditarRoles", new { Email = Email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(Email));

                UsuarioyRoles objUserAndRolesDTO =
                    GetUserAndRoles(Email);

                return View("EditarRoles", objUserAndRolesDTO);
            }
        }
        #endregion

        // Roles *****************************

        // GET: /Admin/Roles
        [Authorize(Roles = "Administrador")]
        #region public ActionResult Roles()
        public ActionResult Roles()
        {
            var roleManager =
                new RoleManager<IdentityRole>
                (
                    new RoleStore<IdentityRole>(new ApplicationDbContext())
                    );

            List<Rol> colRoleDTO = (from objRole in roleManager.Roles
                                    select new Rol
                                    {
                                        Id = objRole.Id,
                                        RoleName = objRole.Name
                                    }).ToList();

            return View(colRoleDTO);
        }
        #endregion

        // GET: /Admin/NuevoRol
        [Authorize(Roles = "Administrador")]
        #region public ActionResult NuevoRol()
        public ActionResult NuevoRol()
        {
            Rol objRoleDTO = new Rol();

            return View(objRoleDTO);
        }
        #endregion

        // PUT: /Admin/NuevoRol
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult NuevoRol(RoleDTO paramRoleDTO)
        public ActionResult NuevoRol(Rol paramRoleDTO)
        {
            try
            {
                if (paramRoleDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var RoleName = paramRoleDTO.RoleName.Trim();

                if (RoleName == "")
                {
                    throw new Exception("Sin Nombre de rol");
                }

                // Create Role
                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext())
                        );

                if (!roleManager.RoleExists(RoleName))
                {
                    roleManager.Create(new IdentityRole(RoleName));
                }

                return RedirectToAction("Roles", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("NuevoRol");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUserRole?RoleName=TestRole
        [Authorize(Roles = "Administrador")]
        #region public ActionResult DeleteUserRole(string RoleName)
        public ActionResult DeleteUserRole(string RoleName)
        {
            try
            {
                if (RoleName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (RoleName.ToLower() == "administrator")
                {
                    throw new Exception(String.Format("No se puede eliminar {0} Rol.", RoleName));
                }

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                var UsersInRole = roleManager.FindByName(RoleName).Users.Count();
                if (UsersInRole > 0)
                {
                    throw new Exception(
                        String.Format(
                            "No se puede eliminar {0} clases, porque todavía tiene usuarios",
                            RoleName)
                            );
                }

                var objRoleToDelete = (from objRole in roleManager.Roles
                                       where objRole.Name == RoleName
                                       select objRole).FirstOrDefault();
                if (objRoleToDelete != null)
                {
                    roleManager.Delete(objRoleToDelete);
                }
                else
                {
                    throw new Exception(
                        String.Format(
                            "No se puede eliminar {0} El Rol no existe.",
                            RoleName)
                            );
                }

                List<Rol> colRoleDTO = (from objRole in roleManager.Roles
                                        select new Rol
                                        {
                                            Id = objRole.Id,
                                            RoleName = objRole.Name
                                        }).ToList();

                return View("Roles", colRoleDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                List<Rol> colRoleDTO = (from objRole in roleManager.Roles
                                        select new Rol
                                        {
                                            Id = objRole.Id,
                                            RoleName = objRole.Name
                                        }).ToList();

                return View("Roles", colRoleDTO);
            }
        }
        #endregion


        // Utility

        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion


        #region public ActionResult ActualizarCookies(String parametros)
        public bool ActualizarCookies(String a)
        {
            ApplicationUser user = UserManager.FindByEmail(a);
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userRoles = userManager.GetRoles(user.Id);
            if (userRoles[0] != null || userRoles[0] != "")
            {
                int Index = 0;
                OficinasController oficinacurrent = new OficinasController();
                HttpCookie RegionalFF = HttpContext.Request.Cookies["RegionalFF"] ?? new HttpCookie("RegionalFF");
                RegionalFF.Values["Oficina"] = oficinacurrent.getEmpresaUsers(user.Email);
                RegionalFF.Values["Ciudad"] = oficinacurrent.getEmpresaCiudadUsers(user.Email);
                RegionalFF.Values["Departamento"] = oficinacurrent.getEmpresaDepartamentoUsers(user.Email);
                RegionalFF.Values["Nombre"] = oficinacurrent.getEmpresaNombreUsuario(user.Email);
                RegionalFF.Values["Apellido"] = oficinacurrent.getEmpresaApellidoUsuario(user.Email);
                RegionalFF.Values["Imagen"] = oficinacurrent.getImagenUsuario(user.Email);
                RegionalFF.Values["Sigla"] = oficinacurrent.getEmpresaSiglaUsers(user.Email);
                RegionalFF.Values["Username"] = oficinacurrent.getEmpresaUsernameUsuario(user.Email);
                RegionalFF.Values["Direccion"] = oficinacurrent.getEmpresaDireccionUsers(user.Email);
                RegionalFF.Values["Documento"] = oficinacurrent.getEmpresaDocumentoUsers(user.Email).ToString();
                RegionalFF.Values["Numero"] = oficinacurrent.getEmpresaNumeroUsers(user.Email).ToString();
                RegionalFF.Values["Rol"] = userRoles[0];
                Index = user.Email.IndexOf("@");
                RegionalFF.Values["Usuario"] = user.Email.Substring(0, Index);
                //RegionalFF.Expires = DateTime.Now.AddDays(1d);
                RegionalFF.Expires.Add(TimeSpan.FromDays(1.0));
                Response.Cookies.Add(RegionalFF);
                return true;
            }
            return false;
        }
        #endregion


        #region private List<SelectListItem> GetTodoRolesComoSelectList()
        private List<SelectListItem> GetTodoRolesComoSelectList()
        {
            List<SelectListItem> SelectRoleListItems =
                new List<SelectListItem>();

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

            SelectRoleListItems.Add(
                new SelectListItem
                {
                    Text = "Select",
                    Value = "0"
                });

            foreach (var item in colRoleSelectList)
            {
                SelectRoleListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name.ToString(),
                        Value = item.Name.ToString()
                    });
            }

            return SelectRoleListItems;
        }
        #endregion

        private ApplicationDbContext db = new ApplicationDbContext();

        #region private List<SelectListItem> GetAllOficinassAsSelectList()
        private List<SelectListItem> GetAllOficinassAsSelectList()
        {
            List<SelectListItem> SelectOficinaListItems = new List<SelectListItem>();


            var colOficinaSelectList = db.Oficinas.OrderBy(x => x.Nombre).ToList();

            SelectOficinaListItems.Add(
                new SelectListItem
                {
                    Text = "Seleccionar Oficina",
                    Value = "0"
                });

            foreach (var item in colOficinaSelectList)
            {
                SelectOficinaListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Nombre.ToString(),
                        Value = item.Id.ToString()
                    });
            }

            return SelectOficinaListItems;
        }
        #endregion


        #region private UsuarioAmpliado GetUser(string Email)
        private UsuarioAmpliado GetUser(string Email)
        {
            UsuarioAmpliado objExpandedUserDTO = new UsuarioAmpliado();

            var result = UserManager.FindByEmail(Email);

            // If we could not find the user, throw an exception
            if (result == null) throw new Exception("No se pudo encontrar el usuario");

            objExpandedUserDTO.UserName = result.UserName;
            objExpandedUserDTO.Nombre = result.Nombre;
            objExpandedUserDTO.Apellido = result.Apellido;
            objExpandedUserDTO.Documento = result.Documento;
            objExpandedUserDTO.Numero = result.Numero;
            objExpandedUserDTO.Email = result.Email;
            objExpandedUserDTO.LockoutEndDateUtc = result.LockoutEndDateUtc;
            objExpandedUserDTO.AccessFailedCount = result.AccessFailedCount;
            objExpandedUserDTO.PhoneNumber = result.PhoneNumber;
            objExpandedUserDTO.OficinaId = result.OficinaId;
            objExpandedUserDTO.Imagen = result.Imagen;
            objExpandedUserDTO.Direccion = result.Direccion;

            return objExpandedUserDTO;
        }
        #endregion


        #region private UsuarioAmpliado GetUsuario(string paramEmail)
        private UsuarioAmpliado GetUsuario(string paramEmail)
        {
            UsuarioAmpliado objExpandedUserDTO = new UsuarioAmpliado();

            var result = UserManager.FindByEmail(paramEmail);

            // If we could not find the user, throw an exception
            if (result == null) throw new Exception("No se pudo encontrar el usuario");

            objExpandedUserDTO.UserName = result.UserName;
            objExpandedUserDTO.Nombre = result.Nombre;
            objExpandedUserDTO.Apellido = result.Apellido;
            objExpandedUserDTO.Documento = result.Documento;
            objExpandedUserDTO.Numero = result.Numero;
            objExpandedUserDTO.Email = result.Email;
            objExpandedUserDTO.LockoutEndDateUtc = result.LockoutEndDateUtc;
            objExpandedUserDTO.AccessFailedCount = result.AccessFailedCount;
            objExpandedUserDTO.PhoneNumber = result.PhoneNumber;
            objExpandedUserDTO.OficinaId = result.OficinaId;
            objExpandedUserDTO.Imagen = result.Imagen;
            objExpandedUserDTO.Direccion = result.Direccion;

            return objExpandedUserDTO;
        }
        #endregion

        #region private ExpandedUserDTO UpdateDTOUser(UsuarioAmpliado objExpandedUserDTO)
        private UsuarioAmpliado UpdateDTOUser(UsuarioAmpliado paramExpandedUserDTO)
        {
            ApplicationUser result = UserManager.FindByEmail(paramExpandedUserDTO.Email);

            // If we could not find the user, throw an exception
            if (result == null)
            {
                throw new Exception("No se pudo encontrar el usuario");
            }

            result.Email = paramExpandedUserDTO.Email;
            result.PhoneNumber = paramExpandedUserDTO.PhoneNumber;
            result.Nombre = paramExpandedUserDTO.Nombre;
            result.Apellido = paramExpandedUserDTO.Apellido;
            result.Numero = paramExpandedUserDTO.Numero;
            result.Documento = paramExpandedUserDTO.Documento;
            result.UserName = paramExpandedUserDTO.UserName;
            result.OficinaId = paramExpandedUserDTO.OficinaId;
            result.Oficina = paramExpandedUserDTO.Oficina;
            //result.Imagen = paramExpandedUserDTO.Imagen;
            result.Direccion = paramExpandedUserDTO.Direccion;

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            IdentityResult estado = UserManager.Update(result);

            if (estado.Succeeded)
            {
                IdentityResult cambiar = UserManager.ChangePassword(result.Id, paramExpandedUserDTO.Password, paramExpandedUserDTO.Password);
                if (cambiar.Errors.Count() > 0)
                {
                    throw new Exception(cambiar.Errors.FirstOrDefault());
                }
                return paramExpandedUserDTO;
            }
            throw new Exception("No se realizo ningun cambio!");
        }
        #endregion



        #region private ExpandedUserDTO ModifUsuario(UsuarioAmpliado objExpandedUserDTO)
        private UsuarioAmpliado ModifUsuario(UsuarioAmpliado paramExpandedUserDTO)
        {
            ApplicationUser result =
                UserManager.FindByEmail(paramExpandedUserDTO.Email);

            // If we could not find the user, throw an exception
            if (result == null)
            {
                throw new Exception("No se pudo encontrar el usuario");
            }

            result.Email = paramExpandedUserDTO.Email;
            result.PhoneNumber = paramExpandedUserDTO.PhoneNumber;
            result.Nombre = paramExpandedUserDTO.Nombre;
            result.Apellido = paramExpandedUserDTO.Apellido;
            result.Numero = paramExpandedUserDTO.Numero;
            result.Documento = paramExpandedUserDTO.Documento;
            result.UserName = paramExpandedUserDTO.UserName;
            result.OficinaId = paramExpandedUserDTO.OficinaId;
            result.Oficina = paramExpandedUserDTO.Oficina;
            result.Imagen = paramExpandedUserDTO.Imagen;
            result.Direccion = paramExpandedUserDTO.Direccion;

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            IdentityResult estado = UserManager.Update(result);

            if (estado.Succeeded)
            {
                ActualizarCookies(paramExpandedUserDTO.Email);
                return paramExpandedUserDTO;
            }
            throw new Exception(estado.Errors.FirstOrDefault());
        }
        #endregion




        #region private void DeleteUser(UsuarioAmpliado paramExpandedUserDTO)
        private void DeleteUser(UsuarioAmpliado paramExpandedUserDTO)
        {
            ApplicationUser user = UserManager.FindByName(paramExpandedUserDTO.Email);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("No se pudo encontrar el usuario");
            }

            var appDataThumbnailPath = Server.MapPath("~/Content/img/Uploads/Usuarios/Thumbnail/" + user.Id);
            var appDataPerfilPath = Server.MapPath("~/Content/img/Uploads/Usuarios/Perfil/" + user.Id);
            Directory.Delete(appDataThumbnailPath, true);
            Directory.Delete(appDataPerfilPath, true);

            bool directorio1Exists = Directory.Exists(appDataThumbnailPath);
            bool directorio2Exists = Directory.Exists(appDataPerfilPath);
            if (directorio1Exists == false && directorio2Exists == false)
            {
                UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
                UserManager.Update(user);
                UserManager.Delete(user);
            }
            else
            {
                TempData["notice"] = "No se pudo eliminar el usuario";
            }
        }
        #endregion

        #region private UserAndRolesDTO GetUserAndRoles(string Email)
        private UsuarioyRoles GetUserAndRoles(string Email)
        {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(Email);

            List<UsuarioRol> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UsuarioRol
                 {
                     RoleName = objRole,
                     Email = Email
                 }).ToList();

            if (colUserRoleDTO.Count() == 0)
            {
                colUserRoleDTO.Add(new UsuarioRol { RoleName = "No se encontraron roles" });
            }

            ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(Email));

            // Create UserRolesAndPermissionsDTO
            UsuarioyRoles objUserAndRolesDTO =
                new UsuarioyRoles();
            objUserAndRolesDTO.Email = Email;
            objUserAndRolesDTO.colUsuarioRol = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion

        #region private List<string> RolesUserIsNotIn(string Email)
        private List<string> RolesUserIsNotIn(string Email)
        {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByEmail(Email);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("No se pudo encontrar el usuario");
            }

            var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
            var colRolesUserInNotIn = (from objRole in colAllRoles
                                       where !colRolesForUser.Contains(objRole)
                                       select objRole).ToList();

            if (colRolesUserInNotIn.Count() == 0)
            {
                colRolesUserInNotIn.Add("No se encontraron roles");
            }

            return colRolesUserInNotIn;
        }
        #endregion
    }
}