using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegionalFF.Models
{
    public class UsuarioAmpliado
    {
        [Key]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingresar la contraseña para confirmar cambios")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

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

        [ForeignKey("OficinaId")]
        [Display(Name = "Oficina")]
        public virtual Oficina Oficina { get; set; }

        [Display(Name = "Lockout End Date Utc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }

        public string Direccion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Imagen { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public IEnumerable<UsuarioRole> Roles { get; set; }
    }

    public class UsuarioRole
    {
        [Key]
        [Display(Name = "Email del Rol")]
        public string Email { get; set; }
    }

    public class UsuarioRol
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string Email { get; set; }
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
        public string Email { get; set; }
        public List<UsuarioRol> colUsuarioRol { get; set; }
    }
}