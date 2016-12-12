using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Facilitacion
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Usuario")]
        [Display(Name = "Funcionario")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Usuario { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el Origen")]
        [Display(Name = "Origen")]
        public int PaisId { get; set; }
        [ForeignKey("PaisId")]
        public virtual Pais Pais { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el Destino")]
        [Display(Name = "Destino")]
        public int CiudadId { get; set; }
        [ForeignKey("CiudadId")]
        public virtual Ciudad Ciudad { get; set; }

        [Range(1, 100)]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1, 100, ErrorMessage = "Rango de Estadía entre 1 y 100")]
        [Display(Name = "Estadía")]
        public int Estadia { get; set; }

        [Display(Name = "Observaciones: ")]
        [StringLength(140, MinimumLength = 3)]
        public string Observaciones { get; set; }
    }
}