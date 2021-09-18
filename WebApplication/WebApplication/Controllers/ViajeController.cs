using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViajes = null;

            using (var bd = new BDPasajeEntities())
            {
                listaViajes = (from viaje in bd.Viaje
                                join origen in bd.Lugar
                                on viaje.IIDLUGARORIGEN equals origen.IIDLUGAR
                                    join destino in bd.Lugar
                                    on viaje.IIDLUGARDESTINO equals destino.IIDLUGAR
                                        join bus in bd.Bus
                                        on viaje.IIDBUS equals bus.IIDBUS
                                            select new ViajeCLS
                                            {
                                                iidViaje = viaje.IIDVIAJE,
                                                bus = bus.PLACA,
                                                origen = origen.NOMBRE,
                                                destino = destino.NOMBRE,

                                            }).ToList();
            }

            return View(listaViajes);
        }
    }
}