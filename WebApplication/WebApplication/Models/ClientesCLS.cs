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

        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        
        [Required]
        [StringLength(150, ErrorMessage = "La longitud máxima es 150")]
        [Display(Name = "Apellido 1")]
        public string appaterno { get; set; }
        
        [Required]
        [StringLength(150, ErrorMessage = "La longitud máxima es 150")]
        [Display(Name = "Apellido 2")]
        public string apmaterno { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public int iidsexo { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "La longitud máxima es 10")]
        [Display(Name = "Teléfono Fijo")]
        public string telefonofijo { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "La longitud máxima es 10")]
        [Display(Name = "Teléfono Móvil")]
        public string telefonocelular { get; set; }
        public int bhabilitado { get; set; }
    }
}