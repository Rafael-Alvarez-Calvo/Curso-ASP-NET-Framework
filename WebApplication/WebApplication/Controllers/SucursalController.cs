using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        [HttpGet]
        public ActionResult Index(SucursalCLS oSucursalCLS)
        {
            string nombreSucursal = oSucursalCLS.nombre;

            List<SucursalCLS> listaSucursal = null;
            
            using (var bd = new BDPasajeEntities()) 
            {
                if (nombreSucursal == null)
                {
                    listaSucursal = (from sucursal in bd.Sucursal
                                        where sucursal.BHABILITADO == 1
                                        select new SucursalCLS
                                        {
                                            iidsucursal = sucursal.IIDSUCURSAL,
                                            nombre = sucursal.NOMBRE,
                                            direccion = sucursal.DIRECCION,
                                            telefono = sucursal.TELEFONO,
                                            email = sucursal.EMAIL,
                                            fechaApertura = (DateTime)sucursal.FECHAAPERTURA

                                        }).ToList();
                    
                }
                else
                {
                    listaSucursal = (from sucursal in bd.Sucursal
                                        where sucursal.BHABILITADO == 1
                                        && sucursal.NOMBRE.Contains(nombreSucursal)
                                        select new SucursalCLS
                                        {
                                            iidsucursal = sucursal.IIDSUCURSAL,
                                            nombre = sucursal.NOMBRE,
                                            direccion = sucursal.DIRECCION,
                                            telefono = sucursal.TELEFONO,
                                            email = sucursal.EMAIL,
                                            fechaApertura = (DateTime)sucursal.FECHAAPERTURA

                                        }).ToList();
                }
            }

            return View(listaSucursal);
        }

        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Agregar( SucursalCLS oSucursalCLS)
        {

            int registrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;

            using( var bd = new BDPasajeEntities())
            {
                registrosEncontrados = bd.Sucursal.Where(res => res.NOMBRE.Equals(nombreSucursal)).Count();
            }

            if(!ModelState.IsValid || registrosEncontrados >= 1)
            {
                if (registrosEncontrados >= 1) oSucursalCLS.mensajeError = "El nombre de sucursal ya existe";
                return View(oSucursalCLS); //Esto permite que si el usuario no introduce bien algun valor se mantengan los datos y no se borren los inputs
            }
            else
            {
                using(var bd = new BDPasajeEntities())
                {
                    Sucursal oSucursal = new Sucursal();

                    oSucursal.NOMBRE = oSucursalCLS.nombre;
                    oSucursal.DIRECCION = oSucursalCLS.direccion;
                    oSucursal.TELEFONO = oSucursalCLS.telefono;
                    oSucursal.EMAIL = oSucursalCLS.email;
                    oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                    oSucursal.BHABILITADO = 1;

                    bd.Sucursal.Add(oSucursal);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();

            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();

                oSucursalCLS.iidsucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaApertura = (DateTime) oSucursal.FECHAAPERTURA;
            }

            return View(oSucursalCLS);
        }


        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS)
        {
            int registrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            int idSucursal = oSucursalCLS.iidsucursal;

            using (var bd = new BDPasajeEntities())
            {
                registrosEncontrados = bd.Sucursal.Where(res => res.NOMBRE.Equals(nombreSucursal) && !res.IIDSUCURSAL.Equals(idSucursal)).Count();
            }

            if (!ModelState.IsValid || registrosEncontrados >= 1)
            {
                if (registrosEncontrados >= 1) oSucursalCLS.mensajeError = "El nombre de sucursal ya existe";
                return View(oSucursalCLS);
            }
            else
            {

                using( var bd = new BDPasajeEntities())
                {
                    Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();

                    oSucursal.NOMBRE = oSucursalCLS.nombre;
                    oSucursal.DIRECCION = oSucursalCLS.direccion;
                    oSucursal.TELEFONO = oSucursalCLS.telefono;
                    oSucursal.EMAIL = oSucursalCLS.email;
                    oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;

                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Borrar(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(res => res.IIDSUCURSAL.Equals(id)).First();

                oSucursal.BHABILITADO = 0;
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}