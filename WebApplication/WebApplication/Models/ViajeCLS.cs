using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Models
{
    public class ViajeCLS
    {

        [Display(Name = "ID Viaje")]
        public int iidViaje { get; set; }

        [Required]
        [Display(Name = "Origen")]
        public int iidLugarOrigen { get; set; }

        [Required]
        [Display(Name = "Destino")]
        public int iidLugarDestino { get; set; }
        
        [Required]
        [Display(Name = "Bus")]
        public int iidBus { get; set; }

        [Required]
        [Display(Name = "Asientos disponibles")]
        public int numeroAsientosDisponibles { get; set; }
        public int bhabilitado { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public double precio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Viaje")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaViaje { get; set; }

        //////// Operaciones Adicionales ///////

        [Display(Name = "Origen")]
        public string origen { get; set; }
        
        [Display(Name = "Destino")]
        public string destino { get; set; }
        
        [Display(Name = "Bus")]
        public string bus { get; set; }
    }
}