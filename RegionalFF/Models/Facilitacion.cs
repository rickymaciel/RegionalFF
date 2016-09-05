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

        [Required(ErrorMessage = "Debe seleccionar el Mes")]
        [Display(Name = "Mes")]
        public int MesId { get; set; }
        [ForeignKey("MesId")]
        public virtual Meses Mes { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el Año")]
        public int AñoId { get; set; }
        [Display(Name = "Año")]
        [ForeignKey("AñoId")]
        public virtual Año Año { get; set; }

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

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Fecha { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime? FechaEdicion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Estadia")]
        public int Estadia { get; set; }

        [Display(Name = "Observaciones: ")]
        public string Observaciones { get; set; }
    }
}