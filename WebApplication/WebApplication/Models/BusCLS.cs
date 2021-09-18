using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class BusCLS
    {

        [Display(Name = "ID Bus")]
        public int iidbus { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public int iidSucursal { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public int iidMarca { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public int iidModelo { get; set; }

        [Required]
        [Display(Name = "Tipo Bus")]
        public int iidTipoBus { get; set; }

        [Required]
        [Display(Name = "Matrícula")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 100 caracteres")]
        public string placa { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Compra")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }

        [Required]
        [Display(Name = "Filas")]
        public int numeroFilas { get; set; }

        [Required]
        [Display(Name = "Columnas")]
        public int numeroColumnas { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200 caracteres")]
        public string descripcion { get; set; }

        [Required]
        [Display(Name = "Observación")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200 caracteres")]
        public string observacion { get; set; }
        public int bhabilitado { get; set; }

        ////// PROPIEDADES ADICIONALES //////////////

        [Display(Name = "Sucursal")]
        public string nombreSucursal { get; set; }
        
        [Display(Name = "Marca")]
        public string nombreMarca { get; set; }

        [Display(Name = "Tipo Bus")]
        public string nombreTipoBus { get; set; }

        [Display(Name = "Modelo")]
        public string nombreModelo { get; set; }
    }
}