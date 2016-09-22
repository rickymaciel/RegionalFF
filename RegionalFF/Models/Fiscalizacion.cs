using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Fiscalizacion
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Usuario")]
        [Display(Name = "Funcionario")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Fiscal { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Transporte")]
        [Display(Name = "Transporte")]
        public int TransporteId { get; set; }
        [ForeignKey("TransporteId")]
        public virtual Transporte Transporte { get; set; }


        [Required(ErrorMessage = "Debe seleccionar el Origen")]
        [Display(Name = "Ciudad de Origen")]
        public int CiudadId { get; set; }
        [ForeignKey("CiudadId")]
        public virtual Ciudad Ciudad { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el Destino")]
        [Display(Name = "Pais de Destino")]
        public int PaisId { get; set; }
        [ForeignKey("PaisId")]
        public virtual Pais Pais { get; set; }

        [Required(ErrorMessage = "Debe indicar la cantidad de pasajeros")]
        [Display(Name = "Cantidad de pasajeros")]
        public int CantidadPasajeros { get; set; }


        [Display(Name = "Observaciones: ")]
        public string Observaciones { get; set; }

        [Display(Name = "Cuenta con la habilitación de la SENATUR?")]
        public bool Habilitado { get; set; }

        public bool Activo { get; set; }
    }
}