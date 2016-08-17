using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Oficina
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(30, MinimumLength = 3)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El Departamento es requerido")]
        [StringLength(30, MinimumLength = 3)]
        public String Departamento { get; set; }

        [Required(ErrorMessage = "La Ciudad es requerida")]
        [StringLength(30, MinimumLength = 3)]
        public String Ciudad { get; set; }

        public String Telefono { get; set; }

        public String Ubicacion { get; set; }

        public String Director { get; set; }



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

        public Oficina()
        {
            Creado = DateTime.Now;
        }

        public virtual ICollection<Funcionario> funcionarios { get; set; }
    }
}