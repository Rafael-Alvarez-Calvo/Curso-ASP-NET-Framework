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
        public ActionResult Index()
        {

            List<SucursalCLS> listaSucursal = null;

            using (var bd = new BDPasajeEntities()) 
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
                return View(listaSucursal);
        }

        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Agregar( SucursalCLS oSucursalCLS)
        {
            if(!ModelState.IsValid)
            {
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
            if(!ModelState.IsValid)
            {
                return View(oSucursalCLS);
            }
            else
            {
                int idSucursal = oSucursalCLS.iidsucursal;

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
    }
}