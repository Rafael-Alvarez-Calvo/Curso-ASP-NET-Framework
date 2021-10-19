using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class RolCLS
    {
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        
        [Required]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Id")]
        public int iidRol { get; set; }
        public int bhabilitado { get; set; }
    }
}