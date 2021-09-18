using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class SucursalCLS
    {

        [Display(Name = "Id Sucursal")]
        public int iidsucursal { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        public DateTime fechaApertura { get; set; }
        public int bhabilitado { get; set; }
    }
}