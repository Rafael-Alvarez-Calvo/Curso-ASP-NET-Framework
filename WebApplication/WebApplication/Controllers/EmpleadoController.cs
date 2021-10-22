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
        public ActionResult Index(EmpleadoCLS oEmpleadoCLS)
        {
            setUserTypeSelector();
            List<EmpleadoCLS> listaEmpleados = null;

            string nombreEmpleado = oEmpleadoCLS.nombre;
            int tipoEmpleado = oEmpleadoCLS.iidtipoUsuario;

            using( var bd = new BDPasajeEntities())
            {
                listaEmpleados = (from empleados in bd.Empleado
                                    join tipousuario in bd.TipoUsuario
                                    on empleados.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                    join tipocontrato in bd.TipoContrato
                                    on empleados.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                    where empleados.BHABILITADO == 1
                                    && (tipoEmpleado == 0 || empleados.IIDTIPOUSUARIO == tipoEmpleado)
                                    && (nombreEmpleado == null || empleados.NOMBRE.Contains(nombreEmpleado))
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

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione Sexo --", Value = "" });
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

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione Tipo de contrato --", Value = "" });
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

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione Tipo de usuario --", Value = "" });
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

            int registroNombreEncontrado = 0;
            int registroapPaternoEncontrado = 0;
            int registroapMaternoEncontrado = 0;

            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.appaterno;
            string apMaterno = oEmpleadoCLS.apmaterno;


            using( var bd = new BDPasajeEntities())
            {
                registroNombreEncontrado = bd.Empleado.Where(res => res.NOMBRE.Equals(nombre)).Count();

                registroapPaternoEncontrado = bd.Empleado.Where(res => res.APPATERNO.Equals(apPaterno)).Count();

                registroapMaternoEncontrado = bd.Empleado.Where(res => res.APMATERNO.Equals(apMaterno)).Count();
            }

            if(!ModelState.IsValid || (registroNombreEncontrado > 0 && registroapPaternoEncontrado > 0 && registroapMaternoEncontrado > 0))
            {
                if (registroNombreEncontrado > 0 && registroapPaternoEncontrado > 0 && registroapMaternoEncontrado > 0)
                {
                    oEmpleadoCLS.mensajeErrorNombre = $"El nombre {nombre} ya existe en el registro";
                    oEmpleadoCLS.mensajeErrorApPaterno = $"El 1º apellido ya existe para el nombre {nombre}";
                    oEmpleadoCLS.mensajeErrorApMaterno = $"El 2º apellido ya existe para el nombre {nombre}";
                }

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

                oEmpleadoCLS.iidempleado = oEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.appaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.apmaterno = oEmpleado.APMATERNO;
                oEmpleadoCLS.fechacontrato = (DateTime)oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.iidSexo = (int)oEmpleado.IIDSEXO;
                oEmpleadoCLS.iidtipoContrato = (int)oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidtipoUsuario = (int)oEmpleado.IIDTIPOUSUARIO;
                //oEmpleaCLSdo.SUELDO = oEmpleadoCLS.sueldo;

            }

            return View(oEmpleadoCLS);
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {

            if(!ModelState.IsValid)
            {
                setSelectors();
                return View(oEmpleadoCLS);
            }
            else
            {
                int idEmpleado = oEmpleadoCLS.iidempleado;

                using( var bd = new BDPasajeEntities())
                {
                    Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmpleado)).First();

                    oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                    oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                    oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechacontrato;
                    oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                    oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                    oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;

                    bd.SaveChanges();

                }

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Borrar(int iidempleado)
        {
            using( var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(res => res.IIDEMPLEADO.Equals(iidempleado)).First();

                oEmpleado.BHABILITADO = 0;

                bd.SaveChanges();
                
                return RedirectToAction("Index");
            }

        }
    }
}