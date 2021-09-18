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

        [Required]
        [Display(Name = "Nombre marca")]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Descripcion Marca")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}