﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegionalFF.Models
{
    public class Ciudad
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }

        [Remote("ComprobarDuplicacion", "Validation")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
    }
}