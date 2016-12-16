using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RegionalFF.Models
{
    public class VisitasMes
    {
        [Display(Name = "Color")]
        public string lineColor { get; set; }

        [Display(Name = "Fecha")]
        public string date { get; set; }

        [Display(Name = "Cantidad")]
        public int duration { get; set; }
    }
}