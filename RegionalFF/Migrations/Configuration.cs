namespace RegionalFF.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RegionalFF.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RegionalFF.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RegionalFF.Models.ApplicationDbContext context)
        {
            //Crear Menu
            context.Menus.AddOrUpdate(
                p => p.Nombre,
              new Models.Menu
              {
                  //1
                  PadreId = 0,
                  Nombre = "Admin",
                  Descripcion = "Módulos del Administrador del Sistema",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.Menu
              {
                  //1
                  PadreId = 1,
                  Nombre = "Menú",
                  Descripcion = "Menu del Sistema",
                  Accion = "Index",
                  Controlador = "Menus",
                  Activo = true
              },
              new Models.Menu
              {
                  //1
                  PadreId = 1,
                  Nombre = "Crear Usuario",
                  Descripcion = "Crear usuario del Usuario del Sistema",
                  Accion = "Create",
                  Controlador = "Admin",
                  Activo = true
              },
              new Models.Menu
              {
                  //1
                  PadreId = 1,
                  Nombre = "Nuevo Rol",
                  Descripcion = "Crear Roles",
                  Accion = "AddRole",
                  Controlador = "Admin",
                  Activo = true
              });


            //Crear si no existe Rol Administrador
            if (!context.Roles.Any(r => r.Name == "Administrador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrador" };

                manager.Create(role);
            }

            //Crear Usuario
            if (!context.Users.Any(u => u.Email == "rmacielb3@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "rmacielb3@gmail.com", UserName = "rmacielb3@gmail.com" };

                manager.Create(user, "1Regional/");
                //Delegador Administrador a usuario
                manager.AddToRole(user.Id, "Administrador");
            }
        }
    }
}
