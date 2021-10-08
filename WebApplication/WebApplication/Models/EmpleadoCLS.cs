using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class EmpleadoCLS
    {
        [Display(Name ="ID")]
        public int iidempleado { get; set; }
        
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string nombre { get; set; }

        [Required]
        [Display(Name ="Apellido 1")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string appaterno { get; set; }

        [Required]
        [Display(Name ="Apellido 2")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string apmaterno { get; set; }
        
        /*[Required]
        [Display(Name = "Salario")]
        [Range(0,100000, ErrorMessage = "Fuera de Rango")]
        [StringLength(9, ErrorMessage = "La longitud máxima es 9")]
        public string sueldo { get; set; }
        */

        [Required]
        [DataType(DataType.Date)] //Esta etiqueta permite usar el tipo input fecha
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Esta etiqueta se utiliza para dar formato a la fecha y se establece que es editable
        [Display(Name = "Fecha Contrato")]
        public DateTime fechacontrato { get; set; }

        [Required]
        [Display(Name = "Tipo de Usuario")]
        public int iidtipoUsuario { get; set; }
        
        [Required]
        [Display(Name = "Tipo de Contrato")]
        public int iidtipoContrato { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public int iidSexo { get; set; }

        public int bhabilitado { get; set; }

        ////// PROPIEDADES ADICIONALES ////////////
        
        [Display(Name = "Tipo de contrato")]
        public string nombreTipoContrato { get; set; }

        [Display(Name = "Tipo de usuario")]
        public string nombreTipoUsuario { get; set; }
        public string mensajeErrorNombre { get; set; }
        public string mensajeErrorApPaterno { get; set; }
        public string mensajeErrorApMaterno { get; set; }
    }
}