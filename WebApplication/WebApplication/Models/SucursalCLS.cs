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

        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "La longitud máxima es 10")]
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido")] //Esta etiqueta valida si es un email correcto en formato
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Date)] //Esta etiqueta permite usar el tipo input fecha
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Esta etiqueta se utiliza para dar formato a la fecha y se establece que es editable
        [Display(Name = "Fecha Apertura")]
        public DateTime fechaApertura { get; set; } //DateTime establece que el dato es de tipo fecha
        public int bhabilitado { get; set; }
        
        public string mensajeError { get; set; }
    }
}