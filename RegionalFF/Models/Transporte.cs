using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Transporte
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Conductor")]
        [Display(Name = "Conductor")]
        public int ConductorId { get; set; }
        [ForeignKey("ConductorId")]
        public virtual Conductor Conductor { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Marca")]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public virtual Marca Marca { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nº de Chapa")]
        public string ChapaNro { get; set; }

        public bool Activo { get; set; }


    }
}