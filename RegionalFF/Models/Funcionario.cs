using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Funcionario
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(30, MinimumLength = 3)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(30, MinimumLength = 3)]
        public String Apellido { get; set; }

        [Required(ErrorMessage = "El Nº de Funcionario es requerido")]
        public int NumeroFuncionario { get; set; }

        public int Documento { get; set; }

        [Required(ErrorMessage = "El Email es requerido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Seleccionar una Oficina Regional")]
        [Display(Name = "Oficina: ")]
        public int OficinaId { get; set; }
        [ForeignKey("OficinaId")]
        public virtual Oficina Oficina { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Creado { get; set; }

        [Required(ErrorMessage = "La Fecha es requerida")]
        [Display(Name = "Fecha: ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Modificado { get; set; }

        public Boolean Activo { get; set; }

        
        public Funcionario()
        {
            Creado = DateTime.Now;
        }
    }
}