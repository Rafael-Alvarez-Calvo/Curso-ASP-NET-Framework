using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class MarcaCLS
    {
        [Display(Name = "Id Marca")]
        public int iidmarca { get; set; }

        [Display(Name = "Nombre marca")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion Marca")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}