using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegionalFF.Models
{
    public class ExpandedUserDTO
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


        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        public IEnumerable<UserRolesDTO> Roles { get; set; }
    }

    public class UserRolesDTO
    {
        [Key]
        [Display(Name = "Nombre del Rol")]
        public string RoleName { get; set; }
    }

    public class UserRoleDTO
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        [Display(Name = "Nombre del Rol")]
        public string RoleName { get; set; }
    }

    public class RoleDTO
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Nombre del Rol")]
        public string RoleName { get; set; }
    }

    public class UserAndRolesDTO
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        public List<UserRoleDTO> colUserRoleDTO { get; set; }
    }
}