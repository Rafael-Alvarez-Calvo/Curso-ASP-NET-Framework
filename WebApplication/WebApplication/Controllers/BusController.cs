using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus

        [HttpGet]
        public ActionResult Index()
        {
            List<BusCLS> listaBuses = null;

            using( var bd = new BDPasajeEntities())
            {
                listaBuses = (from bus in bd.Bus
                                  join sucursal in bd.Sucursal
                                  on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                                    join tipoBus in bd.TipoBus
                                    on bus.IIDTIPOBUS equals tipoBus.IIDTIPOBUS
                                        join modelo in bd.Modelo
                                        on bus.IIDMODELO equals modelo.IIDMODELO
                                            join marca in bd.Marca
                                            on bus.IIDMARCA equals marca.IIDMARCA
                                                select new BusCLS
                                                {

                                                    iidbus = bus.IIDBUS,
                                                    placa = bus.PLACA,
                                                    nombreModelo = modelo.NOMBRE,
                                                    nombreSucursal = sucursal.NOMBRE,
                                                    nombreTipoBus = tipoBus.NOMBRE,
                                                    nombreMarca = marca.NOMBRE

                                                }).ToList();
            }

            return View(listaBuses);
        }
    }
}