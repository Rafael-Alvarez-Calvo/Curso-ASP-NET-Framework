using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class RolPaginaCLS
    {
        public int iidRolPagina { get; set; }
        public int iidRol { get; set; }
        public int iidPagina { get; set; }
        public int bhabilitado { get; set; }

        public string nombreRol { get; set; }
        public string nombreMensaje { get; set; }
    }
}