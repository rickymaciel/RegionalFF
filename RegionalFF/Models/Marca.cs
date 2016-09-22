using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Marca
    {
        public Marca()
        {
            Transportes = new List<Transporte>();
        }
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<Transporte> Transportes { get; set; }
    }
}