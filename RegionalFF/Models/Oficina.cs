using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Oficina
    {
        public Oficina()
        {
            Usuarios = new List<ApplicationUser>();
        }
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }

        public String Sigla { get; set; }

        public String Departamento { get; set; }

        public String Ciudad { get; set; }

        public String Direccion { get; set; }

        public String Telefono { get; set; }

        public virtual ICollection<ApplicationUser> Usuarios { get; set; }
    }
}