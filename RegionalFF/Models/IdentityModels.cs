using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required(ErrorMessage = "El Nº Funcionario es requerido")]
        [Display(Name = "Nº Funcionario")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "La Oficina es requerida")]
        [Display(Name = "Oficina")]
        public int OficinaId { get; set; }

        [Required(ErrorMessage = "El Documento es requerido")]
        [Display(Name = "Documento")]
        public int Documento { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        [ForeignKey("OficinaId")]
        public virtual Oficina Oficina { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<RegionalFF.Models.Menu> Menus { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.MenuAdmin> MenuAdmins { get; set; }

        public System.Data.Entity.DbSet<RegionalFF.Models.Oficina> Oficinas { get; set; }
    }
}