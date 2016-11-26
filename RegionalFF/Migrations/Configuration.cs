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
            AutomaticMigrationsEnabled = false;
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
                  Nombre = "Ajustes",
                  Descripcion = "Gestionar recursos",
                  Perfil = "Facilitador,Fiscalizador",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.Menu
              {
                  //2
                  PadreId = 1,
                  Nombre = "Paises",
                  Descripcion = "Gestionar Paises",
                  Perfil = "Facilitador,Fiscalizador",
                  Accion = "Index",
                  Controlador = "Paises",
                  Activo = true
              },
              new Models.Menu
              {
                  //3
                  PadreId = 1,
                  Nombre = "Ciudades",
                  Descripcion = "Gestionar Ciudades",
                  Perfil = "Facilitador,Fiscalizador",
                  Accion = "Index",
                  Controlador = "Ciudades",
                  Activo = true
              },
              new Models.Menu
              {
                  //4
                  PadreId = 1,
                  Nombre = "Marcas",
                  Descripcion = "Gestionar Marcas",
                  Perfil = "Facilitador,Fiscalizador",
                  Accion = "Index",
                  Controlador = "Marcas",
                  Activo = true
              },
              new Models.Menu
              {
                  //5
                  PadreId = 1,
                  Nombre = "Conductores",
                  Descripcion = "Gestionar Conductores",
                  Perfil = "Fiscalizador",
                  Accion = "Index",
                  Controlador = "Conductores",
                  Activo = true
              },
              new Models.Menu
              {
                  //6
                  PadreId = 1,
                  Nombre = "Transportes",
                  Descripcion = "Gestionar Transportes",
                  Perfil = "Fiscalizador",
                  Accion = "Index",
                  Controlador = "Transportes",
                  Activo = true
              },
              new Models.Menu
              {
                  //7
                  PadreId = 0,
                  Nombre = "Facilitaciones",
                  Descripcion = "Módulo Facilitacion",
                  Perfil = "Facilitador",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.Menu
              {
                  //8
                  PadreId = 7,
                  Nombre = "Hoy",
                  Descripcion = "Módulo Facilitacion | Hoy",
                  Perfil = "Facilitador",
                  Accion = "Index",
                  Controlador = "Facilitaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //9
                  PadreId = 7,
                  Nombre = "Este Mes",
                  Descripcion = "Módulo Facilitacion | Este Mes",
                  Perfil = "Facilitador",
                  Accion = "EsteMes",
                  Controlador = "Facilitaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //10
                  PadreId = 7,
                  Nombre = "Este Año",
                  Descripcion = "Módulo Facilitacion | Este Año",
                  Perfil = "Facilitador",
                  Accion = "EsteAño",
                  Controlador = "Facilitaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //11
                  PadreId = 7,
                  Nombre = "Ver Todo",
                  Descripcion = "Módulo Facilitacion | Todo",
                  Perfil = "Facilitador",
                  Accion = "VerTodo",
                  Controlador = "Facilitaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //12
                  PadreId = 0,
                  Nombre = "Fiscalizaciones",
                  Descripcion = "Módulo Fiscalizaciones",
                  Perfil = "Fiscalizador",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.Menu
              {
                  //13
                  PadreId = 12,
                  Nombre = "Hoy",
                  Descripcion = "Módulo Fiscalizaciones | Hoy",
                  Perfil = "Fiscalizador",
                  Accion = "Index",
                  Controlador = "Fiscalizaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //14
                  PadreId = 12,
                  Nombre = "Este Mes",
                  Descripcion = "Módulo Facilitacion | Este Mes",
                  Perfil = "Fiscalizador",
                  Accion = "EsteMes",
                  Controlador = "Facilitaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //15
                  PadreId = 12,
                  Nombre = "Este Año",
                  Descripcion = "Módulo Fiscalizaciones | Este Año",
                  Perfil = "Fiscalizador",
                  Accion = "EsteAño",
                  Controlador = "Fiscalizaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //16
                  PadreId = 12,
                  Nombre = "Ver Todo",
                  Descripcion = "Módulo Fiscalizaciones | Todo",
                  Perfil = "Fiscalizador",
                  Accion = "VerTodo",
                  Controlador = "Fiscalizaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //17
                  PadreId = 0,
                  Nombre = "Informes",
                  Descripcion = "Módulo Informes de Facilitaciones",
                  Perfil = "Facilitador",
                  Accion = "",
                  Controlador = "",
                  Activo = true
              },
              new Models.Menu
              {
                  //18
                  PadreId = 17,
                  Nombre = "10 Paises de origen",
                  Descripcion = "Informes de Principales Paises de origen de Facilitaciones",
                  Perfil = "Facilitador",
                  Accion = "PaisesPrincipales10",
                  Controlador = "Facilitaciones",
                  Activo = true
              },
              new Models.Menu
              {
                  //19
                  PadreId = 17,
                  Nombre = "10 Principales destinos",
                  Descripcion = "Informes de Principales destinos de Facilitaciones",
                  Perfil = "Facilitador",
                  Accion = "DestinosPrincipales10",
                  Controlador = "Facilitaciones",
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
                        Sigla = "TI",
                        Departamento = "Itapúa",
                        Ciudad = "Encarnación",
                        Direccion = "Artigas",
                        Telefono = " "
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
                        //2
                        Id = 2,
                        Nombre = "Oficina Regional de Encarnación",
                        Sigla = "ORE",
                        Departamento = "Itapúa",
                        Ciudad = "Encarnación",
                        Direccion = "Costanera Padre Bolik",
                        Telefono = "071202889"
                    }
                );
            }

            //Crear si no existe Oficina Regional Ciudad Del Este
            if (!context.Oficinas.Any(r => r.Nombre == "Oficina Regional Ciudad Del Este"))
            {
                context.Oficinas.AddOrUpdate(
                    p => p.Nombre,
                    new Models.Oficina
                    {
                        //3
                        Id = 3,
                        Nombre = "Oficina Regional Ciudad Del Este",
                        Sigla = "ORCE",
                        Departamento = "Alto Paraná",
                        Ciudad = "CDE",
                        Direccion = "Tte.",
                        Telefono = "0745989898"
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
            if (!context.Users.Any(u => u.Email == "rickymaciel@hotmail.es"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "rickymaciel@hotmail.es", UserName = "rickymaciel@hotmail.es", OficinaId = 1, Numero = 100, Documento = 2233922, PhoneNumber = "+595973506058", Nombre = "Super", Apellido = "Admin", Imagen = "default" };

                manager.Create(user, "1Regional/");
                //Delegador Administrador a usuario
                manager.AddToRole(user.Id, "Administrador");
            }


            //Crear Usuario
            if (!context.Users.Any(u => u.Email == "rmacielb3@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "rmacielb3@gmail.com", UserName = "rmacielb3@gmail.com", OficinaId = 2, Numero = 246, Documento = 0, Nombre = "Ricardo", Apellido = "Maciel", Imagen = "default" };

                manager.Create(user, "1Regional/");
                //Delegador Facilitador a usuario
                manager.AddToRole(user.Id, "Facilitador");
            }

            //Crear Usuario
            if (!context.Users.Any(u => u.Email == "rolandorodas007@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "rolandorodas007@gmail.com", UserName = "rolandorodas007@gmail.com", OficinaId = 2, Numero = 245, Nombre = "Rolando", Apellido = "Rodas", Imagen = "default" };

                manager.Create(user, "1Regional/");
                //Delegador Fiscalizador a usuario
                manager.AddToRole(user.Id, "Fiscalizador");
            }

            //Crear Usuario
            if (!context.Users.Any(u => u.Email == "monicasegovia@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "monicasegovia@gmail.com", UserName = "monicasegovia@gmail.com", OficinaId = 3, Numero = 145, Nombre = "Monica", Apellido = "Segovia", Imagen = "default" };

                manager.Create(user, "1Regional/");
                //Delegador Fiscalizador a usuario
                manager.AddToRole(user.Id, "Fiscalizador");
            }
        }
    }
}
