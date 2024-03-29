﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RegionalFF.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;

        }
        public ApplicationUser()
        {
            //Origenes = new List<Pais>();
            //Destinos = new List<Ciudad>();
            //Facilitaciones = new List<Facilitacion>();
            //Fiscalizaciones = new List<Fiscalizacion>();
        }

        //public virtual ICollection<Pais> Origenes { get; set; }
        //public virtual ICollection<Ciudad> Destinos { get; set; }
        //public virtual ICollection<Facilitacion> Facilitaciones { get; set; }
        //public virtual ICollection<Fiscalizacion> Fiscalizaciones { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nº Funcionario")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Oficina")]
        public int OficinaId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Documento")]
        public int Documento { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Apellido { get; set; }

        public string Direccion { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [ForeignKey("OficinaId")]
        public virtual Oficina Oficina { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("REGIONALFF", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<RegionalFF.Models.Menu> Menus { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.MenuAdmin> MenuAdmins { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Oficina> Oficinas { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Pais> Pais { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Ciudad> Ciudads { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Facilitacion> Facilitacions { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Marca> Marcas { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Conductor> Conductors { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Transporte> Transportes { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Fiscalizacion> Fiscalizacions { get; set; }

        //public System.Data.Entity.DbSet<RegionalFF.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}