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
        public ActionResult Index(BusCLS oBusCLS)
        {
            setSelectors();

            List<BusCLS> listaBuses = null; ;
            List<BusCLS> listaFiltradaBuses = new List<BusCLS>();

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
                                                where bus.BHABILITADO == 1 
                                                select new BusCLS
                                                {
                                                    iidbus = bus.IIDBUS,
                                                    placa = bus.PLACA,
                                                    nombreModelo = modelo.NOMBRE,
                                                    nombreSucursal = sucursal.NOMBRE,
                                                    nombreTipoBus = tipoBus.NOMBRE,
                                                    nombreMarca = marca.NOMBRE,
                                                    iidModelo = modelo.IIDMODELO,
                                                    iidSucursal = sucursal.IIDSUCURSAL,
                                                    iidMarca = marca.IIDMARCA,
                                                    iidTipoBus = tipoBus.IIDTIPOBUS,

                                                }).ToList();


                if(oBusCLS.iidbus == 0 && oBusCLS.iidMarca == 0 && oBusCLS.iidModelo == 0 && oBusCLS.iidSucursal == 0 && oBusCLS.iidTipoBus == 0 && oBusCLS.placa == null)
                {
                    listaFiltradaBuses = listaBuses;
                }
                else
                {

                    if(oBusCLS.iidbus != 0)
                    {
                        listaBuses = listaBuses.Where(res => res.iidbus.ToString().Contains(oBusCLS.iidbus.ToString())).ToList();
                    }

                    if(oBusCLS.placa != null)
                    {
                        listaBuses = listaBuses.Where(res => res.placa.Contains(oBusCLS.placa)).ToList();
                    }

                    if (oBusCLS.iidMarca != 0)
                    {
                        listaBuses = listaBuses.Where(res => res.iidMarca.ToString().Contains(oBusCLS.iidMarca.ToString())).ToList();
                    }

                    if (oBusCLS.iidModelo != 0)
                    {
                        listaBuses = listaBuses.Where(res => res.iidModelo.ToString().Contains(oBusCLS.iidModelo.ToString())).ToList();
                    }

                    if (oBusCLS.iidSucursal != 0)
                    {
                        listaBuses = listaBuses.Where(res => res.iidSucursal.ToString().Contains(oBusCLS.iidSucursal.ToString())).ToList();
                    }

                    if (oBusCLS.iidTipoBus != 0)
                    {
                        listaBuses = listaBuses.Where(res => res.iidTipoBus.ToString().Contains(oBusCLS.iidTipoBus.ToString())).ToList();
                    }

                    listaFiltradaBuses = listaBuses;
                }

            }

            return View(listaFiltradaBuses);
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
        
        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS)
        {
            int registrosEncontrados = 0;
            string placa = oBusCLS.placa;

            using (var bd = new BDPasajeEntities())
            {
                registrosEncontrados = bd.Bus.Count(res => res.PLACA.Equals(placa));
            }

            if (!ModelState.IsValid || registrosEncontrados > 0)
            {
                if (registrosEncontrados > 0) oBusCLS.mensajeErrorPlaca = $"El número de placa {placa} ya existe en el registro";
                setSelectors();
                return View(oBusCLS);
            }
            else
            {
                using( var bd = new BDPasajeEntities())
                {
                    Bus oBus = new Bus();

                    oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                    oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                    oBus.PLACA = oBusCLS.placa;
                    oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                    oBus.IIDMODELO = oBusCLS.iidModelo;
                    oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                    oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                    oBus.DESCRIPCION = oBusCLS.descripcion;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    oBus.IIDMARCA = oBusCLS.iidMarca;
                    oBus.BHABILITADO = 1;

                    bd.Bus.Add(oBus);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            setSelectors();

            BusCLS oBusCLS = new BusCLS();
            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();

                oBusCLS.iidbus = oBus.IIDBUS;
                oBusCLS.iidSucursal = (int)oBus.IIDSUCURSAL;
                oBusCLS.iidTipoBus = (int)oBus.IIDTIPOBUS;
                oBusCLS.iidModelo = (int)oBus.IIDMODELO;
                oBusCLS.iidMarca = (int)oBus.IIDMARCA;
                oBusCLS.placa = oBus.PLACA;
                oBusCLS.fechaCompra = (DateTime)oBus.FECHACOMPRA;
                oBusCLS.numeroFilas = (int)oBus.NUMEROFILAS;
                oBusCLS.numeroColumnas = (int)oBus.NUMEROCOLUMNAS;
                oBusCLS.descripcion = oBus.DESCRIPCION;
                oBusCLS.observacion = oBus.OBSERVACION;
            }

            return View(oBusCLS);
        }

        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS)
        {
            int registrosEncontrados = 0;
            string placa = oBusCLS.placa;

            using (var bd = new BDPasajeEntities())
            {
                registrosEncontrados = bd.Bus.Count(res => res.PLACA.Equals(placa) && !res.IIDBUS.Equals(oBusCLS.iidbus));
            }

            if (!ModelState.IsValid || registrosEncontrados > 0)
            {
                if(registrosEncontrados > 0) oBusCLS.mensajeErrorPlaca = $"El número de placa {placa} ya existe en el registro";
                setSelectors();
                return View(oBusCLS);
            }
            else
            {
                var idBus = oBusCLS.iidbus;

                using( var bd = new BDPasajeEntities())
                {
                    Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();

                    oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                    oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                    oBus.IIDMARCA = oBusCLS.iidMarca;
                    oBus.IIDMODELO= oBusCLS.iidModelo;
                    oBus.FECHACOMPRA= oBusCLS.fechaCompra;
                    oBus.PLACA = oBusCLS.placa;
                    oBus.NUMEROFILAS= oBusCLS.numeroFilas;
                    oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                    oBus.DESCRIPCION = oBusCLS.descripcion;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    oBus.BHABILITADO = 1;

                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Borrar(int iidBus)
        {
            using( var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.FirstOrDefault(res => res.IIDBUS.Equals(iidBus));
                oBus.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}