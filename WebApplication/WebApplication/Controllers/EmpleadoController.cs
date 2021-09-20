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
                                        fechacontrato = (DateTime)empleados.FECHACONTRATO,
                                        iidSexo = (int)empleados.IIDSEXO,
                                        nombreTipoUsuario = tipousuario.NOMBRE,
                                        nombreTipoContrato = tipocontrato.NOMBRE

                                    }).ToList();
            }

            return View(listaEmpleados);
        }

        public void setSexSelector()
        {
            List<SelectListItem> lista;

            using( var bd = new BDPasajeEntities())
            {
                lista = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()

                             }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.listaSexo = lista;
            }
        }


        public void setContractTypeSelector()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from TipoContrato in bd.TipoContrato
                         where TipoContrato.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = TipoContrato.NOMBRE,
                             Value = TipoContrato.IIDTIPOCONTRATO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.listaTipoContrato = lista;
            }
        }

        public void setUserTypeSelector()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from TipoUsuario in bd.TipoUsuario
                         where TipoUsuario.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = TipoUsuario.NOMBRE,
                             Value = TipoUsuario.IIDTIPOUSUARIO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.listaTipoUsuario = lista;
            }
        }

        public void setSelectors()
        {
            setSexSelector();
            setContractTypeSelector();
            setUserTypeSelector();
        }


        public ActionResult Agregar()
        {
            setSelectors();
            return View();
        }


        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            if(!ModelState.IsValid)
            {
                setSelectors();
                return View(oEmpleadoCLS);
            }
            else
            {
                using( var bd = new BDPasajeEntities())
                {
                    Empleado oEmpleado = new Empleado();

                    oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                    oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                    oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                    oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechacontrato;
                    //oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                    oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                    oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                    oEmpleado.BHABILITADO = 1;

                    bd.Empleado.Add(oEmpleado);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            setSelectors();

            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();

            using( var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();

                oEmpleado.IIDEMPLEADO = oEmpleadoCLS.iidempleado;
                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;
                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechacontrato;
                //oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;

            }

            return View(oEmpleadoCLS);
        }
    }
}