using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            List<EmpleadoCLS> listaEmpleados = null;

            using( var bd = new BDPasajeEntities())
            {
                listaEmpleados = (from empleados in bd.Empleado
                                    join tipousuario in bd.TipoUsuario
                                    on empleados.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                    join tipocontrato in bd.TipoContrato
                                    on empleados.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                    select new EmpleadoCLS
                                    {
                                        iidempleado = empleados.IIDEMPLEADO,
                                        nombre = empleados.NOMBRE,
                                        appaterno = empleados.APPATERNO,
                                        apmaterno = empleados.APMATERNO,
                                        sueldo = (float)empleados.SUELDO,
                                        fechacontrato = (DateTime)empleados.FECHACONTRATO,
                                        iidtipoSexo = (int)empleados.IIDSEXO,
                                        nombreTipoUsuario = tipousuario.NOMBRE,
                                        nombreTipoContrato = tipocontrato.NOMBRE

                                    }).ToList();
            }

            return View(listaEmpleados);
        }
    }
}