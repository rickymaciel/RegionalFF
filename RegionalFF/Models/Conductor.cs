using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Conductor
    {
        //public Conductor()
        //{
        //    Transportes = new List<Transporte>();
        //}
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public int Documento { get; set; }

        public bool Activo { get; set; }

        //public virtual ICollection<Transporte> Transportes { get; set; }
    }
}