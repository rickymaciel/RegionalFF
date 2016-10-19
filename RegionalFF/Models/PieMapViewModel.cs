using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegionalFF.Models
{
    public class PieMapViewModel
    {
        /// <summary>
        /// datos de gráficos
        /// </summary>
        public List<string> LegendData { get; set; }

        /// <summary>
        /// datos de gráficos
        /// </summary>
        public List<VisitSource> SeriesData { get; set; }
    }
}