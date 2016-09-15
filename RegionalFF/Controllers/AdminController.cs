#region Includes
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

#endregion Includes

namespace RegionalFF.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        // Controllers


        // GET: /Admin/
        #region public ActionResult Index()
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            List<UsuarioAmpliado> col_Usuario = new List<UsuarioAmpliado>();
            var result = UserManager.Users.ToList().OrderBy(x => x.UserName);
            foreach (var item in result)
            {
                UsuarioAmpliado objUsuario= new UsuarioAmpliado();

                objUsuario.UserName = item.UserName;
                objUsuario.Nombre = item.Nombre;
                objUsuario.Apellido = item.Apellido;
                objUsuario.Numero = item.Numero;
                objUsuario.OficinaId = item.OficinaId;
                objUsuario.Oficina = item.Oficina;
                objUsuario.UserName = item.UserName;
                objUsuario.Documento = item.Documento;
                objUsuario.Email = item.Email;
                objUsuario.LockoutEndDateUtc = item.LockoutEndDateUtc;
                objUsuario.PhoneNumber = item.PhoneNumber;

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
        #region public ActionResult Create(UsuarioAmpliado paramUsuarioAmpliado)
        public ActionResult Create(UsuarioAmpliado paramUsuarioAmpliado)
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
                var PhoneNumber = paramUsuarioAmpliado.PhoneNumber.Trim();
                var Password = paramUsuarioAmpliado.Password.Trim();
                var OficinaId = paramUsuarioAmpliado.OficinaId;

                if (Email == "")
                {
                    throw new Exception("El Email es requerido");
                }

                if (Password == "")
                {
                    throw new Exception("El Password es requerido");
                }

                // Setear UserName con Email en minuscula
                UserName = Email.ToLower();

                // Crear Usuario

                var objNewAdminUsuario = new ApplicationUser { UserName = UserName, Email = Email, PhoneNumber = PhoneNumber, Nombre = Nombre, Apellido = Apellido, Numero = Numero, Documento = Documento, OficinaId = OficinaId };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUsuario, Password);

                if (AdminUserCreateResult.Succeeded == true)
                {
                    string strNewRole = Convert.ToString(Request.Form["Roles"]);

                    if (strNewRole != "0")
                    {
                        // Setear Rol en Usuario
                        UserManager.AddToRole(objNewAdminUsuario.Id, strNewRole);
                    }

                    return Redirect("~/Admin");
                }
                else
                {
                    ViewBag.Roles = GetTodoRolesComoSelectList();

                    ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre", paramUsuarioAmpliado.OficinaId);
                    ModelState.AddModelError(string.Empty,
                        "Error : No se ha podido crear el usuario . Compruebe los requisitos de contraseña.");
                    return View(paramUsuarioAmpliado);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Roles = GetTodoRolesComoSelectList();
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("Create");
            }
        }
        #endregion

        // GET: /Admin/Edit/TestUser 
        [Authorize(Roles = "Administrador")]
        #region public ActionResult EditarUsuario(string UserName)
        public ActionResult EditarUsuario(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAmpliado objExpandedUserDTO = GetUser(UserName);
            ViewBag.OficinaId = new SelectList(db.Oficinas, "Id", "Nombre");
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // PUT: /Admin/EditarUsuario
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditarUsuario(UsuarioAmpliado paramExpandedUserDTO)
        public ActionResult EditarUsuario(UsuarioAmpliado paramExpandedUserDTO)
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

                return Redirect("~/Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditarUsuario", GetUser(paramExpandedUserDTO.UserName));
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUser
        [Authorize(Roles = "Administrador")]
        #region public ActionResult DeleteUser(string UserName)
        public ActionResult DeleteUser(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (UserName.ToLower() == this.User.Identity.Name.ToLower())
                {
                    ModelState.AddModelError(
                        string.Empty, "Error : No se puede suprimir la función de administrador para el usuario actual");

                    return View("EditarUsuario");
                }

                UsuarioAmpliado objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                }

                return Redirect("~/Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditarUsuario", GetUser(UserName));
            }
        }
        #endregion

        // GET: /Admin/EditarRoles/TestUser 
        [Authorize(Roles = "Administrador")]
        #region ActionResult EditarRoles(string UserName)
        public ActionResult EditarRoles(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserName = UserName.ToLower();

            // Check that we have an actual user
            UsuarioAmpliado objExpandedUserDTO = GetUser(UserName);

            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }

            UsuarioyRoles objUserAndRolesDTO =
                GetUserAndRoles(UserName);

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

                string UserName = paramUserAndRolesDTO.UserName;
                string strNewRole = Convert.ToString(Request.Form["NuevoRol"]);

                if (strNewRole != "No se encontraron roles")
                {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByName(UserName);

                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }

                ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(UserName));

                UsuarioyRoles objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

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
        #region public ActionResult DeleteRole(string UserName, string RoleName)
        public ActionResult DeleteRole(string UserName, string RoleName)
        {
            try
            {
                if ((UserName == null) || (RoleName == null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                UserName = UserName.ToLower();

                // Check that we have an actual user
                UsuarioAmpliado objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                if (UserName.ToLower() ==
                    this.User.Identity.Name.ToLower() && RoleName == "Administrador")
                {
                    ModelState.AddModelError(string.Empty,
                        "Error : No se puede suprimir la función de administrador para el usuario actual");
                }

                // Go get the User
                ApplicationUser user = UserManager.FindByName(UserName);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, RoleName);
                UserManager.Update(user);

                ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(UserName));

                return RedirectToAction("EditarRoles", new { UserName = UserName });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(UserName));

                UsuarioyRoles objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

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

                return Redirect("~/Admin/Roles");
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


        #region private UsuarioAmpliado GetUser(string paramUserName)
        private UsuarioAmpliado GetUser(string paramUserName)
        {
            UsuarioAmpliado objExpandedUserDTO = new UsuarioAmpliado();

            var result = UserManager.FindByName(paramUserName);

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

            return objExpandedUserDTO;
        }
        #endregion

        #region private ExpandedUserDTO UpdateDTOUser(UsuarioAmpliado objExpandedUserDTO)
        private UsuarioAmpliado UpdateDTOUser(UsuarioAmpliado paramExpandedUserDTO)
        {
            ApplicationUser result =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

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

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            UserManager.Update(result);

            // Was a password sent across?
            if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password))
            {
                // Remove current password
                var removePassword = UserManager.RemovePassword(result.Id);
                if (removePassword.Succeeded)
                {
                    // Add new password
                    var AddPassword =
                        UserManager.AddPassword(
                            result.Id,
                            paramExpandedUserDTO.Password
                            );

                    if (AddPassword.Errors.Count() > 0)
                    {
                        throw new Exception(AddPassword.Errors.FirstOrDefault());
                    }
                }
            }

            return paramExpandedUserDTO;
        }
        #endregion

        #region private void DeleteUser(UsuarioAmpliado paramExpandedUserDTO)
        private void DeleteUser(UsuarioAmpliado paramExpandedUserDTO)
        {
            ApplicationUser user =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("No se pudo encontrar el usuario");
            }

            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
            UserManager.Update(user);
            UserManager.Delete(user);
        }
        #endregion

        #region private UserAndRolesDTO GetUserAndRoles(string UserName)
        private UsuarioyRoles GetUserAndRoles(string UserName)
        {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(UserName);

            List<UsuarioRol> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UsuarioRol
                 {
                     RoleName = objRole,
                     UserName = UserName
                 }).ToList();

            if (colUserRoleDTO.Count() == 0)
            {
                colUserRoleDTO.Add(new UsuarioRol { RoleName = "No se encontraron roles" });
            }

            ViewBag.NuevoRol = new SelectList(RolesUserIsNotIn(UserName));

            // Create UserRolesAndPermissionsDTO
            UsuarioyRoles objUserAndRolesDTO =
                new UsuarioyRoles();
            objUserAndRolesDTO.UserName = UserName;
            objUserAndRolesDTO.colUsuarioRol = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion

        #region private List<string> RolesUserIsNotIn(string UserName)
        private List<string> RolesUserIsNotIn(string UserName)
        {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByName(UserName);

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