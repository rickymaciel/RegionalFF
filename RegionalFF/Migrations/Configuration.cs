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
                  Nombre = "Navegación",
                  Descripcion = "Menú de Navegación del Sistema <Usuario>",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              });

            //Crear MenuAdmins
            context.MenuAdmins.AddOrUpdate(
                p => p.Nombre,
              new Models.MenuAdmin
              {
                  //1
                  PadreId = 0,
                  Nombre = "Menú Administrador",
                  Descripcion = "Módulo Administrador del Menú Admin del Sistema",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //2
                  PadreId = 0,
                  Nombre = "Menú Usuario",
                  Descripcion = "Módulo Administrador del Menú Usuario del Sistema",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //3
                  PadreId = 1,
                  Nombre = "Menú Administrador",
                  Descripcion = "Listado del Menú Administrador del Sistema",
                  Accion = "Index",
                  Controlador = "Accesos",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //4
                  PadreId = 2,
                  Nombre = "Menú Usuario",
                  Descripcion = "Listado del Menú Usuario del Sistema",
                  Accion = "Index",
                  Controlador = "Menus",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //5
                  PadreId = 1,
                  Nombre = "Crear Menú",
                  Descripcion = "Crear Menú Administrador del Sistema",
                  Accion = "Create",
                  Controlador = "Accesos",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //6
                  PadreId = 1,
                  Nombre = "Oficinas",
                  Descripcion = "Gestionar Oficinas",
                  Accion = "Index",
                  Controlador = "Oficinas",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //7
                  PadreId = 1,
                  Nombre = "Roles",
                  Descripcion = "Gestionar Roles",
                  Accion = "Roles",
                  Controlador = "Admin",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //8
                  PadreId = 1,
                  Nombre = "Usuarios",
                  Descripcion = "Gestionar Usuarios del Sistema",
                  Accion = "Index",
                  Controlador = "Admin",
                  Activo = true
              },
              new Models.MenuAdmin
              {
                  //9
                  PadreId = 2,
                  Nombre = "Crear Menú",
                  Descripcion = "Crear Menú Usuario del Sistema",
                  Accion = "Create",
                  Controlador = "Menus",
                  Activo = true
              });

            //Crear si no existe Administración del Sistema
            if (!context.Oficinas.Any(r => r.Nombre == "Administración del Sistema"))
            {
                context.Oficinas.AddOrUpdate(
                    p => p.Nombre,
                    new Models.Oficina
                    {
                        //1
                        Id = 1,
                        Nombre = "Administración del Sistema",
                        Sigla = "AS",
                        Departamento = "'",
                        Ciudad = "-",
                        Direccion = "-",
                        Telefono = "-"
                    }
                );
            }

            //Crear si no existe Oficina Regional de Encarnación
            if (!context.Oficinas.Any(r => r.Nombre == "Oficina Regional de Encarnación"))
            {
                context.Oficinas.AddOrUpdate(
                    p => p.Nombre,
                    new Models.Oficina
                    {
                        //1
                        Id = 1,
                        Nombre = "Oficina Regional de Encarnación",
                        Sigla = "ORE",
                        Departamento = "Itapúa",
                        Ciudad = "Encarnación",
                        Direccion = "Costanera Padre Bolik",
                        Telefono = "071202889"
                    }
                );
            }


            //Crear si no existe Rol Administrador
            if (!context.Roles.Any(r => r.Name == "Administrador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrador" };

                manager.Create(role);
            }


            //Crear si no existe Rol Facilitador
            if (!context.Roles.Any(r => r.Name == "Facilitador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Facilitador" };

                manager.Create(role);
            }

            //Crear si no existe Rol Fiscalizador
            if (!context.Roles.Any(r => r.Name == "Fiscalizador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Fiscalizador" };

                manager.Create(role);
            }

            //Crear Usuario
            if (!context.Users.Any(u => u.Email == "rmacielb3@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "rmacielb3@gmail.com", UserName = "rmacielb3@gmail.com", OficinaId = 1, Numero = 246, Nombre = "Ricardo", Apellido = "Maciel" };

                manager.Create(user, "1Regional/");
                //Delegador Administrador a usuario
                manager.AddToRole(user.Id, "Administrador");
            }
        }
    }
}
