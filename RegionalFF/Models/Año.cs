using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Año
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El año es requerido")]
        [Display(Name = "Año")]
        public int Anho { get; set; }

        public bool Activo { get; set; }
    }
}