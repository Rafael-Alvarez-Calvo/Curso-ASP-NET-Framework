using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class PaginaCLS
    {
        [Required]
        [Display(Name = "Id Página")]
        public int iidpagina { get; set; }
        public int bhabilitado { get; set; }

        [Required]
        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }
        
        [Required]
        [Display(Name = "Acción")]
        public string accion { get; set; }

        [Required]
        [Display(Name = "Controlador")]
        public string controlador { get; set; }
    }
}