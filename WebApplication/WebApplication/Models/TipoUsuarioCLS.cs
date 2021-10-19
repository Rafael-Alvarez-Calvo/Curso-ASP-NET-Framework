using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name = "Id Tipo Usuario")]
        public int iidtipousuario { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(150, ErrorMessage = "La longitud máxima es de 150")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(250, ErrorMessage = "La longitud máxima es de 250")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}