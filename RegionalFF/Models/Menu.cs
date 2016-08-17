using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Menu
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        public virtual int PadreId { get; set; }

        [Required(ErrorMessage = "El nombre del menú es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del menú es requerida")]
        public string Descripcion { get; set; }

        public string Accion { get; set; }

        public string Controlador { get; set; }

        public bool Activo { get; set; }

        public List<Menu> Hijo()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Menus.Where(b => b.PadreId == this.Id).ToList();
        }
    }
}