using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegionalFF.Models
{
    public class ExpandedUserDTO
    {
        [Key]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Lockout End Date Utc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
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