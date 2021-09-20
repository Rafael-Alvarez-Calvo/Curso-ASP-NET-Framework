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


        public void setModelSelector()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Modelo
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMODELO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.listaModelo = lista;
            }
        }

        public void setBusTypeSelector()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.TipoBus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDTIPOBUS.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });

                ViewBag.listaTipoBus = lista;
            }
        }

        public void setBrandSelector()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Marca
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMARCA.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.listaMarca = lista;
            }
        }

        public void setSucursalSelector()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Sucursal
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDSUCURSAL.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });

                ViewBag.listaSucursal = lista;
            }
        }

        public void setSelectors()
        {
            setBrandSelector();
            setBusTypeSelector();
            setModelSelector();
            setSucursalSelector();
        }


        public ActionResult Agregar()
        {
            setSelectors();

            return View();
        }
    }
}