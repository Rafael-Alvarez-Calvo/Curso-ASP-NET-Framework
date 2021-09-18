using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ClientesCLS
    {

        [Display(Name = "ID de Cliente")]
        public int iidcliente { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        public string appaterno { get; set; }
        [Display(Name = "Apellido")]
        public string apmaterno { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        [Display(Name = "Sexo")]
        public int iidsexo { get; set; }
        [Display(Name = "Teléfono Fijo")]
        public string telefonofijo { get; set; }
        [Display(Name = "Teléfono Móvil")]
        public string telefonocelular { get; set; }
        public int bhabilitado { get; set; }
    }
}