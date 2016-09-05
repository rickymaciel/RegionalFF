using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Meses
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}