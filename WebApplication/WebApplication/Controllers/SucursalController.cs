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
    }
}