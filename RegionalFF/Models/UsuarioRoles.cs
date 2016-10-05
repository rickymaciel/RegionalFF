using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegionalFF.Models
{
    public class UsuarioAmpliado
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        public string Password { get; set; }

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
        [Display(Name = "Oficina")]
        public virtual Oficina Oficina { get; set; }

        [Display(Name = "Lockout End Date Utc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }

        public string Direccion { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        public IEnumerable<UsuarioRole> Roles { get; set; }
    }

    public class UsuarioRole
    {
        [Key]
        [Display(Name = "Nombre del Rol")]
        public string RoleName { get; set; }
    }

    public class UsuarioRol
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        [Display(Name = "Nombre del Rol")]
        public string RoleName { get; set; }
    }

    public class Rol
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Nombre del Rol")]
        public string RoleName { get; set; }
    }

    public class UsuarioyRoles
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        public List<UsuarioRol> colUsuarioRol { get; set; }
    }
}